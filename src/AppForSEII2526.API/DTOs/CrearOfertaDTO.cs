namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaDTO
    {
        public int Id { get; set; }

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

        public CrearOfertaDTO(int id, DateTime fechaInicio, DateTime fechaFin, tiposMetodoPago metodoPago, tiposDirigidaOferta? paraSocio, IList<CrearOfertaItemDTO> crearHerramientasAOfertar)
        {
            Id = id;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.metodoPago = metodoPago;
            this.paraSocio = paraSocio;
            CrearHerramientasAOfertar = crearHerramientasAOfertar;
        }

        public override bool Equals(object? obj)
        {
            return obj is CrearOfertaDTO dTO &&
                   Id == dTO.Id &&
                   fechaInicio == dTO.fechaInicio &&
                   fechaFin == dTO.fechaFin &&
                   metodoPago == dTO.metodoPago &&
                   paraSocio == dTO.paraSocio;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, fechaInicio, fechaFin, metodoPago, paraSocio, CrearHerramientasAOfertar);
        }
    }
}
