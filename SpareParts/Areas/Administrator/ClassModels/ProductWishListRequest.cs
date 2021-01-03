using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SpareParts.Areas.Administrator.ClassModels
{
    [DataContract]
    public class ProductWishListRequest
    {
        [DataMember(Name = "productId")]
        public int ProductId { get; set; }

        [DataMember(Name = "userId")]
        public string UserId { get; set; }
    }
}
