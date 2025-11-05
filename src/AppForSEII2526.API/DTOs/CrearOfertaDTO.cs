namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaDTO
    {
        

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int Porcentaje { get; set; }
        public string nombre { get; set; }

        public tiposMetodoPago TiposMetodoPago { get; set; }

        public tiposDirigidaOferta TiposDirigidaOferta { get; set; }
        public List<CrearOfertaItemDTO> CrearOfertaItem { get; set; }

        public CrearOfertaDTO()
        {
            CrearOfertaItem = new List<CrearOfertaItemDTO>();
        }

        public override bool Equals(object? obj)
        {
            return obj is CrearOfertaDTO dTO &&
                   FechaInicio == dTO.FechaInicio &&
                   FechaFin == dTO.FechaFin &&
                   Porcentaje == dTO.Porcentaje &&
                   nombre == dTO.nombre &&
                   TiposMetodoPago == dTO.TiposMetodoPago &&
                   TiposDirigidaOferta == dTO.TiposDirigidaOferta &&
                   CrearOfertaItem.SequenceEqual(dTO.CrearOfertaItem);

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FechaInicio, FechaFin, Porcentaje, nombre, TiposMetodoPago, TiposDirigidaOferta, CrearOfertaItem);
        }
    }
}

