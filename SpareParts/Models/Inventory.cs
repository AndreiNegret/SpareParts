using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }

    }
}
