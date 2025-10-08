namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(alquilarid), nameof(herramientaid))]
    public class alquilarItem
    {

        [Required]
        public int cantidad { get; set; }

        public int alquilarid { get; set; }

        public int herramientaid { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }


        // Relaciones
        public alquilar alquilar { get; set; }
        public Herramienta herramienta { get; set; }






        }


}
