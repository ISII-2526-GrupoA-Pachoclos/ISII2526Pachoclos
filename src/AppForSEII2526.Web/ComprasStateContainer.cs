using AppForSEII2526.Web.API;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace AppForSEII2526.Web
{
    public class ComprasStateContainer
    {

        public CrearCompraDTO Compra { get; private set; } = new CrearCompraDTO()
        {
            HerramientasCompradas = new List<CompraItemDTO>()
        };

        
        public decimal PrecioTotal
        {
            get
            {
                return Convert.ToDecimal(Compra.HerramientasCompradas.Sum(ri => ri.Precio * ri.Cantidad));

            }

        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddHerramientatoComprar(HerramientasParaComprarDTO herramienta) {
            if (!Compra.HerramientasCompradas.Any(ri => ri.Herramientaid == herramienta.Id))
                Compra.HerramientasCompradas.Add(new CompraItemDTO() {
                    Herramientaid = herramienta.Id,
                    Cantidad = 1,
                    Nombre = herramienta.Nombre,
                    Material= herramienta.Material,
                    Precio =herramienta.Precio

                }
                );
            



        }

        

        public void EliminarItemCompra(CompraItemDTO item) {
            Compra.HerramientasCompradas.Remove(item);
           

        }

        public void ClearCarritoCompra() { 

            Compra.HerramientasCompradas.Clear();
            


        }

        public void CompraProcesada() {

            Compra = new CrearCompraDTO()
            {
                HerramientasCompradas = new List<CompraItemDTO>()

            };
        
        
        }

    }
}
