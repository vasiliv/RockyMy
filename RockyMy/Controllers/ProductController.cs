using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RockyMy.Data;
using RockyMy.Models;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using RockyMy.ViewModels;

namespace RockyMy.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(x => x.Category).ToList()) ;
        }
        // instead of Create and Edit
        public IActionResult Upsert(int? id)
        {
            //migrate to strongly typed ViewModels
            //IEnumerable<SelectListItem> CategoryDropDown = _context.Categories.Select(a => new SelectListItem()
            //{
            //    Value = a.Id.ToString(),
            //    Text = a.Name
            //}).ToList();
            //ViewBag.CategoryDropDown = CategoryDropDown;

            ProductVM productVM = new ProductVM() {
                Product = new Product(),
                CategorySelecytList = _context.Categories.Select(a => new SelectListItem()
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                }).ToList()
                };

            if (id == null)
            {
                //Create
                return View(productVM);
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
        public IActionResult Upsert(Product model)
        {
            //if (ModelState.IsValid)
            //{
                string uniqueFileName = UploadedFile(model);

                Product product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Image = uniqueFileName,
                    //gasascorebelia
                    CategoryId = 1
                };

                _context.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            //}
            //return View();
        }
        private string UploadedFile(Product model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
