using Discount.Grpc.Entities;
using Discount.Grpc.Repositories;
using Npgsql;

namespace Discount.Grpc.Data {
    public static class MigrateDbExtention {
        public static WebApplication MigrateDatabase<T>(this WebApplication app, int? retry = 0) {
            var retryForAvailability = retry.Value;

            var configuration = app.Services.GetService<IConfiguration>();
            var logger = app.Services.GetService<ILogger<T>>();

            try {
                logger.LogInformation("Migrating postgresql database.");

                logger.LogInformation(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

                using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand { Connection = connection };

                //   command.CommandText = "DROP TABLE IF EXISTS Coupon";
                //   command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE IF NOT EXISTS Coupon(Id SERIAL PRIMARY KEY, 
                    ProductName VARCHAR(24) NOT NULL,
                    Description TEXT,
                    Amount INT)";
                command.ExecuteNonQuery();

                command.CommandText =
                  "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                command.ExecuteNonQuery();

                command.CommandText =
                  "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                command.ExecuteNonQuery();

                logger.LogInformation("Migrated postgresql database.");
            }
            catch (NpgsqlException ex) {
                logger.LogError(ex, $"An error occurred while migrating the postgresql database");
                logger.LogError($"--> RETRY ATTEMPT {retryForAvailability}");

                if (retryForAvailability < 50) {
                    retryForAvailability++;
                    Thread.Sleep(2000);

                    MigrateDatabase<T>(app, retryForAvailability);
                }
            }

            return app;
        }
        //     public static void PrepPopulation(IApplicationBuilder applicationBuilder) {
        //         using (var app = applicationBuilder.ApplicationServices.CreateScope()) {
        //             var configuration = app.ServiceProvider.GetService<IConfiguration>();
        //             var logger = app.ServiceProvider.GetService<ILogger<Coupon>>();

        //             logger.LogInformation("Migrating postgresql database.");
        //             logger.LogInformation(configuration["DatabaseSettings:ConnectionString"]);
        //             SeedData(configuration, logger);
        //         }
        //     }

        //     private static void SeedData(IConfiguration configuration, ILogger logger) {
        //         Console.WriteLine("Seeding new coupons...");

        //         try {

        //             using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        //             connection.Open();

        //             using var command = new NpgsqlCommand {
        //                 Connection = connection
        //             };

        //             //command.CommandText = "DROP TABLE IF EXISTS Coupon";
        //             //command.ExecuteNonQuery();

        //             command.CommandText = @"CREATE TABLE IF NOT EXISTS Coupon(Id SERIAL PRIMARY KEY, 
        //                 ProductName VARCHAR(24) NOT NULL,
        //                 Description TEXT,
        //                 Amount INT)";
        //             command.ExecuteNonQuery();

        //             command.CommandText =
        //                 "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
        //             command.ExecuteNonQuery();

        //             command.CommandText =
        //                 "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
        //             command.ExecuteNonQuery();

        //             logger.LogInformation("Migrated postgresql database.");
        //         }
        //         catch (NpgsqlException ex) {
        //             logger.LogError(ex, "An error occurred while migrating the postgresql database");
        //         } finally {
        //             logger.LogInformation("Migration complete..");
        //         }
        //     }
    }
}