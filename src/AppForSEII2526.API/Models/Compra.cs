namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }


        


        [Required, StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        public string direccionEnvio { get; set; }


       


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioTotal { get; set; }

        [Required]
        public formaPago metodopago { get; set; }

        
        public IList<ComprarItem> CompraItems { get; set; }

        public ApplicationUser ApplicationUser { get; set; }




    }
    public enum formaPago
    {
        Efectivo,
        TarjetaCredito,
        PayPal
    }
}
