using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ISII2526Pachoclos.LogViewer
{
    public class Subscriber : IDisposable
    {
        private readonly string _hostName;
        private readonly int _port;
        private readonly string _userName;
        private readonly string _password;
        private readonly string _exchangeName;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _routingKey;


        public Subscriber(string hostName, int port, string userName, string password, string exchangeName, string routingKey)
        {
            _hostName = hostName;
            _port = port;
            _userName = userName;
            _password = password;
            _exchangeName = exchangeName;
            _routingKey = routingKey;

            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                Port = _port,
                UserName = _userName,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void StartConsuming()
        {
            try
            {
                // 1. Declarar el exchange (debe coincidir con el del publicador)
                _channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic, durable: true);

                // 2. Declarar cola temporal/exclusiva
                var tempQueue = _channel.QueueDeclare(
                    queue: "", // nombre vacío = cola temporal
                    durable: false,
                    exclusive: true,
                    autoDelete: true);

                var queueName = tempQueue.QueueName;

                // 3. Vincular la cola al exchange
                _channel.QueueBind(
                    queue: queueName,
                    exchange: _exchangeName,
                    routingKey: _routingKey); // routing key vacía para fanout

                Console.WriteLine($" [*] Esperando logs en la cola: {queueName}");
                Console.WriteLine($" [*] Exchange: {_exchangeName}, Host: {_hostName}:{_port}");
                Console.WriteLine(" Presiona [Ctrl+C] para salir.");
                Console.WriteLine(new string('=', 60));

                // 4. Crear consumidor
                var consumer = new EventingBasicConsumer(_channel);

                // 5. Configurar callback
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        // Deserializar el mensaje JSON
                        var logEntry = JsonSerializer.Deserialize<LogEntry>(message);

                        if (logEntry != null)
                        {
                            DisplayLogEntry(logEntry);
                        }
                        else
                        {
                            Console.WriteLine($" [x] No se pudo deserializar el mensaje: {message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" [x] Error procesando mensaje: {ex.Message}");
                        Console.WriteLine($" [x] Mensaje raw: {Encoding.UTF8.GetString(ea.Body.ToArray())}");
                    }
                };

                // 6. Iniciar consumo
                _channel.BasicConsume(
                    queue: queueName,
                    autoAck: true,
                    consumer: consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" [x] Error inicializando el consumidor: {ex.Message}");
                throw;
            }
        }

        private void DisplayLogEntry(LogEntry logEntry)
        {
            var originalColor = Console.ForegroundColor;

            // Cambiar color según el nivel de log
            Console.ForegroundColor = logEntry.LogLevel switch
            {
                "Error" or "Critical" => ConsoleColor.Red,
                "Warning" => ConsoleColor.Yellow,
                "Information" => ConsoleColor.Green,
                "Debug" => ConsoleColor.Blue,
                "Trace" => ConsoleColor.Gray,
                _ => ConsoleColor.White
            };

            // Mostrar el log formateado
            Console.WriteLine($"[{logEntry.Timestamp:HH:mm:ss}] [{logEntry.LogLevel,-12}] {logEntry.Category}");
            Console.WriteLine($"   Mensaje: {logEntry.Message}");

            if (!string.IsNullOrEmpty(logEntry.Exception))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"   Excepción: {logEntry.Exception}");
                Console.ForegroundColor = logEntry.LogLevel switch
                {
                    "Error" or "Critical" => ConsoleColor.Red,
                    "Warning" => ConsoleColor.Yellow,
                    "Information" => ConsoleColor.Green,
                    "Debug" => ConsoleColor.Blue,
                    "Trace" => ConsoleColor.Gray,
                    _ => ConsoleColor.White
                };
            }

            Console.WriteLine(new string('-', 80));
            Console.ForegroundColor = originalColor;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}