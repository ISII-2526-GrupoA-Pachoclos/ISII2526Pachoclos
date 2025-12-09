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
        private readonly ILogger<ComprasController> _logger;

        public ComprasController(ApplicationDbContext context, ILogger<ComprasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetalleDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDetallesCompra(int id)
        {

            var compra = await _context.Compra
                .Where(c => c.Id == id)
                .Include(c => c.ApplicationUser)
                .Include(c => c.CompraItems)
                    .ThenInclude(c => c.herramienta)
                .Select(c => new CompraDetalleDTO(

                    c.Id,
                    c.ApplicationUser.nombre,
                    c.ApplicationUser.apellido,
                    c.direccionEnvio,
                    c.fechaCompra,
                    c.precioTotal,
                    c.CompraItems.Select(ci => new CompraItemDTO(
                        ci.herramientaid,
                        ci.cantidad,
                        ci.herramienta.nombre,
                        ci.herramienta.material,
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


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompraDetalleDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearCompra(CrearCompraDTO Crearcompra)
        {

            if (Crearcompra.HerramientasCompradas.Count == 0)
            {
                ModelState.AddModelError("CompraItem", "Error! Debes incluir al menos una herramienta ");
            }
            else 
            { 
                foreach (var item in Crearcompra.HerramientasCompradas)
                {
                    if (item.descripcion == null) { 

                        item.descripcion = "";

                    }
                    if (item.cantidad <= 0)
                    {
                        ModelState.AddModelError("Cantidad", "Error! La cantidad debe ser mayor que 0");
                    }

                    if (item.cantidad == 3 && item.descripcion == "") {

                        ModelState.AddModelError("Cantidad", "Error! Estas comprando demasiadas herramientas sin descripcion");


                    }
                }


            }


                var user = _context.ApplicationUser.FirstOrDefault(au => au.nombre == Crearcompra.Nombre);

            if (user == null)
                ModelState.AddModelError("ApplicationUser", "Error! Usuario no registrado");



            

            var nombreHerramientas = Crearcompra.HerramientasCompradas.Select(ci => ci.nombre).ToList<string>();

            var Herramientas = _context.Herramienta.Include(c => c.ComprarItems)
                .ThenInclude(ci => ci.compra)
                .Where(h => nombreHerramientas.Contains(h.nombre))

                .Select(h => new {
                    h.id,
                    h.nombre,
                    h.material,
                    h.precio
                })
                .ToList();

           

            

            var ComprasItems = new List<ComprarItem>();

            Compra compra = new Compra(Crearcompra.direccionEnvio, DateTime.Today, 0, Crearcompra.metodoPago, ComprasItems, user);

            var herramientasAux = await _context.Herramienta
                .Where(h => nombreHerramientas.Contains(h.nombre)) // todas las herramientas que esten en los ids anteriores
                .ToListAsync();




            foreach (var item in Crearcompra.HerramientasCompradas)
            {
                var herramienta = herramientasAux.FirstOrDefault(h => h.id == item.herramientaid);


                if (herramienta == null)
                {
                    ModelState.AddModelError("Herramienta", "Error! La herramienta con ese id no existe");
                    continue;
                }

                


                


                ComprarItem CompraItem = new ComprarItem(item.cantidad, item.descripcion, herramienta.precio, compra.Id, item.herramientaid, compra, herramienta);
                compra.CompraItems.Add(CompraItem);
                compra.precioTotal += herramienta.precio * item.cantidad;
            }
            

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Compra.Add(compra);

            try
            {
           
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Compra", $"Error! Ha habido un error al realizar tu compra, por favor, ten paciencia");
                return Conflict("Error" + ex.Message);

            }

            var compraDetalleDTO = new CompraDetalleDTO(compra.Id, user.nombre, user.apellido, compra.direccionEnvio, compra.fechaCompra, compra.precioTotal, Crearcompra.HerramientasCompradas);

            return CreatedAtAction("GetDetallesCompra", new { id = compra.Id }, compraDetalleDTO);










        }
        


    }
}
