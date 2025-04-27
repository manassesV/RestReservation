using RestReservation.Models;

namespace RestReservation.ViewModels;
public class RestaurantListViewModel{
    public IEnumerable<Restaurant>? Restaurants { get; set; }
}