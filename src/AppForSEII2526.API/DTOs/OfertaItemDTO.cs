
namespace AppForSEII2526.API.DTOs
{
    public class OfertaItemDTO
    {
        public int herramientaId { get; set; }
        public string nombre { get; set; }
        public string material { get; set; }
        public string fabricante { get; set; }
        public float precio { get; set; }
        public float precioOferta { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        public int Porcentaje { get; set; }

        public OfertaItemDTO()
        {
        }


        public OfertaItemDTO(string nombre, string material, string fabricante, float precio, float precioOferta, int porcentaje)
        {
            this.nombre = nombre;
            this.material = material;
            this.fabricante = fabricante;
            this.precio = precio;
            this.precioOferta = precioOferta;
            this.Porcentaje = porcentaje;
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItemDTO dTO &&
                   herramientaId == dTO.herramientaId &&
                   nombre == dTO.nombre &&
                   material == dTO.material &&
                   fabricante == dTO.fabricante &&
                   precio == dTO.precio &&
                   precioOferta == dTO.precioOferta &&
                   Porcentaje == dTO.Porcentaje;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(herramientaId, nombre, material, fabricante, precio, precioOferta, Porcentaje);
        }
    }
}
