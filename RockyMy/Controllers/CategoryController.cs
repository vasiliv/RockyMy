using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockyMy.Data;
using RockyMy.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RockyMy.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }        
        public IActionResult Index()
        {
            return View(_context.Categories.ToList());;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]        
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dept = await _context.Categories.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
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
