using MBEAUTY.Models;

namespace MBEAUTY.ViewModels.ProductImageVMs
{
    public class ProductImageDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
