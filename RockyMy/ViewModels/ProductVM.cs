using Microsoft.AspNetCore.Mvc.Rendering;
using RockyMy.Models;
using System.Collections.Generic;

namespace RockyMy.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategorySelecytList { get; set; }
    }
}
