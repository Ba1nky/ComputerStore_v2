using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ComputerStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreContext _context;

        public HomeController(StoreContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category)
                .ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Product = _context.Products.FirstOrDefault(p => p.ProductId == purchase.ProductId);
            _context.Add(purchase);
            _context.SaveChanges();
            return $"Cпасибі, {purchase.Person}, за покупку!";
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if( product is not null)
            {
                SelectList categories = new SelectList(_context.categories, "CategoryId", "Name");
                ViewBag.Categories = categories;

                return View(product);
            }
            return NotFound();
        }
         
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult CreateProduct()
        {
            SelectList categories = new SelectList(_context.categories, "CategoryId", "Name");
            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            Product product = _context.Products.Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost,ActionName("DeleteProduct")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}