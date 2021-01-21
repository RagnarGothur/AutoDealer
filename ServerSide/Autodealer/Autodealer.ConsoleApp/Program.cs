using Autodealer.ConsoleApp.Doers;

using Microsoft.Xrm.Tooling.Connector;

using Serilog;
using Serilog.Events;

using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autodealer.ConsoleApp
{
    /// <summary>
    /// Точка входа консольного приложения
    /// </summary>
    public class Program
    {
        private static string[] connStringKeys = new[]
        {
            "AuthType",
            "RequireNewInstance",
            "Url",
            "AppId",
            "RedirectUri"
        };

        /// <summary>
        /// Точка входа консольного приложения
        /// </summary>
        /// <param name="args">Аргументы</param>
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = CreateSerilogLogger();
                Log.Logger.Debug("Starting up...");

                GetCredentials(args, out string username, out string password);
                var connString = GetConnString(username, password);
                var crmClient = new CrmServiceClient(connString);

                var tasks = new[] {
                    new CommunicationDoer(crmClient).DoAsync(),
                    new ContactDoer(crmClient).DoAsync()
                };

                Log.Logger.Debug($"waiting for the {nameof(IDoer)} tasks");
                //блокирует
                Task.WaitAll(tasks);

                foreach (Task failedTask in tasks.Where(t => t.Exception != null))
                {
                    Log.Logger.Error(failedTask.Exception.Message);
                    Log.Logger.Debug(failedTask.Exception.StackTrace);

                    foreach(Exception inner in failedTask.Exception.InnerExceptions)
                    {
                        Log.Logger.Error(inner.Message);
                        Log.Logger.Debug(inner.StackTrace);
                    }
                }

            }
            catch (Exception e)
            {
                Log.Logger.Fatal(e.Message);
                Log.Logger.Debug(e.StackTrace);
            }
            finally
            {
                Log.Logger.Debug("Shutting down the program...");
                Console.ReadKey();
            }
        }

        private static void GetCredentials(string[] programArgs, out string username, out string password)
        {
            Log.Logger.Debug("Getting credentials...");
            if (programArgs.Length == 2)
            {
                Log.Logger.Debug("Got credentials from program attributes");
                username = programArgs[0];
                password = programArgs[1];
            }
            else
            {
                //TODO: придумать решение лучше
                do
                {
                    Log.Logger.Verbose("Reading console for username...");
                    Console.WriteLine("Username:");
                    username = Console.ReadLine();
                }
                while (String.IsNullOrEmpty(username));

                do
                {
                    Log.Logger.Verbose("Reading console for password...");
                    Console.WriteLine("Password:");
                    password = Console.ReadLine();
                }
                while (String.IsNullOrEmpty(password));

                Log.Logger.Debug("Got credentials from console");
            }
        }

        static string GetConnString(string username, string password)
        {
            Log.Logger.Debug("Getting connection string...");
            var connStringBuilder = new StringBuilder();

            connStringBuilder.Append($"Username={username};");
            connStringBuilder.Append($"Password={password};");

            try
            {
                Log.Logger.Debug("Getting app settings...");
                var appSettings = ConfigurationManager.AppSettings;
                foreach (string key in connStringKeys)
                {
                    var value = appSettings.Get(key);
                    if (value != null)
                    {
                        Log.Logger.Debug($"Got {key} setting");
                        connStringBuilder.Append($"{key}={value};");
                    }
                    else
                    {
                        Log.Logger.Warning($"{key} setting: not found");
                    }
                }
            }
            catch (ConfigurationErrorsException e)
            {
                Log.Logger.Error("Error while reading app settings");
                Log.Logger.Error(e.Message);
                Log.Logger.Debug(e.StackTrace);
            }

            return connStringBuilder.ToString();
        }

        private static ILogger CreateSerilogLogger()
        {
            const string path = @"..\..\..\..\..\log.txt";
            const string logTemplate = "[{Timestamp:yyyy:MM:dd:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}";

            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File(
                    path,
                    restrictedToMinimumLevel: LogEventLevel.Verbose,
                    outputTemplate: logTemplate,
                    rollingInterval: RollingInterval.Day
                )
                .WriteTo.Console(
                    restrictedToMinimumLevel: LogEventLevel.Verbose,
                    outputTemplate: logTemplate
                )
                .CreateLogger();
        }
    }
}
