using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Saga
{
    public class OrderRequest
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
    }
}
