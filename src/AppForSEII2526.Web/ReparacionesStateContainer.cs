using AppForSEII2526.Web.API;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AppForSEII2526.Web
{
    public class ReparacionesStateContainer
    {
        public ReparacionParaCrearDTO Reparacion { get; private set; } = new ReparacionParaCrearDTO()
        {
            Herramientas = new List<ReparacionItemDTO>()
        };

        public double PrecioTotal
        {
            get
            {
                return Reparacion.Herramientas.Sum(h => h.Precio * h.Cantidad);
            }
            // De solo lectura, sin setter
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddHerramientaToReparar(HerramientasParaRepararDTO herramienta)
        {
            if (!Reparacion.Herramientas.Any(h => h.HerramientaId == herramienta.Id)) // evitar duplicados
            {
                Reparacion.Herramientas.Add(new ReparacionItemDTO()
                {
                    HerramientaId = herramienta.Id,
                    Precio = herramienta.Precio,
                    NombreHerramienta = herramienta.Nombre,
                    Descripcion = null, // opcional, se rellenará después
                    Cantidad = 1, // valor inicial por defecto
                    TiempoReparacion = herramienta.TiempoReparacion
                });
            }
        }

        public void UpdateCantidadHerramienta(int herramientaId, int nuevaCantidad)
        {
            var herr = Reparacion.Herramientas.FirstOrDefault(h => h.HerramientaId == herramientaId);
            if (herr != null && nuevaCantidad > 0)
            {
                herr.Cantidad = nuevaCantidad;
            }
        }
        
        public void UpdateDescripcionHerramienta(int herramientaId, string nuevaDescripcion)
        {
            var herr = Reparacion.Herramientas.FirstOrDefault(h => h.HerramientaId == herramientaId);
            if (herr != null)
            {
                herr.Descripcion = nuevaDescripcion;
                NotifyStateChanged();
            }
        }

        public void BorrarHerramientaParaReparar(ReparacionItemDTO item)
        {
            Reparacion.Herramientas.Remove(item);
        }

        public void BorrarCarritoReparacion()
        {
            Reparacion.Herramientas.Clear();
            // No es necesario poner Reparacion.PrecioTotal = 0,
            // ya que se calcula automáticamente cuando hacemos Herramientas.Clear()
            NotifyStateChanged();
        }

        public void ProcesarReparacion()
        {
            // Reiniciar el estado tras finalizar una reparación
            Reparacion = new ReparacionParaCrearDTO
            {
                Herramientas = new List<ReparacionItemDTO>()
            };
            NotifyStateChanged();
        }

        public bool EsCarritoVacio()
        {
            return !Reparacion.Herramientas.Any();
        }

        public int TotalHerramientas()
        {
            return Reparacion.Herramientas.Sum(ri => ri.Cantidad);
        }

        public int GetMaxTiempoReparacion()
        {
            if (!Reparacion.Herramientas.Any()) return 0;

            int maxDias = 0;
            foreach (var item in Reparacion.Herramientas)
            {
                if (!string.IsNullOrEmpty(item.TiempoReparacion))
                {
                    // Parsear "X dias" -> extraer número X
                    if (int.TryParse(item.TiempoReparacion.Split(' ')[0], out int dias))
                    {
                        if (dias > maxDias) maxDias = dias;
                    }
                }
            }
            return maxDias;
        }

        public DateTimeOffset GetFechaRecogidaEstimada()
        {
            if (Reparacion.FechaEntrega == default)
                return DateTime.Today;

            int maxDias = GetMaxTiempoReparacion();
            return Reparacion.FechaEntrega.AddDays(maxDias);
        }
    }
}