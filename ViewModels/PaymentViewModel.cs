namespace CarRentalManagement.ViewModels
{
    public class PaymentViewModel
    {
        internal string CarModel;

        public int BookingId { get; set; }
        public int CarId { get; set; }
        public string CarName { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalCost { get; set; }

        // User details
        public int UserId { get; set; }
        public string UserName { get; set; }

        // Payment form
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal CarPricePerDay { get; internal set; }
    }
}
