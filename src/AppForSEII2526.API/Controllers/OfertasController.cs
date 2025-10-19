using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [ProducesResponseType(typeof(IList<OfertasDTO>) , (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDetallesParaOfertas_conTodosLosDatos_DTO()
        {
            var ofertas = await _context.Ofertas
                
                .ToListAsync();
            return Ok(ofertas);
        }
    }
}
