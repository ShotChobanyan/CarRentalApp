using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarRentalApp.Controllers
{
    public class CarsController : Controller
    {
        ApplicationContext db;
        public CarsController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Cars.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Car car, IFormFile photo)
        {
            if ( photo != null &&  photo.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    photo.CopyTo(stream);
                    car.CarPhoto = Convert.ToBase64String(stream.ToArray());
                }
            }
            
            db.Cars.Add(car);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var car = db.Cars.Single(w => w.Id == id);
            return View(car);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Car car, IFormFile photo)
        {
            if(photo != null && photo.Length > 0)
            {

            using (var stream = new MemoryStream())
            {
                photo.CopyTo(stream);
                car.CarPhoto = Convert.ToBase64String(stream.ToArray());
            }
            }

            db.Cars.Update(car);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Rent(int id)
        {
            var car = db.Cars.Single(w => w.Id == id);
            ViewBag.Car = car;
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Rent(RentHistory rentHistory)
        {
            db.RentHistory.Add(rentHistory);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
         }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Stats(int id)
        {
            var history = db.RentHistory.Include(x => x.Car).ToList();
            return View(history);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reserve()
        {
            return View(await db.Cars.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            var car = await db.Cars.SingleOrDefaultAsync(x => x.Id == model.Id);
            db.Cars.Remove(car);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
