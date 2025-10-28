using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Security.Claims;

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
                .Include(o => o.ofertaItems)
                    .ThenInclude(oi => oi.herramienta)
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
        [ProducesResponseType(typeof(OfertaDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearOferta(CrearOfertaDTO crearOfertaDTO)
        {
            if (crearOfertaDTO.fechaInicio <= DateTime.Today)
                ModelState.AddModelError("fechaInicio", "La fecha de inicio debe ser posterior a la fecha actual.");

            if (crearOfertaDTO.fechaInicio >= crearOfertaDTO.fechaFin)
                ModelState.AddModelError("fechaFin", "La fecha de fin debe ser posterior a la fecha de inicio.");

            if (crearOfertaDTO.CrearHerramientasAOfertar.Count == 0)
                ModelState.AddModelError("CrearHerramientasAOfertar", "Debe agregar al menos una herramienta a la oferta.");
            
            
            var tipoDirigido = crearOfertaDTO.paraSocio ?? tiposDirigidaOferta.Clientes;

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            var herramientaIds = crearOfertaDTO.CrearHerramientasAOfertar
                .Select(i => i.herramientaId)
                .Distinct()
                .ToList();

            var herramientasEnDB = await _context.Herramienta
                .Include(h => h.fabricante)
                .Where(h => herramientaIds.Contains(h.id))
                .ToDictionaryAsync(h => h.id);

            var ofertaNueva = new Oferta
            {
                fechaInicio = crearOfertaDTO.fechaInicio,
                fechaFin = crearOfertaDTO.fechaFin,
                fechaOferta = DateTime.UtcNow,
                metodoPago = crearOfertaDTO.metodoPago,
                paraSocio = tipoDirigido,
                ofertaItems = new List<OfertaItem>()
            };


            foreach (var itemDTO in crearOfertaDTO.CrearHerramientasAOfertar)
            {
                if (!herramientasEnDB.TryGetValue(itemDTO.herramientaId, out var herramienta))
                {
                    ModelState.AddModelError(nameof(crearOfertaDTO.CrearHerramientasAOfertar), $"La herramienta con id {itemDTO.herramientaId} no existe.");
                    continue;
                }

                else if (itemDTO.porcentaje <= 0 || itemDTO.porcentaje > 100)
                {
                    ModelState.AddModelError(nameof(crearOfertaDTO.CrearHerramientasAOfertar), $"El porcentaje {itemDTO.porcentaje}% de '{herramienta.nombre}' no es correcto");
                    continue;
                }

                else
                {
                    float precioFinal = herramienta.precio * (1 - (itemDTO.porcentaje / 100.0f));
                    var nuevoItem = new OfertaItem(
                        herramienta.id,
                        itemDTO.porcentaje,
                        precioFinal,
                        herramienta
                    );
                    ofertaNueva.ofertaItems.Add(nuevoItem);

                }
            }

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            

            _context.Add(ofertaNueva);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("ErrorGuardar", "Ocurrió un error al guardar la oferta.");
                return Conflict("Error" + ex.Message);
            }


            var ofertaDetail = new OfertaDetalleDTO(
                ofertaNueva.Id,
                ofertaNueva.fechaInicio,
                ofertaNueva.fechaFin,
                ofertaNueva.fechaOferta,
                ofertaNueva.metodoPago,
                ofertaNueva.paraSocio,
                ofertaNueva.ofertaItems.Select(oi => new OfertaItemDTO(
                    oi.herramienta.id,
                    oi.herramienta.nombre,
                    oi.herramienta.material,
                    oi.herramienta.fabricante.nombre,
                    oi.herramienta.precio,
                    oi.precioFinal
                )).ToList()
            );

            return CreatedAtAction(
                nameof(GetDetalles_Oferta),
                new { id = ofertaNueva.Id },
                ofertaDetail
            );
        }
    }
}
