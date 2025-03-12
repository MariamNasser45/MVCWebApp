namespace ProductCatalog.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Code { get; set; }
        public Category? Category { get; set; }
        public int ? CategoryId { get; set; }
    }
}
