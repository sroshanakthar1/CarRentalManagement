using System.ComponentModel.DataAnnotations;

namespace CarRentalManagement.Models
{
    public class Car
    {
        [Key]
        public int CarID { get; set; }

        [Required, StringLength(100)]
        public string CarName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string CarModel { get; set; } = string.Empty;

        [StringLength(200)]
        public string? ImageUrl { get; set; }

        [Required, StringLength(20)]
        public string Transmission { get; set; } // e.g., Automatic, Manual

        [Required, StringLength(20)]
        public string FuelType { get; set; }   // e.g., Petrol, Diesel, Hybrid, Electric

        [Range(2, 10)]
        public int SeatingCapacity { get; set; } 

        [DataType(DataType.Currency)]
     
        public decimal PricePerDay { get; set; } 

        public bool IsAvailable { get; set; } = true;

        // Navigation property
        public ICollection<Booking>? Bookings { get; set; }
    }
}
