namespace AppForSEII2526.API.Models
{
    public class alquilar
    {

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





        
    }
    public enum tiposMetodosPago
        {
            tarjetaCredito,
            paypal,
            Efectivo

        }

}

