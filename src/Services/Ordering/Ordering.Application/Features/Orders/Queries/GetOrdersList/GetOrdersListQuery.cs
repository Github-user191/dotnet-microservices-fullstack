using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList {
    public class GetOrdersListQuery : IRequest<List<OrdersVm>> {

        // Field we want to query by
        public string UserName { get; set; }

        public GetOrdersListQuery(string userName) {

            try {
                if (userName == "user") {
                    throw new NotFoundException("Not found");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }

            UserName = userName;
        }
    }
}
