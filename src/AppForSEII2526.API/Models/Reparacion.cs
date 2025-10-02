namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
        [StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        public string apellidoCliente { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime fechaEntrega { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime fechaRecogida { get; set; }

        [Key]
        public int id { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombreCliente { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string numTelefono { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioTotal { get; set; }
    }
}
