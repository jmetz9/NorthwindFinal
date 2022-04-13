using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class HomeController : Controller
    {
        // this controller depends on the NorthwindRepository
        private NorthwindContext _northwindContext;
        public HomeController(NorthwindContext db) => _northwindContext = db;

        public ActionResult Index() => View(_northwindContext.Discounts.Where(d => d.StartTime <= DateTime.Now && d.EndTime > DateTime.Now).Take(3));
    }
}