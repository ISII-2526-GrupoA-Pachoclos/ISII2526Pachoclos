using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerramientasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HerramientasController> _logger;

        public HerramientasController(ApplicationDbContext context, ILogger<HerramientasController> logger)
        {
            _context = context;
            _logger = logger;
        }


        [HttpGet]
        [Route("[accion]")]
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllHerramientas()
        {
            IList<Herramienta> herramientas = await _context.Herramienta.ToListAsync();
            return Ok(herramientas);

        }


        [HttpGet]
        [Route("[accion]")]
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientas_sinDTOs() { 
            IList<Herramienta> herramientas = await _context.Herramienta.ToListAsync();
            return Ok(herramientas);

        }
        [HttpGet]
        [Route("[accion]")]
        [ProducesResponseType(typeof(IList<HerramientasParaComprarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaComprar_conTodosLosDatos_DTO(float? filtroPrecio, string? filtroMaterial) {
            
            var herramientas = await _context.Herramienta
                .Include(h => h.fabricante)
                .Where(h => (h.precio <= filtroPrecio || (filtroPrecio==null))
                && (h.material.Contains(filtroMaterial) ||  (filtroMaterial==null))
                )
                .OrderBy(h => h.nombre)
                .Select(h => new HerramientasParaComprarDTO(h.id, h.nombre, h.material, h.fabricante.nombre, h.precio))
                .ToListAsync();
            return Ok(herramientas);

        }



    }
}
