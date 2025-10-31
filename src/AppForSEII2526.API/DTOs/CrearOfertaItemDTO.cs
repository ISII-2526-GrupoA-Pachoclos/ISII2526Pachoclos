namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaItemDTO
    {
        public CrearOfertaItemDTO(int id, string nombre, string material, float precio, string fabricante, float precioFinal)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            PrecioFinal = precioFinal;
        }

        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Material { get; set; }
        public float Precio { get; set; }
        public string Fabricante { get; set; }
        public float PrecioFinal { get; set; }

    }
}
