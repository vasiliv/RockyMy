using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockyMy.Data;
using RockyMy.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RockyMy.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(x => x.Category).ToList()) ;
        }
        // instead of Create and Edit
        public IActionResult Upsert(int? id)
        {
            if (id == null)
            {
                //Create
                return View();
            }
            else
            {
                //Update
                var prod = _context.Products.Find(id);
                if (prod == null)
                {
                    return NotFound();
                }
                return View(prod);
            }            
        }
        // instead of Create and Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
