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

        public tiposMetodoPago tiposMetodoPago { get; set; }

        public tiposDirigidaOferta? tiposDirigidaOferta { get; set; }

        public string nombreHerramienta { get; set; }
        public string materialHerramienta { get; set; }
        public string fabricanteHerramienta { get; set; }
        public float precioOriginalHerramienta { get; set; }
        public float precioConOfertaHerramienta { get; set; }


        public OfertasDTO()
        {
        }

        public OfertasDTO(int id, DateTime fechaInicio, DateTime fechaFin, DateTime fechaOferta, tiposMetodoPago tiposMetodoPago, tiposDirigidaOferta? tiposDirigidaOferta, string nombreHerramienta, string materialHerramienta, string fabricanteHerramienta, float precioOriginalHerramienta, float precioConOfertaHerramienta)
        {
            Id = id;
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.fechaOferta = fechaOferta;
            this.tiposMetodoPago = tiposMetodoPago;
            this.tiposDirigidaOferta = tiposDirigidaOferta;
            this.nombreHerramienta = nombreHerramienta;
            this.materialHerramienta = materialHerramienta;
            this.fabricanteHerramienta = fabricanteHerramienta;
            this.precioOriginalHerramienta = precioOriginalHerramienta;
            this.precioConOfertaHerramienta = precioConOfertaHerramienta;
        }
    }
}
