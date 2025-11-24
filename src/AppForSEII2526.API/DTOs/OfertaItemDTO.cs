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

        public OfertaItemDTO()
        {
        }


        public OfertaItemDTO(string nombre, string material, string fabricante, float precio, float precioOferta)
        {
            this.nombre = nombre;
            this.material = material;
            this.fabricante = fabricante;
            this.precio = precio;
            this.precioOferta = precioOferta;
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItemDTO dTO &&
                   herramientaId == dTO.herramientaId &&
                   nombre == dTO.nombre &&
                   material == dTO.material &&
                   fabricante == dTO.fabricante &&
                   precio == dTO.precio &&
                   precioOferta == dTO.precioOferta;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(herramientaId, nombre, material, fabricante, precio, precioOferta);
        }
    }
}
