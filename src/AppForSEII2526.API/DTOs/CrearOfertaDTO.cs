namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaDTO
    {
        public CrearOfertaDTO(DateTime FechaInicio, DateTime FechaFin, int porcentaje, tiposMetodoPago tiposMetodoPago, tiposDirigidaOferta tiposDirigidaOferta, IList<CrearOfertaItemDTO> crearOfertaItemDTO)
        {
            this.fechaInicio = FechaInicio;
            this.fechaFin = FechaFin;
            this.porcentaje = porcentaje;
            this.metodoPago = tiposMetodoPago;
            this.tiposDirigidaOferta = tiposDirigidaOferta;
            this.crearOfertaItemDTO = crearOfertaItemDTO;
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaInicio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaFin { get; set; }

        [Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int porcentaje { get; set; }

        public tiposMetodoPago metodoPago { get; set; }

        public tiposDirigidaOferta tiposDirigidaOferta { get; set; }
        public IList<CrearOfertaItemDTO> crearOfertaItemDTO { get; set; }
    }
}
