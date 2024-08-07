namespace MBEAUTY.Models
{
    public class Brand : BaseEntity
    {
        public string Logo { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
