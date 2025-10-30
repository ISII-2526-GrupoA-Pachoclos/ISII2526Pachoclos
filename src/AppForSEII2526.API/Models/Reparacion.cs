namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {

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

        public ApplicationUser ApplicationUser { get; set; }

    }
    public enum metodoPago
    {
        Efectivo,
        TarjetaCredito,
        PayPal
    }
}
