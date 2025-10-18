using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Data;

namespace AppForSEII2526.API.DTOs
{
    public class HerramientasParaOfertasDTO
    {
        public int id { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string material { get; set; }

        [Required, StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombre { get; set; }
        
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        public string fabricante { get; set; }

        // Constructor sin parámetros requerido por EF y proyecciones LINQ
        public HerramientasParaOfertasDTO() { }

        public HerramientasParaOfertasDTO(int id, string material, string nombre, float precio, string fabricante) {
            this.id = id;
            this.material = material;
            this.nombre = nombre;
            this.precio = precio;
            this.fabricante = fabricante;
        }

        

    }
}
