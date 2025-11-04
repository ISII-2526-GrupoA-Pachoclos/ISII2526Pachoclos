
namespace AppForSEII2526.API.DTOs
{
    public class OfertaItemDTO
    {
        public string Nombre { get; set; }
        public string Material { get; set; }
        public string Fabricante { get; set; }
        public float Precio { get; set; }
        public float PrecioOferta { get; set; }

        public OfertaItemDTO()
        {
        }

        public OfertaItemDTO(string nombre, string material, string fabricante, float precio, float precioOferta)
        {
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            PrecioOferta = precioOferta;
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItemDTO dTO &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio &&
                   PrecioOferta == dTO.PrecioOferta;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nombre, Material, Fabricante, Precio, PrecioOferta);
        }
    }
}
