using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RestReservation.Models;
using RestReservation.ViewModels;

namespace RestReservation.Controllers;


public class RestaurantController : Controller
{

    private readonly IRestaurantService _restaurantService;

    public RestaurantController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    public IActionResult Index()
    {
        RestaurantListViewModel viewModel = new()
        {
            Restaurants = _restaurantService.GetAllRestaurants()
        };

        return View(viewModel);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(RestaurantAddViewModel restaurantAddView)
    {
        if (ModelState.IsValid)
        {
            Restaurant newRestaurant = new()
            {
                name = restaurantAddView.Restaurants.name,
                borough = restaurantAddView.Restaurants.borough,
                cuisine = restaurantAddView.Restaurants.cuisine
            };

            _restaurantService.AddRestaurant(newRestaurant);

            return RedirectToAction("Index");
        }

        return View(restaurantAddView);
    }

    public IActionResult Edit(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
            return NotFound();

        var selectRestaurant = _restaurantService.GetRestaurantById(id);

        return View(selectRestaurant);
    }


    [HttpPost]
    public IActionResult Edit(Restaurant restaurant)
    {
        try
        {

            if (ModelState.IsValid)
            {
                _restaurantService.EditRestaurant(restaurant);

                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }

        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Updating the restaurant");
        }

        return View(restaurant);
    }

    public IActionResult Delete(ObjectId id)
    {
        if (id == null || id == ObjectId.Empty)
            return NotFound();

        var selectedRestaurant = _restaurantService.GetRestaurantById(id);

        return View(selectedRestaurant);
    }


    [HttpPost]
    public IActionResult Delete(Restaurant restaurant)
    {

        if (restaurant.Id == ObjectId.Empty)
        {
            ViewData["ErrorMessage"] = "Deleting th restaurant failed, invalid ID";

            return View();
        }

        try
        {
            _restaurantService.DeleteRestaurant(restaurant);
            TempData["RestaurantDeleted"] = "Restaurant deleted successfully";

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewData["ErrorMessage"] = $"Deleting the restaurant failed, please try again";
        }

        var selectedRestaurant = _restaurantService.GetRestaurantById(restaurant.Id);

        return View(selectedRestaurant);
    }

}