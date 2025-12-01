using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    public class FabricantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        public FabricantesController(ApplicationDbContext context, ILogger<FabricantesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //GET: /api/Fabricantes
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetFabricantes(string? fabricanteNombre)
        {
            IList<string> fabricantes = await _context.fabricante
                .Where(f => (fabricanteNombre == null || f.nombre.Contains(fabricanteNombre)))
                .OrderBy(f => f.nombre)
                .Select(f => f.nombre)
                .ToListAsync();

            return Ok(fabricantes);
        }
    }
}
