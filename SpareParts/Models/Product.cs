﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpareParts.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Photo { get; set; }

        public int Quantity { get; set; }
        public string Brand { get; set; }



        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }



        public int CategoryId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Category Category { get; set; }
    }
}
