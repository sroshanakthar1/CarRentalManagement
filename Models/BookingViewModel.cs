namespace CarRentalManagement.Models
{
    public class BookingViewModel
    {
        public int CarID { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalCost { get; set; }

        // Extra info for dropdown / display
        public List<Car>? Cars { get; set; }
        public int? SelectedCarId { get; set; }
        public string? Error { get; set; }
    }
}
