using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RockyMy.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Display Order must be greater than zero")]
        public int DisplayOrder { get; set; }
    }
}
