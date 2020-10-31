using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }

        [Key]
        public int ID { get; set; }

        public string ApplicationUserID { get; set; }

        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }

        [Range(1,1000,ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
