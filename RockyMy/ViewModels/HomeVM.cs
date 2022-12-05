using Newtonsoft.Json.Linq;
using RockyMy.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace RockyMy.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
