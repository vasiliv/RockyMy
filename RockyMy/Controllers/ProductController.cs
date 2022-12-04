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
                productVM.Product = _context.Products.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }            
        }
        // instead of Create and Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Upsert(Product model)
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                //from https://github.com/vasiliv/ImageUpload master branch, MvcCoreUploadAndDisplayImageDemo project
                //string uniqueFileName = UploadedFile(model);

                //Product product = new Product()
                //{
                //    Name = model.Name,
                //    Description = model.Description,
                //    Price = model.Price,
                //    Image = uniqueFileName,
                //    //gasascorebelia
                //    CategoryId = 1
                //};

                //_context.Add(product);
                //_context.SaveChanges();
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Product.Id == 0)
                {
                    //create

                    //location of files
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //upload image
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.Image = fileName + extension;

                    _context.Products.Add(productVM.Product);
                    
                }
                else
                {
                    //edit
                    var objFromDb = _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == productVM.Product.Id);

                    if (files.Count > 0)
                    {
                        //location of files
                        string upload = webRootPath + WC.ImagePath;
                        //string upload =  WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        //upload new image
                        //using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        using (var fileStream = new FileStream(upload + fileName + extension, FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        //if the image is modified, change filename and extension
                        productVM.Product.Image = fileName + extension;
                    }
                    //image file was not updated
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _context.Products.Update(productVM.Product);
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            productVM.CategorySelecytList = _context.Categories.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();
            return View(productVM);
        }
        //GET - DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _context.Products.Include(u => u.Category).FirstOrDefault(u => u.Id == id);
            //product.Category = _db.Category.Find(product.CategoryId);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //POST - DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }


            _context.Products.Remove(obj);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        //from https://github.com/vasiliv/ImageUpload master branch, MvcCoreUploadAndDisplayImageDemo project
        //private string UploadedFile(Product model)
        //{
        //    string uniqueFileName = null;
        //    if (model.ProfileImage != null)
        //    {
        //        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.ProfileImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}
    }
}
