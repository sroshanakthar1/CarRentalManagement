namespace CarRentalManagement.ViewModels
{
    public class PaymentViewModel
    {
        public int CarID { get; set; }
        public int CarId { get; internal set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal CarPricePerDay { get; set; } // optional, needed if using JS recalculation

    }
}
