using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Data {
    public class DBContextSeed {

        public static async Task SeedAsync(ApplicationDbContext context, ILogger<DBContextSeed> logger) {
            if (!context.Orders.Any()) {
                context.Orders.AddRange(GetPreconfiguredOrders());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders() {
            return new List<Order> {
                new Order() {
                    UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com",
                    AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350
                }
            };
        }
    }
}
