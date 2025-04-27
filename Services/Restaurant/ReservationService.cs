using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace RestReservation;

public class ReservationService : IReservationService
{
    public RestaurantReservationDbContext _reservationDbContext;

    public ReservationService(RestaurantReservationDbContext reservationDbContext)
    {
        _reservationDbContext = reservationDbContext;
    }

    public void AddReservation(Reservation newReservation)
    {
        var bookedRestaurant = _reservationDbContext.Restaurants.FirstOrDefault(c => c.Id == newReservation.RestaurantId);

        if(bookedRestaurant == null){
            throw new ArgumentException("The restaurantv to be reserved cannot be found");
        }

        newReservation.RestaurantName = bookedRestaurant.name;

        _reservationDbContext.Reservations.Add(newReservation);

        _reservationDbContext.ChangeTracker.DetectChanges();

       Console.WriteLine(_reservationDbContext.ChangeTracker.DebugView.LongView);

       _reservationDbContext.SaveChangesAsync();
    }

    public void DeleteReservation(Reservation reservationToDelete)
    {
        var deleteReservation = _reservationDbContext.Reservations.FirstOrDefault(c => c.Id == reservationToDelete.Id);

        if(deleteReservation != null){

            _reservationDbContext.Reservations.Remove(deleteReservation);
            _reservationDbContext.ChangeTracker.DetectChanges();
            _reservationDbContext.SaveChanges();

             Console.WriteLine(_reservationDbContext.ChangeTracker.DebugView.LongView);

        }else{
            throw new ArgumentException("Reservation cannot be found");
        }
    }

    public void EditReservation(Reservation updateReservation)
    {
        var bookedRestaurant = _reservationDbContext.Reservations.FirstOrDefault(c => c.Id == updateReservation.Id);

        if(bookedRestaurant != null){
            bookedRestaurant.date = updateReservation.date;

            _reservationDbContext.Reservations.Update(bookedRestaurant);

            _reservationDbContext.ChangeTracker.DetectChanges();
            _reservationDbContext.SaveChanges();

            Console.WriteLine(_reservationDbContext.ChangeTracker.DebugView.LongView);
        }else{
                        throw new ArgumentException("Reservation to be updated cannot be found");

        }
    }

    public IEnumerable<Reservation> GetAllReservation()
    {
        return _reservationDbContext
        .Reservations
        .OrderBy(b => b.date)
        .Take(20)
        .AsTracking()
        .AsEnumerable();
    }

    public Reservation? GetReservationById(ObjectId id)
    {
        return _reservationDbContext
        .Reservations
        .AsTracking()
        .FirstOrDefault(b => b.Id == id);
    }
}