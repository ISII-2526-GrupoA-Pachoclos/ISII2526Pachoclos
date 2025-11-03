
namespace AppForSEII2526.API.DTOs
{
    public class HerramientasParaComprarDTO
    {

        [Key]
        public int id { get; set; }

        [Required, StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string nombre { get; set; }

        [StringLength(50, ErrorMessage = "No puede tener mas de 50 caracteres.", MinimumLength = 1)]
        public string material { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        public string fabricante { get; set; }

        public HerramientasParaComprarDTO(int id, string nombre, string material, string fabricante, float precio)
        {
            this.id = id;
            this.nombre = nombre;
            this.material = material;
            this.fabricante = fabricante;
            this.precio = precio;
        }

        public HerramientasParaComprarDTO()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is HerramientasParaComprarDTO dTO &&
                   id == dTO.id &&
                   nombre == dTO.nombre &&
                   material == dTO.material &&
                   precio == dTO.precio &&
                   fabricante == dTO.fabricante;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, nombre, material, precio, fabricante);
        }
    }
}
