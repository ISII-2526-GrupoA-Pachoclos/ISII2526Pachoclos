using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Linq;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OfertasController> _logger;

        public OfertasController(ApplicationDbContext context, ILogger<OfertasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<OfertaDetalleDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDetalles_Oferta(int id)
        {
            var ofertas = await _context.Oferta
                .Where(o => o.Id == id)
                .Include(o => o.ApplicationUser)
                .Include(o => o.ofertaItems)
                    .ThenInclude(oi => oi.herramienta)
                        .ThenInclude(h => h.fabricante)
                .Select(o => new OfertaDetalleDTO(
                    o.Id,
                    o.fechaInicio,
                    o.fechaFin,
                    o.fechaOferta,
                    o.metodoPago,
                    o.paraSocio,
                    o.ofertaItems.Select(oi => new OfertaItemDTO(
                        oi.herramienta.id,
                        oi.herramienta.nombre,
                        oi.herramienta.material,
                        oi.herramienta.fabricante.nombre,
                        oi.herramienta.precio,
                        oi.herramienta.precio * (100f - oi.porcentaje) / 100
                    )).ToList()
                ))
                .FirstOrDefaultAsync();

            
            if (ofertas == null)
                return NotFound();

            var precioTotal = ofertas.HerramientasAOfertar?.Sum(i => i.precioOferta) ?? 0f;

            return Ok(new { detalle = ofertas, precioTotal });
            
            
        }



        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearOferta(CrearOfertaDTO crearofertas)
        {
            if (crearofertas.FechaInicio <= DateTime.Today)
                ModelState.AddModelError("fechaInicio", "La fecha de inicio debe ser posterior a la fecha actual.");

            if (crearofertas.FechaInicio >= crearofertas.FechaFin)
                ModelState.AddModelError("fechaFin", "La fecha de fin debe ser posterior a la fecha de inicio.");

            if (crearofertas.CrearOfertaItem.Count == 0)
                ModelState.AddModelError("CrearHerramientasAOfertar", "Debe agregar al menos una herramienta a la oferta.");

            if (crearofertas.FechaInicio == DateTime.MinValue)
            {
                ModelState.AddModelError("FechaInicio", "Tiene que introducir una fecha de inicio");
            }

            if (crearofertas.FechaFin == DateTime.MinValue)
            {
                ModelState.AddModelError("FechaFin", "Tiene que introducir una fecha de fin");
            }

            if(crearofertas.TiposMetodoPago == null)
            {
                ModelState.AddModelError("MetodoPago", "Tiene que seleccionar un metodo de pago");
            }

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            // prefer Ids
            var herramientaIds = crearofertas.CrearOfertaItem.Select(i => i.Id).ToList();
            var herramientas = _context.Herramienta
                .Include(h => h.fabricante)
                .Where(h => herramientaIds.Contains(h.id))
                .ToList();

            Oferta oferta = new Oferta
            {
                fechaInicio = crearofertas.FechaInicio,
                fechaFin = crearofertas.FechaFin,
                fechaOferta = DateTime.Now,
                metodoPago = crearofertas.TiposMetodoPago,
                paraSocio = crearofertas.TiposDirigidaOferta,
                ofertaItems = new List<OfertaItem>()
            };

            foreach (var herramientaItem in crearofertas.CrearOfertaItem)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.id == herramientaItem.Id);
                if (herramienta == null)
                {
                    ModelState.AddModelError("CrearHerramientasAOfertar", $"Herramienta '{herramientaItem.Nombre}' (Id {herramientaItem.Id}) no encontrada.");
                    continue;
                }

                if (crearofertas.Porcentaje < 0 || crearofertas.Porcentaje > 100)
                {
                    ModelState.AddModelError("porcentaje", "El porcentaje debe estar entre 0 y 100.");
                    break;
                }

                float precioFinal = herramienta.precio * (1 - (crearofertas.Porcentaje / 100f));
                oferta.ofertaItems.Add(new OfertaItem { porcentaje = crearofertas.Porcentaje, precioFinal = precioFinal, oferta = oferta, herramienta = herramienta});
            }
            if (ModelState.ErrorCount > 0) return BadRequest(new ValidationProblemDetails(ModelState));
            _context.Oferta.Add(oferta);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Oferta", $"Hubo un error al guardar la oferta, por favor, intente de nuevo.");
                return Conflict("Error" + ex.Message);
            }

            var ofertaCreada = new OfertaDetalleDTO(
                oferta.Id,
                oferta.fechaInicio,
                oferta.fechaFin,
                oferta.fechaOferta,
                oferta.metodoPago,
                oferta.paraSocio,
                oferta.ofertaItems.Select(oi => new OfertaItemDTO(
                    oi.herramienta.id,
                    oi.herramienta.nombre,
                    oi.herramienta.material,
                    oi.herramienta.fabricante.nombre,
                    oi.herramienta.precio,
                    oi.precioFinal
                )).ToList()
            );

            return CreatedAtAction(nameof(GetDetalles_Oferta), new { id = oferta.Id }, ofertaCreada);
        }
    }
}
