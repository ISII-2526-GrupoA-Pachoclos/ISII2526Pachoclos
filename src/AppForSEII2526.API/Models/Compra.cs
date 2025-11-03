using Microsoft.DotNet.Scaffolding.Shared.T4Templating;

namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        public Compra()
        {
            CompraItems = new List<ComprarItem>();
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

        // Navegación nullable para evitar inserciones de ApplicationUser sin datos
        public ApplicationUser? ApplicationUser { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Compra compra &&
                   Id == compra.Id &&
                   direccionEnvio == compra.direccionEnvio &&
                   fechaCompra == compra.fechaCompra &&
                   precioTotal == compra.precioTotal &&
                   metodopago == compra.metodopago &&
                   EqualityComparer<IList<ComprarItem>>.Default.Equals(CompraItems, compra.CompraItems) &&
                   EqualityComparer<ApplicationUser?>.Default.Equals(ApplicationUser, compra.ApplicationUser);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, direccionEnvio, fechaCompra, precioTotal, metodopago, CompraItems, ApplicationUser);
        }
    }
    public enum formaPago
    {
        Efectivo,
        TarjetaCredito,
        PayPal
    }
}
