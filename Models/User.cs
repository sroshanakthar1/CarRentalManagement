using System.ComponentModel.DataAnnotations;


namespace CarRentalManagement.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        public string Nationality { get; set; } = string.Empty;

        [StringLength(20)]
        public string? NationalID { get; set; }

        [StringLength(20)]
        public string? PassportNumber { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), StringLength(50)]
        public string Password { get; set; } = string.Empty;

        [EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(15)]
        public string Phone { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Role { get; set; } = "Customer";

        public ICollection<Booking>? Bookings { get; set; }
    }
}