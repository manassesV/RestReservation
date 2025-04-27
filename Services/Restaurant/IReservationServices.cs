using MongoDB.Bson;

namespace RestReservation;


public interface IReservationService{
    IEnumerable<Reservation> GetAllReservation();

    Reservation? GetReservationById(ObjectId id);

    void AddReservation(Reservation newReservation);
    void EditReservation(Reservation updateReservation);
    void DeleteReservation(Reservation reservationToDelete);
}