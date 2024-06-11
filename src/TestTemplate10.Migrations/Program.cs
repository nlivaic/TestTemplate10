using System;
using System.IO;
using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TestTemplate10.Migrations
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var connectionString = string.Empty;
            var dbUser = string.Empty;
            var dbPassword = string.Empty;
            var scriptsPath = string.Empty;
            var sqlUsersGroupName = string.Empty;

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? "Production";
            Console.WriteLine($"Environment: {env}.");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            InitializeParameters();
            var connectionStringBuilderTestTemplate10 = new SqlConnectionStringBuilder(connectionString);
            if (env.Equals("Development"))
            {
                connectionStringBuilderTestTemplate10.UserID = dbUser;
                connectionStringBuilderTestTemplate10.Password = dbPassword;
            }
            else
            {
                connectionStringBuilderTestTemplate10.UserID = dbUser;
                connectionStringBuilderTestTemplate10.Password = dbPassword;
                connectionStringBuilderTestTemplate10.Authentication = SqlAuthenticationMethod.ActiveDirectoryPassword;
            }
            var upgraderTestTemplate10 =
                DeployChanges.To
                    .SqlDatabase(connectionStringBuilderTestTemplate10.ConnectionString)
                    .WithVariable("SqlUsersGroupNameVariable", sqlUsersGroupName)
                    .WithScriptsFromFileSystem(
                        !string.IsNullOrWhiteSpace(scriptsPath)
                                ? Path.Combine(scriptsPath, "TestTemplate10Scripts")
                            : Path.Combine(Environment.CurrentDirectory, "TestTemplate10Scripts"))
                    .LogToConsole()
                    .Build();
            Console.WriteLine($"Now upgrading TestTemplate10.");
            if (env == "Development")
            {
                upgraderTestTemplate10.MarkAsExecuted("0000_AzureSqlContainedUser.sql");
            }
            var resultTestTemplate10 = upgraderTestTemplate10.PerformUpgrade();

            if (!resultTestTemplate10.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TestTemplate10 upgrade error: {resultTestTemplate10.Error}");
                Console.ResetColor();
                return -1;
            }

            // Uncomment the below sections if you also have an Identity Server project in the solution.
            /*
            var connectionStringTestTemplate10Identity = string.IsNullOrWhiteSpace(args.FirstOrDefault())
                ? config["ConnectionStrings:TestTemplate10IdentityDb"]
                : args.FirstOrDefault();

            var upgraderTestTemplate10Identity =
                DeployChanges.To
                    .SqlDatabase(connectionStringTestTemplate10Identity)
                    .WithScriptsFromFileSystem(
                        scriptsPath != null
                            ? Path.Combine(scriptsPath, "TestTemplate10IdentityScripts")
                            : Path.Combine(Environment.CurrentDirectory, "TestTemplate10IdentityScripts"))
                    .LogToConsole()
                    .Build();
            Console.WriteLine($"Now upgrading TestTemplate10 Identity.");
            if (env != "Development")
            {
                upgraderTestTemplate10Identity.MarkAsExecuted("0004_InitialData.sql");
                Console.WriteLine($"Skipping 0004_InitialData.sql since we are not in Development environment.");
                upgraderTestTemplate10Identity.MarkAsExecuted("0005_Initial_Configuration_Data.sql");
                Console.WriteLine($"Skipping 0005_Initial_Configuration_Data.sql since we are not in Development environment.");
            }
            var resultTestTemplate10Identity = upgraderTestTemplate10Identity.PerformUpgrade();

            if (!resultTestTemplate10Identity.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TestTemplate10 Identity upgrade error: {resultTestTemplate10Identity.Error}");
                Console.ResetColor();
                return -1;
            }
            */

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;

            void InitializeParameters()
            {
                // Local database, populated from .env file.
                if (args.Length == 0)
                {
                    connectionString = config["ConnectionStrings:TestTemplate10Db_Migrations_Connection"];
                    dbUser = config["DB_USER"];
                    dbPassword = config["DB_PASSWORD"];
                }

                // Remote database
                else if (args.Length == 5)
                {
                    connectionString = args[0];
                    dbUser = args[1];
                    dbPassword = args[2];
                    scriptsPath = args[3];
                    sqlUsersGroupName = args[4];
                }
            }
        }
    }
}
