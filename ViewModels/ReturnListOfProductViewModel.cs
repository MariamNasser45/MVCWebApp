namespace ProductCatalog.ViewModels
{
    public class ReturnListOfProductViewModel
    {
        public string Messege { get; set; } = string.Empty;
        public List<ProductDataViewModel>? ProductData { get; set; }
    }
}
