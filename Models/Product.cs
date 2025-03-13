using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [StringLength(450)]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue)]
        [Display(Name = "Duration in Days")]
        public int Duration { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int Price { get; set; }

        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}