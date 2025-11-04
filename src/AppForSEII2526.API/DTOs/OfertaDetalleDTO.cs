
namespace AppForSEII2526.API.DTOs
{
    public class OfertaDetalleDTO
    {

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        [Required, DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaOferta { get; set; }
        public tiposDirigidaOferta TiposDirigidaOferta { get; set; }

        public tiposMetodoPago MetodoPago { get; set; }

        public IList<OfertaItemDTO> HerramientasAOfertar { get; set; }

        public OfertaDetalleDTO()
        {
        }

        public OfertaDetalleDTO(DateTime fechaInicio, DateTime fechaFinal, DateTime fechaOferta, tiposDirigidaOferta tiposDirigidaOferta, tiposMetodoPago metodoPago, IList<OfertaItemDTO> herramientasAOfertar)
        {
            FechaInicio = fechaInicio;
            FechaFinal = fechaFinal;
            FechaOferta = fechaOferta;
            TiposDirigidaOferta = tiposDirigidaOferta;
            MetodoPago = metodoPago;
            HerramientasAOfertar = herramientasAOfertar;
        }

        public override bool Equals(object? obj)
        {
            return obj is OfertaDetalleDTO dTO &&
                   FechaInicio == dTO.FechaInicio &&
                   FechaFinal == dTO.FechaFinal &&
                   FechaOferta == dTO.FechaOferta &&
                   TiposDirigidaOferta == dTO.TiposDirigidaOferta &&
                   MetodoPago == dTO.MetodoPago &&
                   EqualityComparer<IList<OfertaItemDTO>>.Default.Equals(HerramientasAOfertar, dTO.HerramientasAOfertar);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FechaInicio, FechaFinal, FechaOferta, TiposDirigidaOferta, MetodoPago, HerramientasAOfertar);
        }
    }
}
