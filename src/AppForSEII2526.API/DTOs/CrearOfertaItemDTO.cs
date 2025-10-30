namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaItemDTO
    {
        public CrearOfertaItemDTO(int id, string nombre, string material, float precio, string fabricante, float precioFinal)
        {
            this.id = id;
            this.nombre = nombre;
            this.material = material;
            this.precio = precio;
            this.fabricante = fabricante;
            this.precioFinal = precioFinal;
        }

        public int id { get; set; }

        public string nombre { get; set; }
        public string material { get; set; }
        public float precio { get; set; }
        public string fabricante { get; set; }
        public float precioFinal { get; set; }

    }
}
