namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaOferta { get; set; }
        public tiposDirigidaOferta TiposDirigidaOferta { get; set; }
        public tiposMetodoPago MetodoPago { get; set; }
        public IList<CrearOfertaItemDTO> HerramientasAOfertar { get; set; }
        public CrearOfertaDTO()
        {
            HerramientasAOfertar = new List<CrearOfertaItemDTO>();
        }
    }
}
