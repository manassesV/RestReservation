using RestReservation.Models;

namespace RestReservation.ViewModels;
public class ReservationListViewModel{
    public IEnumerable<Reservation>? Reservations{get; set;}
}