using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RestReservation.Models;
using RestReservation.ViewModels;

namespace RestReservation;

public class ReservationController:Controller{
  
   private readonly IReservationService _reservationService;
   private readonly IRestaurantService _restaurantService;

   public ReservationController(
    IReservationService reservationService,
    IRestaurantService restaurantService
   )
   {
      _restaurantService = restaurantService;
      _reservationService = reservationService;
   }

   public IActionResult Index(){
    ReservationListViewModel viewModel = new ReservationListViewModel()
     {
        Reservations = _reservationService.GetAllReservation()
     };

     return View(viewModel);
   }


   public IActionResult Add(ObjectId restaurantId){
      var selectedRestaurant = _restaurantService.GetRestaurantById(restaurantId);

      ReservationAddViewModel reservationAddViewModel = new ReservationAddViewModel();

      reservationAddViewModel.Reservations = new Reservation();
      reservationAddViewModel.Reservations.RestaurantId = selectedRestaurant.Id;
      reservationAddViewModel.Reservations.RestaurantName = selectedRestaurant.name;
      reservationAddViewModel.Reservations.date = DateTime.UtcNow;

      return View(reservationAddViewModel);

   }

    [HttpPost]
   public IActionResult Add(ReservationAddViewModel reservationAddViewModel){
    Reservation newReservation = new(){
       RestaurantId = reservationAddViewModel.Reservations.RestaurantId,
       date = reservationAddViewModel.Reservations.date
    };

    _reservationService.AddReservation(newReservation);
    return RedirectToAction("Index");
   }

   public  IActionResult Edit(string Id){
    if(Id == null || string.IsNullOrEmpty(Id)){
       return NotFound();
    }

    var selectedReservation = _reservationService.GetReservationById(new ObjectId(Id));
    return View(selectedReservation);
   }

   [HttpPost]
   public IActionResult Edit(Reservation reservation){
     try{

        var existingReservation = _reservationService.GetReservationById(reservation.Id);

        if(existingReservation != null){
            _reservationService.EditReservation(reservation);

            return RedirectToAction("Index");
        }else{
            ModelState.AddModelError("", "Updating the reservation");
        }


     }catch(Exception ex){
        ModelState.AddModelError("", "Updating the reservation");
     }

     return View(reservation);
   }

    public IActionResult Delete(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
            return NotFound();

        var selectedReservation = _reservationService.GetReservationById(id);

        return View(selectedReservation);
    }


    [HttpPost]
    public IActionResult Delete(Reservation reservation)
    {

        if (reservation.Id == ObjectId.Empty)
        {
            ViewData["ErrorMessage"] = "Deleting the reservation failed, invalid ID";

            return View();
        }

        try
        {
            _reservationService.DeleteReservation(reservation);
            TempData["ReservationDeleted"] = "reservation deleted successfully";

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = $"Deleting the reservation failed, please try again";
        }

        var selectedReservation = _reservationService.GetReservationById(reservation.Id);

        return View(selectedReservation);
    }


}