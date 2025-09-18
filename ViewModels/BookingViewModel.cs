namespace CarRentalManagement.ViewModels
{
    public class BookingViewModel
    {
        public int CarID { get; set; }
        public string? CarName { get; set; }
        public string? CarModel { get; set; }
        public string? CarImageUrl { get; set; }
        public decimal CarPrice { get; set; }
        public string? Transmission { get; set; }
        public string? FuelType { get; set; }
        public int SeatingCapacity { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalCost { get; set; }
        public int SelectedCarId { get; set; }
        public string? Error { get; set; }


    }
}
