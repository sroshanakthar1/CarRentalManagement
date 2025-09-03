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


        public bool IsAvailable { get; set; } = true;


        public ICollection<Booking>? Bookings { get; set; }
    }
}