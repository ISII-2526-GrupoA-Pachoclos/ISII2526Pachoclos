
namespace AppForSEII2526.API.DTOs
{
    public class CompraItemDTO
    {

        [Key]
        public int herramientaid { get; set; }

        [Required]
        public int cantidad { get; set; }

        [Required, StringLength(50, ErrorMessage="No puede tener mas de 50 caracteres", MinimumLength=1)]
        public string nombre { get; set; }

        
        public string? descripcion { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        public float precio { get; set; }

        

        public CompraItemDTO(int herramientaid, int cantidad, string nombre, string descripcion, float precio)
        {
            this.herramientaid = herramientaid;
            this.cantidad = cantidad;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
            
        }

        public CompraItemDTO()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is CompraItemDTO dTO &&
                   herramientaid == dTO.herramientaid &&
                   cantidad == dTO.cantidad &&
                   nombre == dTO.nombre &&
                   descripcion == dTO.descripcion &&
                   precio == dTO.precio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(herramientaid, cantidad, nombre, descripcion, precio);
        }
    }
}
