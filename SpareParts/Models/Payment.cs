using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public string PaymentType { get; set; }
        public double Amount { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
    }
}
