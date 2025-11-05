namespace AppForSEII2526.API.DTOs
{
    public class OfertaDetalleDTO
    {

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaFin { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaOferta { get; set; }
        public string nombreUsuario { get; set; }

        public tiposMetodoPago metodoPago { get; set; }

        public tiposDirigidaOferta tiposDirigidaOferta { get; set; }

        public IList<OfertaItemDTO> HerramientasAOfertar { get; set; }

        public OfertaDetalleDTO()
        {
        }

        public OfertaDetalleDTO(DateTime fechaInicio, DateTime fechaFin, DateTime fechaOferta, string nombreUsuario, tiposMetodoPago metodoPago, tiposDirigidaOferta tiposDirigidaOferta, IList<OfertaItemDTO> herramientasAOfertar)
        {
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.fechaOferta = fechaOferta;
            this.nombreUsuario = nombreUsuario;
            this.metodoPago = metodoPago;
            this.tiposDirigidaOferta = tiposDirigidaOferta;
            HerramientasAOfertar = herramientasAOfertar;
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaDetalleDTO dTO &&
                   fechaInicio == dTO.fechaInicio &&
                   fechaFin == dTO.fechaFin &&
                   fechaOferta == dTO.fechaOferta &&
                   nombreUsuario == dTO.nombreUsuario &&
                   metodoPago == dTO.metodoPago &&
                   tiposDirigidaOferta == dTO.tiposDirigidaOferta;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(fechaInicio, fechaFin, fechaOferta, nombreUsuario, metodoPago, tiposDirigidaOferta, HerramientasAOfertar);
        }
    }
}
