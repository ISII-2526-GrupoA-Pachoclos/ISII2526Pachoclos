using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        // GET: api/Ofertas/GetDetallesParaOfertas_conTodosLosDatos_DTO
        // Devuelve una entrada por cada OfertaItem (oferta + herramienta)
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IEnumerable<OfertasDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OfertasDTO>>> GetDetallesParaOfertas_conTodosLosDatos_DTO()
        {
            var items = await _context.OfertaItem
                .Include(oi => oi.oferta)
                .Include(oi => oi.herramienta!)
                    .ThenInclude(h => h.fabricante)
                .Select(oi => new OfertasDTO
                {
                    Id = oi.oferta.Id,
                    fechaInicio = oi.oferta.fechaInicio,
                    fechaFin = oi.oferta.fechaFin,
                    fechaOferta = oi.oferta.fechaOferta,
                    tiposMetodoPago = oi.oferta.metodoPago,
                    tiposDirigidaOferta = oi.oferta.paraSocio,
                    nombreHerramienta = oi.herramienta != null ? oi.herramienta.nombre : string.Empty,
                    materialHerramienta = oi.herramienta != null ? oi.herramienta.material : string.Empty,
                    fabricanteHerramienta = (oi.herramienta != null && oi.herramienta.fabricante != null) ? oi.herramienta.fabricante.nombre : string.Empty,
                    precioOriginalHerramienta = oi.herramienta != null ? oi.herramienta.precio : 0f,
                    precioConOfertaHerramienta = oi.precioFinal
                })
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/Ofertas/detalle/{id}  -> devuelve los items para la oferta con id dado
        [HttpGet("detalle/{id}")]
        [ProducesResponseType(typeof(IEnumerable<OfertasDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<OfertasDTO>>> GetDetallePorOferta(int id)
        {
            var exists = await _context.Oferta.AnyAsync(o => o.Id == id);
            if (!exists) return NotFound();

            var items = await _context.OfertaItem
                .Where(oi => oi.ofertaId == id)
                .Include(oi => oi.oferta)
                .Include(oi => oi.herramienta!)
                    .ThenInclude(h => h.fabricante)
                .Select(oi => new OfertasDTO
                {
                    Id = oi.oferta.Id,
                    fechaInicio = oi.oferta.fechaInicio,
                    fechaFin = oi.oferta.fechaFin,
                    fechaOferta = oi.oferta.fechaOferta,
                    tiposMetodoPago = oi.oferta.metodoPago,
                    tiposDirigidaOferta = oi.oferta.paraSocio,
                    nombreHerramienta = oi.herramienta != null ? oi.herramienta.nombre : string.Empty,
                    materialHerramienta = oi.herramienta != null ? oi.herramienta.material : string.Empty,
                    fabricanteHerramienta = (oi.herramienta != null && oi.herramienta.fabricante != null) ? oi.herramienta.fabricante.nombre : string.Empty,
                    precioOriginalHerramienta = oi.herramienta != null ? oi.herramienta.precio : 0f,
                    precioConOfertaHerramienta = oi.precioFinal
                })
                .ToListAsync();

            return Ok(items);
        }
    }
}
