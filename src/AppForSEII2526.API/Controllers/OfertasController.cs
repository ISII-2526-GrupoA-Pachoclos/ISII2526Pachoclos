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
        public async Task<IActionResult> GetDetalles_Oferta(int id)
        {

            if (_context.Oferta == null)
            {
                return NotFound();
            }

            var ofertas = await _context.Oferta
                .Include(o => o.metodoPago)
                .Include(o => o.ofertaItems)
                    .ThenInclude(oi => oi.herramienta)
                        .ThenInclude(h => h.fabricante)
                .Where(o => o.Id == id)
                .ToListAsync();


            var ofertaDTO = ofertas.Select(o => new OfertaDetalleDTO(
                o.fechaInicio,
                o.fechaFin,
                o.fechaOferta,
                o.paraSocio,
                o.metodoPago,
                o.ofertaItems.Select(oi => new OfertaItemDTO(
                    oi.herramienta.nombre,
                    oi.herramienta.material,
                    oi.herramienta.fabricante.nombre,
                    oi.herramienta.precio,
                    oi.herramienta.precio * (100f - oi.porcentaje) / 100
                )).ToList()
            )).FirstOrDefault();

            if (ofertaDTO == null)
            {
                return NotFound();
            }

            return Ok(ofertaDTO);
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(OfertaDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> CrearOferta([FromBody] )
    }
}
