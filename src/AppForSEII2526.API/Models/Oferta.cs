namespace AppForSEII2526.API.Models
{
    public class Oferta
    {
        [Key]
        public int Id { get; set; }


        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaFin { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaOferta { get; set; }


        // Relaciones
        public List<OfertaItem> ofertaItems { get; set; }
        public tiposMetodoPago metodoPago { get; set; }
        

        public tiposDirigidaOferta paraSocio { get; set; }
    }
    public enum tiposMetodoPago
        {
            Tarjeta,
            PayPal,
            Efectivo
        }
    public enum tiposDirigidaOferta
        {
            Socios,
            Clientes
        }
}
