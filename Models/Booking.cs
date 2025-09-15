using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarRentalManagement.Models
{
    public class Booking
    {
        [Key]
        public int BookingID { get; set; }


        [Required]
        public int CustomerID { get; set; }
        public Users? Customer { get; set; }


        [Required]
        public int CarID { get; set; }
        public Car? Car { get; set; }


        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalCost { get; set; }
    }
}