namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaDTO
    {
        public CrearOfertaDTO(DateTime fechaInicio, DateTime fechaFin, int porcentaje, tiposMetodoPago tiposMetodoPago, tiposDirigidaOferta tiposDirigidaOferta, IList<CrearOfertaItemDTO> crearOfertaItem)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Porcentaje = porcentaje;
            TiposMetodoPago = tiposMetodoPago;
            TiposDirigidaOferta = tiposDirigidaOferta;
            CrearOfertaItem = crearOfertaItem;
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int Porcentaje { get; set; }

        public tiposMetodoPago TiposMetodoPago { get; set; }

        public tiposDirigidaOferta TiposDirigidaOferta { get; set; }
        public IList<CrearOfertaItemDTO> CrearOfertaItem { get; set; }
    }
}
