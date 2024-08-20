namespace MBEAUTY.ViewModels.BasketVMs
{
    public class BasketProductAddVM
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
