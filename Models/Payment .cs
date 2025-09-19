
using System;
using System.ComponentModel.DataAnnotations;

namespace CarRentalManagement.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int BookingId { get; set; }  // Linked to Booking

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } // Card, Cash, Online

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }
    }
}
