namespace AppForSEII2526.API.DTOs
{
    public class CrearOfertaItemDTO
    {
        public int herramientaId { get; set; }

        [Required, Range(1, 100, ErrorMessage = "Establece un porcentaje entre 1 y 100")]
        public int porcentaje { get; set; }

        public CrearOfertaItemDTO()
        {
        }

        public CrearOfertaItemDTO(int herramientaId, int porcentaje)
        {
            this.herramientaId = herramientaId;
            this.porcentaje = porcentaje;
        }

        public override bool Equals(object? obj)
        {
            return obj is CrearOfertaItemDTO dTO &&
                   herramientaId == dTO.herramientaId &&
                   porcentaje == dTO.porcentaje;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(herramientaId, porcentaje);
        }


    }
}
