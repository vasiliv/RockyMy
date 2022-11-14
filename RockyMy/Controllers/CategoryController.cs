using Microsoft.AspNetCore.Mvc;
using RockyMy.Data;
using System.Linq;

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
    }
}
