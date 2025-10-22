using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReparacionesController> _logger;

        public ReparacionesController(ApplicationDbContext context, ILogger<ReparacionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Obtiene el detalle de una reparación por su ID. Corresponde al paso
        // 7 del caso de uso "Reparar herramientas". -> GET del Detalle
        [HttpGet]
        [ProducesResponseType(typeof(ReparacionDetalleDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDetalles_Reparacion(int id)
        {
            var reparacion = await _context.Reparacion
                .Where(r => r.id == id)
                .Include(r => r.ApplicationUser)           // Carga ApplicationUser
                .Include(r => r.ReparacionItems)             // Carga los items de la reparación
                    .ThenInclude(i => i.Herramienta) // Carga la herramienta asociada a cada item
                .Select(r => new ReparacionDetalleDTO(
                    r.id,
                    r.ApplicationUser.nombre,               // de ApplicationUser
                    r.ApplicationUser.apellido,             // de ApplicationUser
                    r.fechaEntrega,
                    r.fechaRecogida,
                    r.metodoPago,
                    r.precioTotal,
                    r.ReparacionItems.Select(i => new ReparacionItemDTO(
                        i.Herramientaid,
                        i.Herramienta.precio,
                        i.Herramienta.nombre,
                        i.descripcion,
                        i.cantidad
                    )).ToList()
                ))
                .FirstOrDefaultAsync();

            if (reparacion == null)
            {
                _logger.LogWarning($"Reparación no encontrada.");
                return NotFound();
            }

            return Ok(reparacion);
        }


    }
}
