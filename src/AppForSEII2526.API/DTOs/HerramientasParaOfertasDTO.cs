using Microsoft.EntityFrameworkCore;
using AppForSEII2526.API.Data;

namespace AppForSEII2526.API.DTOs
{
    public class HerramientasParaOfertasDTO
    {
        [Required, StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string Material { get; set; }

        public string Fabricante { get; set; }
        
        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float Precio { get; set; }



        // Constructor sin parámetros requerido por EF y proyecciones LINQ
        public HerramientasParaOfertasDTO() { }

        public HerramientasParaOfertasDTO(string nombre, string material, string fabricante, float precio) {
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
        }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasParaOfertasDTO dTO &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Material, Fabricante, Precio);
        }
    }
}
