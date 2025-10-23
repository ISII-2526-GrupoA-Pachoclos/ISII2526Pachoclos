namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaItemDTO
    {
        public int herramientaId { get; set; }
        public string nombre { get; set; }
        public string material { get; set; }
        public float precio { get; set; }

        [Required, Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int porcentaje { get; set; }

        public CrearOfertaItemDTO()
        {
        }

        public CrearOfertaItemDTO(int herramientaId, string nombre, string material, float precio, int porcentaje)
        {
            this.herramientaId = herramientaId;
            this.nombre = nombre;
            this.material = material;
            this.precio = precio;
            this.porcentaje = porcentaje;
        }

        public override bool Equals(object? obj)
        {
            return obj is CrearOfertaItemDTO dTO &&
                   herramientaId == dTO.herramientaId &&
                   nombre == dTO.nombre &&
                   material == dTO.material &&
                   precio == dTO.precio &&
                   porcentaje == dTO.porcentaje;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(herramientaId, nombre, material, precio, porcentaje);
        }


    }
}
