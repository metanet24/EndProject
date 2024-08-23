namespace MBEAUTY.ViewModels.BasketVMs
{
    public class BasketListVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
