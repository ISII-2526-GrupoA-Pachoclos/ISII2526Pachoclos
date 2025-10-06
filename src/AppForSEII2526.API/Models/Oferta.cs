namespace AppForSEII2526.API.Models
{
    public class Oferta
    {
        [Key]
        public int Id { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaFin { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaOferta { get; set; }

        public enum tiposMetodoPago
        {
            Tarjeta,
            PayPal,
            Efectivo
        }

        public enum tiposDirigidaOferta
        {
            Socios,
            Clientes
        }
    }
}
