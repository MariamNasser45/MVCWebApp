using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ViewModels
{
    public class ProductDataViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
