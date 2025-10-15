using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppForSEII2526.API.DTOs;

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

        /*
        // Devuelve toda la informacion directamente de la BBDD
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientas()
        {
            IList<Herramienta> herramientas = await _context.Herramienta.ToArrayAsync();
            return Ok(herramientas);
        }
        */


        // Devuelve toda la informacion de la herramienta que se piden en el paso 2 del CU (HerramientaParaRepararDTO)
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaRepararDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientas_conTodosLosDatos_DTO(string? filtroNombre, string? filtroTiempoReparacion)
        {
            var herramientas = await _context.Herramienta
                .Include(herramienta => herramienta.fabricante)
                .Where(h => h.nombre.Contains(filtroNombre) || (filtroNombre == null)
                 && (h.tiempoReparacion.Equals(filtroTiempoReparacion) || filtroTiempoReparacion == null)
                ).OrderBy(herramienta => herramienta.fabricante.nombre)
                .Select(h => new HerramientasParaRepararDTO (h.id, h.material, h.nombre, 
                    h.precio, h.tiempoReparacion, h.fabricante.nombre))
                .ToArrayAsync();
            return Ok(herramientas);
        }


    }
}
