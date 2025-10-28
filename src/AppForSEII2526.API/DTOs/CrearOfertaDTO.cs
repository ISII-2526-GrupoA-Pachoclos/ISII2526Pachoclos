namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaDTO
    {

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaFin { get; set; }

        public tiposMetodoPago metodoPago { get; set; }

        public tiposDirigidaOferta? paraSocio { get; set; }

        public IList<CrearOfertaItemDTO> CrearHerramientasAOfertar { get; set; }

        public CrearOfertaDTO()
        {
        }

        public CrearOfertaDTO(DateTime fechaInicio, DateTime fechaFin, tiposMetodoPago metodoPago, tiposDirigidaOferta? paraSocio, IList<CrearOfertaItemDTO> crearHerramientasAOfertar)
        {
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.metodoPago = metodoPago;
            this.paraSocio = paraSocio;
            CrearHerramientasAOfertar = crearHerramientasAOfertar;
        }

        public override bool Equals(object? obj)
        {
            return obj is CrearOfertaDTO dTO &&
                   fechaInicio == dTO.fechaInicio &&
                   fechaFin == dTO.fechaFin &&
                   metodoPago == dTO.metodoPago &&
                   paraSocio == dTO.paraSocio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(fechaInicio, fechaFin, metodoPago, paraSocio, CrearHerramientasAOfertar);
        }
    }
}
