namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(idAlquiler), nameof(idHerramienta))]
    public class alquilarItem
    {

        [Required]
        public int cantidad { get; set; }

        public int idAlquiler { get; set; }

        public int idHerramienta { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        // Relaciones
        public alquilar alquilar { get; set; }
        public Herramienta herramienta { get; set; }






        }


}
}
