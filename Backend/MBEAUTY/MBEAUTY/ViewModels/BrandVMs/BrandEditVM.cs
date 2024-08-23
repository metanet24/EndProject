namespace MBEAUTY.ViewModels.BrandVMs
{
    public class BrandEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public IFormFile Photo { get; set; }
    }
}
