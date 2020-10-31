using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.Models
{
    public class OrderHeader
    {
        [Key]
        public int ID { get; set; }

        public string ApplicationUserID { get; set; }

        [ForeignKey("ApplicationUserID")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime ShippedDate { get; set; }

        [Required]
        public double OrderTotal { get; set; }

        public string TrackingNumber { get; set; }

        public string Carrier { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime PaymentDueDate { get; set; }

        public string TransactionID { get; set; }

        public string Name { get; set; }

        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string State { get; set; }
    }
}
