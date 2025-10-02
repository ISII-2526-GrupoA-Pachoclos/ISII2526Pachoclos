namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
        [StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        public string apellidoCliente { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaEntrega { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaRecogida { get; set; }

        [Key]
        public int id { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string numTelefono { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioTotal { get; set; }

        public metodoPago metodoPago { get; set; }

    }
    public enum metodoPago
    {
        Efectivo,
        TarjetaCredito,
        PayPal
    }
}
