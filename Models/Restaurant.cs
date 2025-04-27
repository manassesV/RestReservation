

using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace RestReservation.Models;


[Collection("restaurants")]
public class Restaurant{

 public ObjectId Id{get; set;}

 [Required(ErrorMessage = "You must provide a name")]
 [Display(Name = "Name")]
 public string? name {get; set;}

 [Required(ErrorMessage = "You must add a cuisine type")]
 [Display(Name = "Cuisine")]
 public string? cuisine{get; set;}

 [Required(ErrorMessage = "You must add the borough os the restaurant")]
 public string? borough {get; set;}
}