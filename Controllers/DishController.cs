using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using crudelicious.Models;
using System.Reflection.Metadata.Ecma335;
using System.Globalization;

namespace crudelicious.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    private MyContext _context;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Dish> AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
        return View("Index", AllDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {

        return View("NewDish");
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        _context.Add(newDish);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet("dishes/{dishesId}")]
    public IActionResult OneDish(int dishesId)
    {
        Dish? SingleDish = _context.Dishes.FirstOrDefault(t => t.DishesId == dishesId);
        return View("Details", SingleDish);
    }

    [HttpPost("dishes/{dishesId}/delete")]
    public IActionResult DeleteDish(int dishesId)
    {
        Dish? DishToDelete = _context.Dishes.SingleOrDefault(t => t.DishesId == dishesId);
        if (DishToDelete != null)
        {
            _context.Remove(DishToDelete);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    [HttpGet("dishes/{dishesId}/edit")]
    public IActionResult EditDish(int dishesId)
    {
        Dish? SingleDish = _context.Dishes.FirstOrDefault(t => t.DishesId == dishesId);
        if (SingleDish == null)
        {
            return RedirectToAction("Index");
        }
        return View("Edit", SingleDish);
    }

    [HttpPost("dishes/{dishesId}/update")]
    public IActionResult UpdateDish(int dishesId, Dish editedDish)
    {
        Dish? OldDish = _context.Dishes.FirstOrDefault(t => t.DishesId == dishesId);
        if (!ModelState.IsValid)
        {
            return View("Edit", OldDish);
        }
        OldDish.Chef = editedDish.Chef;
        OldDish.Name = editedDish.Name;
        OldDish.Calories = editedDish.Calories;
        OldDish.Tastiness = editedDish.Tastiness;
        OldDish.Description = editedDish.Description;
        OldDish.UpdateAt = DateTime.Now;
        _context.SaveChanges();


        return RedirectToAction("OneDish", new { dishesId });
    }






    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
