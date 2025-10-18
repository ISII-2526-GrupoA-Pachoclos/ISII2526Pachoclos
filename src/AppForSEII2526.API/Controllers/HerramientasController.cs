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
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllHerramientas()
        {
            IList<Herramienta> herramientas = await _context.Herramienta.ToListAsync();
            return Ok(herramientas);

        }
        

        /*
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientas_sinDTOs() { 
            IList<Herramienta> herramientas = await _context.Herramienta.ToListAsync();
            return Ok(herramientas);

        }
        */
        
        
        [HttpGet]
        [Route("[action]")]
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
        public async Task<ActionResult> GetHerramientasParaReparar_conTodosLosDatos_DTO(string? filtroNombre, string? filtroTiempoReparacion)
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



  /*
        //Devuelve toda la información directamente de la BBDD
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<Herramienta>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramienta_sinDTOs()
        {
            IList<Herramienta> herramientas = await _context.Herramienta.ToListAsync();
            return Ok(herramientas);
        }
  */

        //Devuelve todos los datos de las herramientas que se piden en el paso 2 del CU3 (Crear Ofertas)
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaOfertasDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaOferta_conTodosLosDatos_DTO(float? filtroPrecio, string? filtroFabricante)
        {
            var herramientas = await _context.Herramienta
                .Include(h => h.fabricante)
                .Where(h => (filtroPrecio == null || h.precio <= filtroPrecio) &&
                    (filtroFabricante == null || h.fabricante.nombre == filtroFabricante)
                )
                .OrderBy(herramientas => herramientas.fabricante.nombre)
                    .ThenBy(herramientas => herramientas.precio)
                .Select(h => new HerramientasParaOfertasDTO(h.id, h.material, h.nombre, h.precio, h.fabricante.nombre))
                .ToListAsync();

            return Ok(herramientas);
        }

    }
}
