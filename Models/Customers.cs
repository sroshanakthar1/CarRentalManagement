using System.ComponentModel.DataAnnotations;

namespace CarRentalManagement.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, Phone]
        public string Phone { get; set; } = string.Empty;

        [StringLength(50)]
        public string DrivingLicense { get; set; } = string.Empty;
    }
}
