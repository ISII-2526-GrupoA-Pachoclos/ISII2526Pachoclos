using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class OfertasStateContainer
    {
        public CrearOfertaDTO Oferta { get; private set; } = new CrearOfertaDTO()
        {
            OfertaItem = new List<OfertaItemDTO>()
        };

        public float PrecioFinal
        {
            get
            {
                return Oferta.OfertaItem.Sum(item => item.Precio * (100 - item.Porcentaje) / 100);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddHerramientaToOferta(HerramientasParaOfertasDTO herramienta)
        {
            if (!Oferta.OfertaItem.Any(item => item.HerramientaId == herramienta.Id))
            {
                Oferta.OfertaItem.Add(new OfertaItemDTO()
                {
                    HerramientaId = herramienta.Id,
                    Nombre = herramienta.Nombre,
                    Material = herramienta.Material,
                    Fabricante = herramienta.Fabricante,
                    Precio = (float)herramienta.Precio,
                    Porcentaje = 1
                });
            }
        }

        public void EliminarItemToOferta(OfertaItemDTO item)
        {
            Oferta.OfertaItem.Remove(item);
        }

        public void ClearCarritoOferta()
        {
            Oferta.OfertaItem.Clear();
        }

        public void OfertaProcesada()
        {
            Oferta = new CrearOfertaDTO()
            {
                OfertaItem = new List<OfertaItemDTO>()
            };
        }
    }
}
