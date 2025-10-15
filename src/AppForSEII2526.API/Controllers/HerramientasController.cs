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

        //Devuelve toda la información directamente de la BBDD
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramienta_sinDTOs()
        {
            IList<Herramienta> herramientas = await _context.Herramienta.ToListAsync();
            return Ok(herramientas);
        }

        //Devuelve todos los datos de las herramientas que se piden en el paso 2 del CU3 (Crear Ofertas)
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaOfertasDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientas_conTodosLosDatos_DTO(float? filtroPrecio, string? filtroFabricante)
        {
            var herramientas = await _context.Herramienta
                .Include(h => h.fabricante)
                .Where(h =>
                    (filtroPrecio == null || h.precio < filtroPrecio) &&
                    (filtroFabricante == null || h.fabricante.nombre == filtroFabricante)
                )
                .OrderBy(herramientas => herramientas.fabricante.nombre)
                .Select(h => new HerramientasParaOfertasDTO(h.id, h.material, h.nombre, h.precio, h.fabricante.nombre))
                .ToListAsync();

            return Ok(herramientas);
        }
    }
}
