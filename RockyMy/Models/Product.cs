using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockyMy.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        
        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile ProfileImage { get; set; }

        //navigation properties
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
