namespace AppForSEII2526.API.DTOs
{
    public class OfertaDetalleDTO
    {
        public int Id { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaFin { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaOferta { get; set; }

        public tiposMetodoPago metodoPago { get; set; }

        public tiposDirigidaOferta tiposDirigidaOferta { get; set; }

        public IList<OfertaItemDTO> HerramientasAOfertar { get; set; }

        public OfertaDetalleDTO()
        {
        }

        public OfertaDetalleDTO(int id, DateTime fechaInicio, DateTime fechaFin, DateTime fechaOferta, tiposMetodoPago metodoPago, tiposDirigidaOferta tiposDirigidaOferta, IList<OfertaItemDTO> herramientasAOfertar)
        {
            Id = id;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.fechaOferta = fechaOferta;
            this.metodoPago = metodoPago;
            this.tiposDirigidaOferta = tiposDirigidaOferta;
            HerramientasAOfertar = herramientasAOfertar;
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaDetalleDTO dTO &&
                   Id == dTO.Id &&
                   fechaInicio == dTO.fechaInicio &&
                   fechaFin == dTO.fechaFin &&
                   fechaOferta == dTO.fechaOferta &&
                   metodoPago == dTO.metodoPago &&
                   tiposDirigidaOferta == dTO.tiposDirigidaOferta;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, fechaInicio, fechaFin, fechaOferta, metodoPago, tiposDirigidaOferta, HerramientasAOfertar);
        }
    }
}
