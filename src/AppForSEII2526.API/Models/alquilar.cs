namespace AppForSEII2526.API.Models
{
    public class alquilar
    {
        public alquilar()
        {
            alquilarItems = new List<alquilarItem>();
            applicationUser = new ApplicationUser();
        }

        public alquilar(int id, string direccionEnvio, DateTime fechaAlquiler, DateTime fechaInicio, DateTime fechaFin, 
            float precioTotal, tiposMetodosPago metodoPago, List<alquilarItem> alquilarItems, ApplicationUser applicationUser)
        {
            this.id = id;
            this.direccionEnvio = direccionEnvio;
            this.fechaAlquiler = fechaAlquiler;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.precioTotal = precioTotal;
            this.metodoPago = metodoPago;
            this.alquilarItems = alquilarItems;
            this.applicationUser = applicationUser;
        }

        [Key]
        public int id { get; set; }


        [Required, StringLength(100, ErrorMessage = "No puede tener mas de 100 caracteres.", MinimumLength = 1)]
        public string direccionEnvio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaAlquiler { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaFin { get; set; }

     

        [Required, StringLength(30, ErrorMessage = "No puede tener mas de 30 caracteres.", MinimumLength = 1)]
        //public string Periodo get; set;}

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precioTotal { get; set; }

        public tiposMetodosPago metodoPago { get; set; }

        // Relaciones
        public List<alquilarItem> alquilarItems { get; set; }

        public ApplicationUser applicationUser { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is alquilar alquilar &&
                   id == alquilar.id &&
                   direccionEnvio == alquilar.direccionEnvio &&
                   fechaAlquiler == alquilar.fechaAlquiler &&
                   fechaInicio == alquilar.fechaInicio &&
                   fechaFin == alquilar.fechaFin &&
                   precioTotal == alquilar.precioTotal &&
                   metodoPago == alquilar.metodoPago &&
                   EqualityComparer<List<alquilarItem>>.Default.Equals(alquilarItems, alquilar.alquilarItems) &&
                   EqualityComparer<ApplicationUser>.Default.Equals(applicationUser, alquilar.applicationUser);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(id);
            hash.Add(direccionEnvio);
            hash.Add(fechaAlquiler);
            hash.Add(fechaInicio);
            hash.Add(fechaFin);
            hash.Add(precioTotal);
            hash.Add(metodoPago);
            hash.Add(alquilarItems);
            hash.Add(applicationUser);
            return hash.ToHashCode();
        }
    }
    public enum tiposMetodosPago
        {
            tarjetaCredito,
            paypal,
            Efectivo

        }

}

