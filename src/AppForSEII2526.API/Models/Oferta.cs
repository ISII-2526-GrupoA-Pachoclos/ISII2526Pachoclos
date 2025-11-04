namespace AppForSEII2526.API.Models
{
    public class Oferta
    {
        public Oferta()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaOferta { get; set; }


        // Relaciones
        public List<OfertaItem> OfertaItems { get; set; }

        // Navegación opcional: no forzar la creación de un ApplicationUser vacío
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public tiposMetodoPago MetodoPago { get; set; }
        

        public tiposDirigidaOferta ParaSocio { get; set; }


        public Oferta(DateTime fechaInicio, DateTime fechaFin, DateTime fechaOferta, List<OfertaItem> ofertaItems, ApplicationUser? applicationUser, tiposMetodoPago metodoPago, tiposDirigidaOferta paraSocio)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaOferta = fechaOferta;
            OfertaItems = ofertaItems;
            MetodoPago = metodoPago;
            ParaSocio = paraSocio;
        }
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
