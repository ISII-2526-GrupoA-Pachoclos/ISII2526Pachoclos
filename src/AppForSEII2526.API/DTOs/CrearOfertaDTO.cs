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

        public tiposMetodoPago TiposMetodoPago { get; set; }

        public tiposDirigidaOferta TiposDirigidaOferta { get; set; }
        public IList<OfertaItemDTO> OfertaItem { get; set; }

        public CrearOfertaDTO()
        {
            OfertaItem = new List<OfertaItemDTO>();
        }

        public override bool Equals(object? obj)
        {
            return obj is CrearOfertaDTO dTO &&
                   FechaInicio == dTO.FechaInicio &&
                   FechaFin == dTO.FechaFin &&
                   TiposMetodoPago == dTO.TiposMetodoPago &&
                   TiposDirigidaOferta == dTO.TiposDirigidaOferta &&
                   OfertaItem.SequenceEqual(dTO.OfertaItem);

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FechaInicio, FechaFin, TiposMetodoPago, TiposDirigidaOferta, OfertaItem);
        }
    }
}

