namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }


        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombreCliente { get; set; }


        [StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        public string direccionEnvio { get; set; }


        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string apellidoCliente { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string correoElectronico { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioTotal { get; set; }

        [StringLength(9, ErrorMessage = "No puede tener mas de 9 numeros.", MinimumLength = 1)]
        public string telefono { get; set; }

        public formaPago metodopago { get; set; }

        
        public IList<ComprarItem> CompraItems { get; set; }




    }
    public enum formaPago
    {
        Efectivo,
        TarjetaCredito,
        PayPal
    }
}
