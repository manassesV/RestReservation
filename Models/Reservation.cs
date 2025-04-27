using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace RestReservation;


[Collection("reservations")]
public class Reservation{
    public ObjectId Id {get; set;}

    public ObjectId RestaurantId {get; set;}

    public string? RestaurantName{get; set;}


    [Required(ErrorMessage = "The date time is required to make reservation")]
    [Display(Name = "Date")]
    public DateTime date {get; set;}
}