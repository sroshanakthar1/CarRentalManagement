using System.ComponentModel.DataAnnotations;

namespace CarRentalManagement.Models
{
    public class Payment
    {

        [Key]
        public int PaymentId { get; set; }

        public int BookingId { get; set; }
        public int CarId { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } // e.g. "Pending", "Completed"
        public DateTime PaymentDate { get; set; }
    }
}
