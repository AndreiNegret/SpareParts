using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
