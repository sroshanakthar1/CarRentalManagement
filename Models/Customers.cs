using System.ComponentModel.DataAnnotations;

namespace CarRentalManagement.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [StringLength(50)]
        public string DrivingLicense { get; set; } = string.Empty;
    }
}
