using System.ComponentModel.DataAnnotations;

namespace CarRentalManagement.Models
{
    public class Users
    {

        [Key]
        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Nationality { get; set; } = string.Empty;

        public string? NationalID { get; set; }
        public string? PassportNumber { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        // 👇 Default value set here
        [Required]
        public string Role { get; set; } = "Customer";

        public ICollection<Booking>? Bookings { get; set; }
    }
}