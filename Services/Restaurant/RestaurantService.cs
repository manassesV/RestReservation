using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using RestReservation;
using RestReservation.Models;

namespace RestReservation;

public class RestaurantService : IRestaurantService
{

    private readonly RestaurantReservationDbContext _restaurantDbContext;

    public RestaurantService(RestaurantReservationDbContext reservationDbContext)
    {
        _restaurantDbContext = reservationDbContext;
    }

    public void AddRestaurant(Restaurant restaurant)
    {
        _restaurantDbContext.Restaurants.AddAsync(restaurant);

        _restaurantDbContext.ChangeTracker.DetectChanges();

        Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);

        _restaurantDbContext.SaveChangesAsync();
    }

    public void DeleteRestaurant(Restaurant restaurant)
    {
        var restaurantToDelete = _restaurantDbContext.Restaurants
        .Where(c => c.Id == restaurant.Id).First();

        if (restaurantToDelete != null)
        {
            _restaurantDbContext.Restaurants.Remove(restaurantToDelete);
            _restaurantDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);
            _restaurantDbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentNullException("The restaurant to delete cannot be found");
        }
    }

    public void EditRestaurant(Restaurant restaurant)
    {
        var restaurantToUpdate = _restaurantDbContext.Restaurants
    .Where(c => c.Id == restaurant.Id).First();

        if (restaurantToUpdate != null)
        {
            restaurantToUpdate.name = restaurant.name;
            restaurantToUpdate.cuisine = restaurant.cuisine;
            restaurantToUpdate.borough = restaurant.borough;

            _restaurantDbContext.Restaurants.Update(restaurantToUpdate);

            _restaurantDbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_restaurantDbContext.ChangeTracker.DebugView.LongView);

            _restaurantDbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentNullException("The restaurant to delete cannot be found");
        }
    }

    public IEnumerable<Restaurant> GetAllRestaurants()
    {
        return _restaurantDbContext.Restaurants.OrderByDescending(c => c.Id)
        .Take(20).AsNoTracking().AsParallel();
    }

    public Restaurant? GetRestaurantById(ObjectId id)
    {
        return _restaurantDbContext.Restaurants.FirstOrDefault(c => c.Id == id);
    }

}