namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        public Compra()
        {
            CompraItems = new List<ComprarItem>();
            ApplicationUser = new ApplicationUser();
            metodopago = new formaPago();
        }

        public Compra(int id, string direccionEnvio, DateTime fechaCompra, float? precioTotal, formaPago metodopago,
            IList<ComprarItem> compraItems, ApplicationUser applicationUser)
        {
            Id = id;
            this.direccionEnvio = direccionEnvio;
            this.fechaCompra = fechaCompra;
            this.precioTotal = precioTotal;
            this.metodopago = metodopago;
            CompraItems = compraItems;
            ApplicationUser = applicationUser;
        }

        public Compra(string direccionEnvio, DateTime fechaCompra, float? precioTotal, formaPago metodopago,
            IList<ComprarItem> compraItems, ApplicationUser applicationUser)
        {
            
            this.direccionEnvio = direccionEnvio;
            this.fechaCompra = fechaCompra;
            this.precioTotal = precioTotal;
            this.metodopago = metodopago;
            CompraItems = compraItems;
            ApplicationUser = applicationUser;
        }

        [Key]
        public int Id { get; set; }


        


        [Required, StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        public string direccionEnvio { get; set; }


        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }




        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float? precioTotal { get; set; }

        

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
