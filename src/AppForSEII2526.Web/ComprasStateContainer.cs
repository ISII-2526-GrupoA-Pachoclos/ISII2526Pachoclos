using AppForSEII2526.Web.API;
namespace AppForSEII2526.Web
{
    public class ComprasStateContainer
    {
        public CrearCompraDTO Compra { get; private set; } = new CrearCompraDTO()
        {
            HerramientasCompradas = new List<CompraItemDTO>()
        };

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddHerramientatoComprar(HerramientasParaComprarDTO herramienta) {
            if (!Compra.HerramientasCompradas.Any(ri => ri.Herramientaid == herramienta.Id))
                Compra.HerramientasCompradas.Add(new CompraItemDTO() {
                    Herramientaid = herramienta.Id,
                    Cantidad = 1,
                    Nombre = herramienta.Nombre,
                    Precio=herramienta.Precio

                }
                );
            ComputePrecioTotal();



        }

        public void ComputePrecioTotal() { 
            
            
            NotifyStateChanged();

        }

        public void EliminarItemCompra(CompraItemDTO item) {
            Compra.HerramientasCompradas.Remove(item);
            ComputePrecioTotal();

        }

        public void ClearCarritoCompra() { 

            Compra.HerramientasCompradas.Clear();
            ComputePrecioTotal();


        }

        public void CompraProcesada() {

            Compra = new CrearCompraDTO()
            {
                HerramientasCompradas = new List<CompraItemDTO>()

            };
        
        
        }

    }
}
