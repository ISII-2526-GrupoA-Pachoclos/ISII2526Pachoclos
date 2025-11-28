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
        [ProducesResponseType(typeof(OfertaDetalleDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDetallesOferta(int id)
        {
            // Cargar la entidad con sus relaciones
            var ofertaEntity = await _context.Oferta
                .Include(o => o.ApplicationUser)
                .Include(o => o.ofertaItems)
                    .ThenInclude(oi => oi.herramienta)
                        .ThenInclude(h => h.fabricante)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (ofertaEntity == null)
                return NotFound();


            // Mapear en memoria de forma defensiva para evitar NullReference en tests
            var ofertaDto = new OfertaDetalleDTO(
                ofertaEntity.fechaInicio,
                ofertaEntity.fechaFin,
                ofertaEntity.fechaOferta,
                ofertaEntity.ApplicationUser?.nombre ?? string.Empty,
                ofertaEntity.metodoPago,
                ofertaEntity.paraSocio,
                ofertaEntity.ofertaItems?
                    .Select(oi => new OfertaItemDTO
                    {
                        herramientaId = oi.herramientaid,
                        nombre = oi.herramienta?.nombre ?? string.Empty,
                        material = oi.herramienta?.material ?? string.Empty,
                        fabricante = oi.herramienta?.fabricante?.nombre ?? string.Empty,
                        precio = oi.herramienta?.precio ?? 0f,
                        precioOferta = (oi.herramienta?.precio ?? 0f) * (100f - oi.porcentaje) / 100f,
                        Porcentaje = oi.porcentaje
                    })
                    .ToList() ?? new List<OfertaItemDTO>()
            );

            return Ok(ofertaDto);
        }



        [HttpPost]
        [ProducesResponseType(typeof(OfertaDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearOferta(CrearOfertaDTO crearOfertaDTO)
        {
            // Validaciones de fechas
            if (crearOfertaDTO.FechaInicio < DateTime.Today)
                ModelState.AddModelError("FechaInicio", "La fecha de inicio debe ser posterior o igual a la fecha actual.");

            if (crearOfertaDTO.FechaInicio >= crearOfertaDTO.FechaFin)
                ModelState.AddModelError("FechaFin", "La fecha de fin debe ser posterior a la fecha de inicio.");

            if (crearOfertaDTO.OfertaItem == null || crearOfertaDTO.OfertaItem.Count == 0)
                ModelState.AddModelError("CrearOfertaItem", "Debe agregar al menos una herramienta a la oferta.");


            //Modificación Examen Sprint 2
            if (crearOfertaDTO.FechaFin < crearOfertaDTO.FechaInicio.AddDays(7))
                ModelState.AddModelError("FechaFin", "¡Error, la oferta debe durar al menos 1 semana!");


            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            

            // Obtener usuario (en un caso real, esto vendría del contexto de autenticación)
            var usuario = await _context.ApplicationUser.FirstOrDefaultAsync();
            if (usuario == null)
            {
                ModelState.AddModelError("Usuario", "No se encontró un usuario válido.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var herramientaIds = crearOfertaDTO.OfertaItem
                .Select(i => i.herramientaId)
                .Distinct()
                .ToList();

            var herramientasEnDB = await _context.Herramienta
                .Include(h => h.fabricante)
                .Where(h => herramientaIds.Contains(h.id))
                .ToDictionaryAsync(h => h.id);

            // Validar que todas las herramientas existen
            foreach (var itemDTO in crearOfertaDTO.OfertaItem)
            {
                if (!herramientasEnDB.ContainsKey(itemDTO.herramientaId))
                {
                    ModelState.AddModelError("CrearOfertaItem", $"La herramienta con id {itemDTO.herramientaId} no existe.");
                }
                else if (itemDTO.Porcentaje <= 0 || itemDTO.Porcentaje > 100)
                {
                    var herramienta = herramientasEnDB[itemDTO.herramientaId];
                    ModelState.AddModelError("CrearOfertaItem", $"El porcentaje {itemDTO.Porcentaje}% de '{herramienta.nombre}' debe estar entre 1 y 100.");
                }
            }

            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));

            // Crear la oferta
            var ofertaNueva = new Oferta
            {
                fechaInicio = crearOfertaDTO.FechaInicio,
                fechaFin = crearOfertaDTO.FechaFin,
                fechaOferta = DateTime.Today,
                metodoPago = crearOfertaDTO.TiposMetodoPago,
                paraSocio = crearOfertaDTO.TiposDirigidaOferta,
                ofertaItems = new List<OfertaItem>(),
                ApplicationUser = usuario
            };

            // Crear items de oferta
            foreach (var itemDTO in crearOfertaDTO.OfertaItem)
            {
                var herramienta = herramientasEnDB[itemDTO.herramientaId];
                float precioFinal = herramienta.precio * (1 - (itemDTO.Porcentaje / 100.0f));

                var nuevoItem = new OfertaItem
                {
                    porcentaje = itemDTO.Porcentaje,
                    precioFinal = precioFinal,
                    oferta = ofertaNueva,
                    herramienta = herramienta,
                    herramientaid = herramienta.id
                };

                ofertaNueva.ofertaItems.Add(nuevoItem);
            }

            _context.Oferta.Add(ofertaNueva);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la oferta");
                return Conflict($"Error al guardar la oferta: {ex.Message}");
            }

            // Crear DTO de respuesta
            var ofertaDetail = new OfertaDetalleDTO(
                ofertaNueva.fechaInicio,
                ofertaNueva.fechaFin,
                ofertaNueva.fechaOferta,
                ofertaNueva.ApplicationUser?.nombre ?? string.Empty,
                ofertaNueva.metodoPago,
                ofertaNueva.paraSocio,
                ofertaNueva.ofertaItems.Select(oi => new OfertaItemDTO
                {
                    herramientaId = oi.herramientaid,
                    nombre = oi.herramienta.nombre,
                    material = oi.herramienta.material,
                    fabricante = oi.herramienta.fabricante.nombre,
                    precio = oi.herramienta.precio,
                    precioOferta = oi.precioFinal,
                    Porcentaje = oi.porcentaje
                }).ToList()
            );

            return CreatedAtAction(
                nameof(GetDetallesOferta),
                new { id = ofertaNueva.Id },
                ofertaDetail
            );
        }

    }
}