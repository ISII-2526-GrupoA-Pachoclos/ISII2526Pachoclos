namespace ISII2526Pachoclos.LogViewer;

class Program
{
    static void Main(string[] args)
    {
        // Configuración de conexión
        const string hostName = "host.docker.internal";
        const int port = 5672;
        const string userName = "guest";
        const string password = "guest";
        const string exchangeName = "logs_topic";

        Console.WriteLine("=== LogViewer - Sistema de Monitoreo de Logs ===");
        Console.WriteLine();

        // Mostrar menú de opciones
        string routingKey = MostrarMenuYObtenerRoutingKey();

        Console.WriteLine("\nIniciando aplicación...");
        Console.WriteLine($"Filtrando logs por: {ObtenerDescripcionFiltro(routingKey)}");
        Console.WriteLine();

        using var subscriber = new Subscriber(hostName, port, userName, password, exchangeName, routingKey);

        try
        {
            subscriber.StartConsuming();

            Console.WriteLine("\nPresiona 'x' + ENTER para salir...");
            Console.WriteLine("Esperando logs...\n");

            // Bucle principal que verifica si se presiona 'x'
            while (true)
            {
                var input = Console.ReadLine();
                if (input?.ToLower() == "x")
                {
                    Console.WriteLine("Saliendo de LogViewer...");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($" [x] Error crítico: {ex.Message}");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
        }

        Console.WriteLine("LogViewer cerrado correctamente.");
    }

    private static string MostrarMenuYObtenerRoutingKey()
    {
        Console.WriteLine("Seleccione el tipo de logs que desea visualizar:");
        Console.WriteLine();
        Console.WriteLine("  1. Solo Information");
        Console.WriteLine("  2. Solo Error");
        Console.WriteLine();
        Console.Write("Ingrese su opción (1-2): ");

        string? opcion = Console.ReadLine();

        return opcion switch
        {
            "1" => "log.Information",
            "2" => "log.Error",
            _ => ObtenerRoutingKeyConValidacion()
        };
    }

    private static string ObtenerRoutingKeyConValidacion()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nOpción no válida. Intente nuevamente.");
        Console.ResetColor();
        Console.WriteLine();
        return MostrarMenuYObtenerRoutingKey();
    }

    private static string ObtenerDescripcionFiltro(string routingKey)
    {
        return routingKey switch
        {
            "log.Information" => "Solo logs de nivel Information",
            "log.Error" => "Solo logs de nivel Error",
            _ => $"Routing key personalizado: {routingKey}"
        };
    }
}