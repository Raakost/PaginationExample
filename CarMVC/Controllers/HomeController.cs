using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using CarMVC.Models;

namespace CarMVC.Controllers
{
    public class HomeController : Controller
    {
        List<Car> _cars = new List<Car>();

        public HomeController()
        {
            CreateCars();
        }

        public ActionResult Index()
        {
            return View(_cars);
        }

        /// <summary>
        /// Create 15 cars with unique id's, add them to list then return it.
        /// </summary>
        /// <returns>list of cars</returns>
        public List<Car> CreateCars()
        {
            int count = 15;

            for (int i = 0; i < count; i++)
            {
                var car = new Car() { Id = i + 1, Make = "Toyota" };
                _cars.Add(car);
            }
            return _cars;
        }

        /// <summary>
        /// Using Linq to find a car with specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The car with the specified id</returns>
        public ActionResult Single(int id)
        {
            var car = new List<Car>();
            car.Add(_cars.FirstOrDefault(x => x.Id == id));
            return View("Index", car);
        }

        /// <summary>
        /// Using Linq Take() to get specified amount of cars to show
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>The amount of cars in ascending order</returns>
        public ActionResult First(int amount)
        {
            return View("Index", _cars.Take(amount).ToList());
        }

        /// <summary>
        /// Returns a paginated list of cars as Json.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPrPage"></param>
        /// <param name="sortDir"></param>
        /// <returns></returns>
        [ActionName("Pagination")]
        public ActionResult PaginatedIndex(int page, int itemsPrPage, bool sortDir)
        {
            if (sortDir)
            {
                _cars = _cars.OrderBy(x => x.Id).ToList();
            }
            else
            {
                _cars = _cars.OrderByDescending(x => x.Id).ToList();
            }
            //Ex - Show page 2: 
            //page = 2  
            //itemsPrPage = 5
            //Skip (2 - 1) * 5) therefore skip items 1 - 5, then take next 5 (6 - 10).             
            return Json(_cars.Skip((page - 1) * itemsPrPage).Take(itemsPrPage).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}