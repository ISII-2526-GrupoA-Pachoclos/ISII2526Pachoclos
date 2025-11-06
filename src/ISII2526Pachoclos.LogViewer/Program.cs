namespace ISII2526Pachoclos.LogViewer;

class Program
{
    static void Main(string[] args)
    {
        // Configuración
        const string hostName = "localhost";
        const int port = 5672;
        const string userName = "guest";
        const string password = "guest";
        const string exchangeName = "logs";

        Console.WriteLine("=== LogViewer - Sistema de Monitoreo de Logs ===");
        Console.WriteLine("Iniciando aplicación...");

        using var subscriber = new Subscriber(hostName, port, userName, password, exchangeName);

        try
        {
            subscriber.StartConsuming();

            Console.WriteLine("\nPresiona 'x' para salir...");
            Console.WriteLine("Esperando logs...\n");

            // Bucle principal que verifica si se presiona 'x'
            while (true)
            {
                // Verificar si hay una tecla presionada sin bloquear
                if (Console.ReadLine().ToLower() == "x")
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
}

