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
            if(crearOfertaDTO.fechaInicio <= DateTime.Today) 
                ModelState.AddModelError("fechaInicio", "La fecha de inicio debe ser posterior a la fecha actual.");

            if(crearOfertaDTO.fechaInicio >= crearOfertaDTO.fechaFin)
                ModelState.AddModelError("fechaFin", "La fecha de fin debe ser posterior a la fecha de inicio.");

            if(crearOfertaDTO.CrearHerramientasAOfertar.Count == 0)
                ModelState.AddModelError("CrearHerramientasAOfertar", "Debe agregar al menos una herramienta a la oferta.");
        }
    }
}
