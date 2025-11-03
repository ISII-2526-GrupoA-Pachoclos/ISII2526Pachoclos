namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
        public Reparacion()
        {
            ReparacionItems = new List<ReparacionItem>();
           
        }

        public Reparacion(DateTime fechaEntrega, DateTime fechaRecogida, int id, float precioTotal, 
            IList<ReparacionItem> reparacionItems, metodoPago metodoPago, ApplicationUser applicationUser)
        {
            this.fechaEntrega = fechaEntrega;
            this.fechaRecogida = fechaRecogida;
            this.id = id;
            this.precioTotal = precioTotal;
            ReparacionItems = reparacionItems;
            this.metodoPago = metodoPago;
            ApplicationUser = applicationUser;
        }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaEntrega { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaRecogida { get; set; }

        [Key]
        public int id { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioTotal { get; set; }

        // Relaciones
        public IList<ReparacionItem> ReparacionItems { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        public metodoPago metodoPago { get; set; }

        // Dejar la navegación nullable para evitar inserciones accidentales de ApplicationUser vacío
        public ApplicationUser? ApplicationUser { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Reparacion reparacion &&
                   fechaEntrega == reparacion.fechaEntrega &&
                   fechaRecogida == reparacion.fechaRecogida &&
                   id == reparacion.id &&
                   precioTotal == reparacion.precioTotal &&
                   EqualityComparer<IList<ReparacionItem>>.Default.Equals(ReparacionItems, reparacion.ReparacionItems) &&
                   metodoPago == reparacion.metodoPago &&
                   EqualityComparer<ApplicationUser?>.Default.Equals(ApplicationUser, reparacion.ApplicationUser);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(fechaEntrega, fechaRecogida, id, precioTotal, ReparacionItems, 
                metodoPago, ApplicationUser);
        }
    }
    public enum metodoPago
    {
        Efectivo,
        TarjetaCredito,
        PayPal
    }
}
