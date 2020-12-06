using Microsoft.AspNetCore.Mvc.Rendering;
using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Areas.Administrator.ClassModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Suppliers { get; set; }
    }
}
