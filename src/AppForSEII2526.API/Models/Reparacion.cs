namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
        [Required]
        [StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string apellidoCliente { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaEntrega { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaRecogida { get; set; }

        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string nombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "El número de teléfono no es válido.")]
        public string numTelefono { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioTotal { get; set; }


        // Relaciones

        public IList<ReparacionItem> ReparacionItems { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        public metodoPago metodoPago { get; set; }

    }
    public enum metodoPago
    {
        Efectivo,
        TarjetaCredito,
        PayPal
    }
}
