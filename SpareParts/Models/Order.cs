using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        public List<OrderDetail> OrderLines { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address 2")]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "Zip code")]
        [StringLength(10, MinimumLength = 4)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        [DisplayName("Order Total")]
        public decimal OrderTotal { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        [DisplayName("Order Date")]
        public DateTime OrderPlaced { get; set; }

        //[BindNever]
        //[ScaffoldColumn(false)]
        //public string OrderStatus { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

    }
}