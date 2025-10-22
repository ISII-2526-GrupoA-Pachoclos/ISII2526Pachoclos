using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HerramientasController> _logger;

        public ComprasController(ApplicationDbContext context, ILogger<HerramientasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetalleDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDetalles_Compra(int id)
        {

            var compra = await _context.Compra
                .Where(c => c.Id == id)
                .Include(c => c.ApplicationUser)
                .Include(c => c.CompraItems)
                    .ThenInclude(c => c.herramienta)
                .Select(c => new CompraDetalleDTO(
                    c.Id,
                    c.direccionEnvio,
                    c.fechaCompra,
                    c.precioTotal,
                    c.CompraItems.Select(ci => new CompraItemDTO(
                        ci.herramientaid,
                        ci.cantidad,
                        ci.herramienta.nombre,
                        ci.descripcion,
                        ci.herramienta.precio
                    )).ToList()

                 ))
                .FirstOrDefaultAsync();
            if (compra == null)
            {
                _logger.LogWarning("No se ha encontrado la compra con Id {Id}", id);
                return NotFound();
            }
            return Ok(compra);




        }
    }
}
