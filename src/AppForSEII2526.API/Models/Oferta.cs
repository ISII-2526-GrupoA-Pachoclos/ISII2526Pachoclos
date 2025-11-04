namespace AppForSEII2526.API.Models
{
    public class Oferta
    {
        public Oferta()
        {
        }

        public Oferta(int id, DateTime fechaInicio, DateTime fechaFin, DateTime fechaOferta, List<OfertaItem> ofertaItems, 
            ApplicationUser applicationUser, tiposMetodoPago metodoPago, tiposDirigidaOferta paraSocio)
        {
            Id = id;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.fechaOferta = fechaOferta;
            this.ofertaItems = ofertaItems;
            ApplicationUser = applicationUser;
            this.metodoPago = metodoPago;
            this.paraSocio = paraSocio;
        }

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

        // Navegación opcional: no forzar la creación de un ApplicationUser vacío
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public tiposMetodoPago metodoPago { get; set; }
        

        public tiposDirigidaOferta paraSocio { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Oferta oferta &&
                   Id == oferta.Id &&
                   fechaInicio == oferta.fechaInicio &&
                   fechaFin == oferta.fechaFin &&
                   fechaOferta == oferta.fechaOferta &&
                   EqualityComparer<List<OfertaItem>>.Default.Equals(ofertaItems, oferta.ofertaItems) &&
                   EqualityComparer<ApplicationUser?>.Default.Equals(ApplicationUser, oferta.ApplicationUser) &&
                   metodoPago == oferta.metodoPago &&
                   paraSocio == oferta.paraSocio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, fechaInicio, fechaFin, fechaOferta, ofertaItems, ApplicationUser, 
                metodoPago, paraSocio);
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
