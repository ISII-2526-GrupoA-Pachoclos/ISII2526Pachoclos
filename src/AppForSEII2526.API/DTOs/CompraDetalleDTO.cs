using SQLitePCL;

namespace AppForSEII2526.API.DTOs
{
    public class CompraDetalleDTO
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        public string direccionEnvio { get; set; }

        //[Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime fechaCompra { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float? precioTotal { get; set; }

        public IList<CompraItemDTO> HerramientasCompradas { get; set; }

        public CompraDetalleDTO(int id, string direccionEnvio, float? precioTotal, IList<CompraItemDTO> herramientasCompradas)
        {
            Id = id;
            this.direccionEnvio = direccionEnvio;
            //this.fechaCompra = fechaCompra;
            HerramientasCompradas = herramientasCompradas;
            this.precioTotal = HerramientasCompradas.Sum(h => h.precio * h.cantidad);
        }

    }
}
