namespace ISII2526Pachoclos.LogViewer;

class Program
{
    static void Main(string[] args)
    {
        // Configuración de conexión
        const string hostName = "localhost";
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
        Console.WriteLine("  1. Todos los logs (*)");
        Console.WriteLine("  2. Solo Trace");
        Console.WriteLine("  3. Solo Debug");
        Console.WriteLine("  4. Solo Information");
        Console.WriteLine("  5. Solo Warning");
        Console.WriteLine("  6. Solo Error");
        Console.WriteLine("  7. Solo Critical");
        Console.WriteLine();
        Console.Write("Ingrese su opción (1-7): ");

        string? opcion = Console.ReadLine();

        return opcion switch
        {
            "1" => "*",
            "2" => "trace",
            "3" => "debug",
            "4" => "information",
            "5" => "warning",
            "6" => "error",
            "7" => "critical",
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
            "*" => "Todos los logs",
            "trace" => "Solo logs de nivel Trace",
            "debug" => "Solo logs de nivel Debug",
            "information" => "Information",
            "warning" => "Solo logs de nivel Warning",
            "error" => "Error",
            "critical" => "Solo logs de nivel Critical",
            _ => $"Routing key personalizado: {routingKey}"
        };
    }
}