using CarRentalManagement.Models;

namespace CarRentalManagement.ViewModels
{
    public class BookingPageViewModel
    {

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();
        public BookingViewModel BookingInfo { get; set; } = new BookingViewModel();


    }
}
