using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Contracts
{
    public interface IOrderContract
    {
        ValueTask<OperationResult<Order>> PlaceOrder(Order order);
    }
}
