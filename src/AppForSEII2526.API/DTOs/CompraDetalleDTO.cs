using SQLitePCL;

namespace AppForSEII2526.API.DTOs
{
    public class CompraDetalleDTO
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        public string direccionEnvio { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float? precioTotal { get; set; }

        public IList<CompraItemDTO> HerramientasCompradas { get; set; }

        public CompraDetalleDTO(int id, string direccionEnvio, DateTime fechaCompra, float? precioTotal, IList<CompraItemDTO> herramientasCompradas)
        {
            Id = id;
            this.direccionEnvio = direccionEnvio;
            this.fechaCompra = fechaCompra;
            HerramientasCompradas = herramientasCompradas;
            this.precioTotal = HerramientasCompradas.Sum(h => h.precio * h.cantidad);
        }

        public CompraDetalleDTO()
        {
        }

       

        protected bool CompareDate(DateTime date1, DateTime date2)
        {
            return (date1.Subtract(date2) < new TimeSpan(0, 1, 0));
        }

        public override bool Equals(object? obj)
        {
            if (obj is not CompraDetalleDTO dto)
                return false;

            return Id == dto.Id
                && precioTotal == dto.precioTotal
                && CompareDate(fechaCompra, dto.fechaCompra)
                && direccionEnvio == dto.direccionEnvio
                && HerramientasCompradas.SequenceEqual(dto.HerramientasCompradas);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, precioTotal, fechaCompra, direccionEnvio);
        }
    }
}
