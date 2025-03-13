using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ViewModels
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

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

        public IEnumerable<SelectListItem>? Category { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Category")]
        public int CategoryId { get; set; } // id for selected category
    }
}
