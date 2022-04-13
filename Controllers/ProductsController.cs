using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class ProductsController : Controller
    {
        // this controller depends on the BloggingRepository
        private NorthwindContext _northwindContext;
        public ProductsController(NorthwindContext db) => _northwindContext = db;

        public IActionResult Category() => View(_northwindContext.Categories.OrderBy(c => c.CategoryName));
        public IActionResult Index(int id) => View(new ProductViewModel
        {
            category = _northwindContext.Categories.FirstOrDefault(c => c.CategoryId == id),
            Products = _northwindContext.Products.Where(p => p.CategoryId == id)
        });

        public IActionResult Discount() => View(_northwindContext.Discounts.Where(d => DateTime.Compare(d.StartTime,DateTime.Now) <= 0 && DateTime.Compare(d.EndTime,DateTime.Now) > 0));
            
        public IActionResult Orders() => View();
    }
}