using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CarRentalManagement.Models
{
    public class Booking
    {
        private string username;

        [Key]
        public int BookingID { get; set; }

        [Required]
        public string Username { get => username; internal set => username = value; }
        public int CustomerID { get; set; }
        public User? Customer { get; set; }


        [Required]
        public int CarID { get; set; }
       

        public Car? Car { get; set; }


        [DataType(DataType.Date)]
        public DateTime PickupDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }


        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalCost { get; set; }
       
        public DateTime BookingDate { get; internal set; }
        public int UserID { get; set; }  // Foreign key

        public User? User { get; set; }  // Navigation property
        public int StartDate { get; internal set; }
        public int EndDate { get; internal set; }
    }
}