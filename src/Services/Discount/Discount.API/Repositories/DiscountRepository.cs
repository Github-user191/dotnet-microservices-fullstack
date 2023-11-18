using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories {
    public class DiscountRepository : IDiscountRepository {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration) {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName) {

            // Get connection to Postgres DB
            using var connection = new NpgsqlConnection(_configuration["DatabaseSettings:ConnectionString"]);

            // Query Database and apply Dapper filter (QueryFirstOrDefaultAsync)
            // Returns the first row as an instance of the type specified by the T type parameter or null if no results are returned
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName",
                new {ProductName = productName}
            );

            if(coupon == null) {
                return new Coupon {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description= "No Discount Description"
                };
            }
            
            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon) {
            // Get connection to Postgres DB
            using var connection = new NpgsqlConnection(_configuration["DatabaseSettings:ConnectionString"]);

            var affectedRows = await connection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new Coupon {
                    ProductName = coupon.ProductName,
                    Amount = coupon.Amount,
                    Description= coupon.Description
                }
            );

            return affectedRows != 0;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon) {
            // Get connection to Postgres DB
            using var connection = new NpgsqlConnection(_configuration["DatabaseSettings:ConnectionString"]);

            var affectedRows = await connection.ExecuteAsync(
                "UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id = @Id",
                new Coupon {
                    Id = coupon.Id,
                    ProductName = coupon.ProductName,
                    Amount = coupon.Amount,
                    Description= coupon.Description
                }
            );

            Console.WriteLine($"--> Update affected rows: {affectedRows}");

        
            return affectedRows != 0;
        }

        public async Task<bool> DeleteDiscount(string productName) {
            // Get connection to Postgres DB
            using var connection = new NpgsqlConnection(_configuration["DatabaseSettings:ConnectionString"]);

            var affectedRows = await connection.ExecuteAsync(
                "DELETE FROM Coupon WHERE ProductName = @ProductName",
                new {ProductName = productName}
            );

            return affectedRows != 0;
        }

    }
}