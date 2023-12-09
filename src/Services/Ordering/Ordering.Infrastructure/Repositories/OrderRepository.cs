using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories {
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository {

        public OrderRepository(ApplicationDbContext context) : base(context) {
            
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName) {
            var orders = await _context.Orders
                .Where(o => o.UserName == userName)
                .ToListAsync();

            return orders;
        }


    }
}
