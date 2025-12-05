using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
            _logger.LogInformation("ReparacionesController instantiated.");
        }

        // Obtiene el detalle de una reparación por su ID. Corresponde al paso
        // 7 del caso de uso "Reparar herramientas". -> GET del Detalle
        [HttpGet]
        [ProducesResponseType(typeof(ReparacionDetalleDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDetalles_Reparacion(int id)
        {
            _logger.LogInformation("Solicitando detalles de reparación con ID: {ReparacionId}", id);

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
                    r.precioTotal,
                    r.ReparacionItems.Select(i => new ReparacionItemDTO(
                        i.Herramientaid,
                        i.Herramienta.precio,
                        i.Herramienta.nombre,
                        i.descripcion,
                        i.cantidad,
                        i.Herramienta.tiempoReparacion
                    )).ToList()
                ))
                .FirstOrDefaultAsync();

            if (reparacion == null)
            {
                _logger.LogError($"Reparación no encontrada.");
                return NotFound();
            }

            _logger.LogInformation("Reparación con ID {ReparacionId} encontrada. Cliente: {ClienteNombre} {ClienteApellido}, Precio total: {PrecioTotal}",
               id, reparacion.nombre, reparacion.apellido, reparacion.precioTotal);

            return Ok(reparacion);
        }


        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ReparacionDetalleDTO), (int)HttpStatusCode.Created)] // creado con exito
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)] // error de validación de datos
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)] // error al guardar en la base de datos
        public async Task<ActionResult> CrearReparacion(ReparacionParaCrearDTO reparacionParaCrear)
        {
            // === Validación de reglas de negocio (Flujos Alternativos) ===

            // Flujo Alternativo 1: fecha de entrega <= hoy ??
            if (reparacionParaCrear.fechaEntrega.Date < DateTime.Today)
            {
                _logger.LogError("Validación fallida: La fecha de entrega debe ser igual o posterior a hoy. Fecha recibida: {FechaEntrega}",
                     reparacionParaCrear.fechaEntrega);
                ModelState.AddModelError("fechaEntrega", "La fecha de entrega debe ser igual o posterior a hoy.");
            }

            // Flujo Alternativo 3 y 5: al menos una herramienta y cantidad >= 1 (cantidad ya validada en el DTO)
            if (reparacionParaCrear.Herramientas == null || reparacionParaCrear.Herramientas.Count == 0)
            {
                _logger.LogError("Validación fallida: Debe incluir al menos una herramienta para reparar.");
                ModelState.AddModelError("Herramientas", "Debe incluir al menos una herramienta para reparar.");
            }
            else
            {
                _logger.LogInformation("Validando {CantidadHerramientas} herramientas para reparar.",
                    reparacionParaCrear.Herramientas.Count);

                // Validación extra (ya validad en el DTO): cantidad > 0
                foreach (var item in reparacionParaCrear.Herramientas)
                {
                    if (item.cantidad <= 0)
                    {
                        _logger.LogError("Validación fallida: La cantidad de la herramienta '{NombreHerramienta}' debe ser mayor que 0.",
                            item.nombreHerramienta);
                        ModelState.AddModelError("Herramientas", $"La cantidad de la herramienta " +
                            $"'{item.nombreHerramienta}' debe ser mayor que 0.");
                    }
                }
            }

            // Validación extra para el enum metodoPago
            if (!Enum.IsDefined(typeof(metodoPago), reparacionParaCrear.metodoPago))
            {
                _logger.LogError("Validación fallida: El método de pago '{MetodoPago}' no es válido.",
                    reparacionParaCrear.metodoPago);
                ModelState.AddModelError("metodoPago",
                    "El método de pago no es válido. Valores permitidos: 0 (Efectivo), 1 (TarjetaCredito), 2 (PayPal).");
            }

            // cliente en la base de datos (AspNetUsers) ??
            var cliente = await _context.ApplicationUser
                .FirstOrDefaultAsync(u => u.nombre == reparacionParaCrear.nombreC && 
                u.apellido == reparacionParaCrear.apellidos); // por nombre y apellidos

            if (cliente == null)
            {
                _logger.LogError("Validación fallida: El cliente '{NombreC} {Apellidos}' no está registrado en el sistema.",
                    reparacionParaCrear.nombreC, reparacionParaCrear.apellidos);
                ModelState.AddModelError("Cliente", "El cliente no está registrado en el sistema.");
            }

            _logger.LogInformation("Cliente '{NombreC} {Apellidos}' encontrado en la base de datos.",
                reparacionParaCrear.nombreC, reparacionParaCrear.apellidos);

            // === Validación de herramientas ===

            // ids herramientas
            var herramientaIds = reparacionParaCrear.Herramientas.Select(h => h.HerramientaId).ToList();

            // ver BD para obtener info herramientas solicitadas
            var herramientasDb = await _context.Herramienta
                .Where(h => herramientaIds.Contains(h.id))
                .Select(h => new
                {
                    h.id,
                    h.nombre,
                    h.precio,
                    h.tiempoReparacion // Ej: "10 dias"
                })
                .ToListAsync();

            // herramientas solicitadas existen ??
            foreach (var item in reparacionParaCrear.Herramientas)
            {
                // buscar herramienta en los resultados de la BD
                var dbTool = herramientasDb.FirstOrDefault(h => h.id == item.HerramientaId);
                
                if (dbTool == null)
                {
                    _logger.LogError("Validación fallida: La herramienta con ID {HerramientaId} no existe en la base de datos.",
                        item.HerramientaId);
                    ModelState.AddModelError("Herramientas", $"La herramienta con ID {item.HerramientaId} no existe.");
                }
                else
                {
                    // nombre enviado = nombre en BD ??
                    if (!string.Equals(dbTool.nombre, item.nombreHerramienta))
                    {
                        _logger.LogError("Validación fallida: El nombre de la herramienta '{NombreHerramienta}' no coincide con el ID {HerramientaId}. " +
                            "La herramienta con ese ID se llama '{NombreBD}'.",
                            item.nombreHerramienta, item.HerramientaId, dbTool.nombre);
                        ModelState.AddModelError("Herramientas",
                            $"El nombre de la herramienta '{item.nombreHerramienta}' no coincide con el ID {item.HerramientaId}. " +
                            $"La herramienta con ese ID se llama '{dbTool.nombre}'.");
                    }

                    _logger.LogInformation("Herramienta con ID {HerramientaId} y nombre '{NombreHerramienta}' validada correctamente.",
                        item.HerramientaId, item.nombreHerramienta);
                }
            }

            if (ModelState.ErrorCount > 0) // comprobar errores acumulados
            {
                _logger.LogError("Errores de validación encontrados. No se puede crear la reparación.");
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            // === Cálculo de la fecha de recogida ===

            // sumar el MÁXIMO tiempo de reparación entre todas las herramientas
            int maxDiasReparacion = 0;
            foreach (var item in reparacionParaCrear.Herramientas)
            {
                var dbTool = herramientasDb.First(h => h.id == item.HerramientaId);

                // Parsear "X dias" -> extraer número X
                if (int.TryParse(dbTool.tiempoReparacion.Split(' ')[0], out int dias))
                {
                    if (dias > maxDiasReparacion) maxDiasReparacion = dias;
                }
            }

            var fechaRecogida = reparacionParaCrear.fechaEntrega.AddDays(maxDiasReparacion);

            _logger.LogInformation("Fecha de recogida calculada: {FechaRecogida} (máximo tiempo de reparación: {MaxDias} días).",
                fechaRecogida, maxDiasReparacion);

            // === Creación de la entidad Reparacion y sus ítems ===

            // crear reparacion
            var reparacion = new Reparacion
            {
                fechaEntrega = reparacionParaCrear.fechaEntrega.Date, // sin hora
                fechaRecogida = fechaRecogida.Date, // sin hora
                metodoPago = reparacionParaCrear.metodoPago,
                ApplicationUser = cliente,
                precioTotal = 0, // a continuación se calcula
                ReparacionItems = new List<ReparacionItem>()
            };

            float precioTotal = 0;

            var herramientasAux = await _context.Herramienta
                .Where(h => herramientaIds.Contains(h.id)) // todas las herramientas que esten en los ids anteriores
                .ToListAsync();

            _logger.LogInformation("Creando ítems de reparación para {CantidadHerramientas} herramientas.",
                reparacionParaCrear.Herramientas.Count);

            // reparacionitem para cada herramienta
            foreach (var item in reparacionParaCrear.Herramientas)
            {
                var herramienta = herramientasAux.First(h => h.id == item.HerramientaId);

                var reparacionItem = new ReparacionItem
                {
                    Herramientaid = herramienta.id,
                    cantidad = item.cantidad,
                    descripcion = item.descripcion,
                    precio = herramienta.precio, // precio de la BD
                    Herramienta = herramienta,
                    Reparacion = reparacion
                };
                reparacion.ReparacionItems.Add(reparacionItem);
                precioTotal += herramienta.precio * item.cantidad;

                _logger.LogInformation("Ítem de reparación creado: Herramienta ID {HerramientaId}, Cantidad {Cantidad}, Precio unitario {PrecioUnitario}.",
                    herramienta.id, item.cantidad, herramienta.precio);
            }
            reparacion.precioTotal = precioTotal;

            _logger.LogInformation("Precio total de la reparación calculado: {PrecioTotal}.", precioTotal);

            _context.Reparacion.Add(reparacion); // marcado como entidad a insertar

            // === guardar cambios ===

            
            try
            {
                _logger.LogInformation("Guardando la reparación en la base de datos.");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la reparación.");
                return Conflict("Error al guardar la reparación. Por favor, inténtelo de nuevo más tarde.");
            }
            

            // === RESPUESTA ===

            // Construir el DTO de respuesta
            var reparacionDetalle = new ReparacionDetalleDTO(
                id: reparacion.id,
                nombre: reparacion.ApplicationUser.nombre,
                apellido: reparacion.ApplicationUser.apellido,
                fechaEntrega: reparacion.fechaEntrega,
                fechaRecogida: reparacion.fechaRecogida,
                precioTotal: reparacion.precioTotal,
                herramientasAReparar: reparacion.ReparacionItems.Select(ri => new ReparacionItemDTO(
                    herramientaId: ri.Herramientaid,
                    precio: ri.precio,
                    nombreHerramienta: ri.Herramienta.nombre,
                    descripcion: ri.descripcion,
                    cantidad: ri.cantidad,
                    tiempoReparacion: ri.Herramienta.tiempoReparacion
                )).ToList()
            );

            _logger.LogInformation("Reparación creada con éxito. ID: {ReparacionId}", reparacion.id);

            // 201 de exito
            return CreatedAtAction("GetDetalles_Reparacion", new { id = reparacion.id }, reparacionDetalle);
        }
    }
}