using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Northwind.Models;

namespace Northwind.Controllers
{
    public class APIController : Controller
    {
        // this controller depends on the NorthwindRepository
        private NorthwindContext _northwindContext;
        public APIController(NorthwindContext db) => _northwindContext = db;

        [HttpGet, Route("api/product")]
        // returns all products
        public IEnumerable<Product> Get() => _northwindContext.Products.OrderBy(p => p.ProductName);
        [HttpGet, Route("api/product/{id}")]
        // returns specific product
        public Product Get(int id) => _northwindContext.Products.FirstOrDefault(p => p.ProductId == id);
        [HttpGet, Route("api/product/discontinued/{discontinued}")]
        // returns all products where discontinued = true/false
        public IEnumerable<Product> GetDiscontinued(bool discontinued) => _northwindContext.Products.Where(p => p.Discontinued == discontinued).OrderBy(p => p.ProductName);
        [HttpGet, Route("api/category/{CategoryId}/product")]
        // returns all products in a specific category
        public IEnumerable<Product> GetByCategory(int CategoryId) => _northwindContext.Products.Where(p => p.CategoryId == CategoryId).OrderBy(p => p.ProductName);
        [HttpGet, Route("api/category/{CategoryId}/product/discontinued/{discontinued}")]
        // returns all products in a specific category where discontinued = true/false
        public IEnumerable<Product> GetByCategoryDiscontinued(int CategoryId, bool discontinued) => _northwindContext.Products.Where(p => p.CategoryId == CategoryId && p.Discontinued == discontinued).OrderBy(p => p.ProductName);

        [HttpGet, Route("api/orders/overdue")]
        [Authorize(Roles = "northwind-employee")]
        public IEnumerable<Order> GetOverdueOrders() => _northwindContext.Orders.Where(o => o.RequiredDate.Value.Date <= System.DateTime.Now.Date && o.ShippedDate == null).OrderBy(o => o.OrderDate);
        
        [HttpGet, Route("api/orders/")]
        [Authorize(Roles = "northwind-employee")]
        public IEnumerable<Order> GetOrders() => _northwindContext.Orders.OrderBy(o => o.OrderDate);
        [HttpGet, Route("api/orders/overdue/{daysAlmostOverdue}")]
        [Authorize(Roles = "northwind-employee")]
        public IEnumerable<Order> GetNearOverdueOrders(int daysAlmostOverdue) => _northwindContext.Orders.Where(o => o.RequiredDate.Value.Date <= System.DateTime.Now.AddDays(daysAlmostOverdue).Date && o.RequiredDate.Value.Date > System.DateTime.Now.Date && o.ShippedDate == null).OrderBy(o => o.OrderDate);
    }
}