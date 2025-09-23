namespace CarRentalManagement.ViewModels
{
    public class PaymentSuccessViewModel
    {

        public int BookingId { get; set; }
        public string Username { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalCost { get; set; }
        public string CollectionAddress { get; set; }
        public string ContactNumber { get; set; }
    }
}
