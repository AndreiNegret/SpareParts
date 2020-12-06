using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Models
{
    public class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }
        public int SupplierId { get; set; }

        public string CompanyName { get; set; }


        public virtual ICollection<Product> Products { get; set; }
    }
}
