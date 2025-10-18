namespace AppForSEII2526.API.DTOs
{
    public class OfertasDTO
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

        public string tiposMetodoPago { get; set; }

        public string tiposDirigidaOferta { get; set; }

        public List<HerramientasParaOfertasDTO> herramienta { get; set; }

        public OfertasDTO()
        {
        }

        public OfertasDTO(int id, DateTime fechaInicio, DateTime fechaFin, DateTime fechaOferta, string tiposMetodoPago, string tiposDirigidaOferta, List<HerramientasParaOfertasDTO> herramienta)
        {
            Id = id;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.fechaOferta = fechaOferta;
            this.tiposMetodoPago = tiposMetodoPago;
            this.tiposDirigidaOferta = tiposDirigidaOferta;
            this.herramienta = herramienta;
        }
    }
}
