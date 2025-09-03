using System.ComponentModel.DataAnnotations;


namespace CarRentalManagement.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }


        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;


        // Plain text by assignment simplicity (not recommended for real apps)
        [Required, StringLength(50)]
        public string Password { get; set; } = string.Empty;


        [Required, StringLength(20)]
        public string Role { get; set; } = "Customer"; // "Admin" or "Customer"


        public ICollection<Booking>? Bookings { get; set; }
    }
}