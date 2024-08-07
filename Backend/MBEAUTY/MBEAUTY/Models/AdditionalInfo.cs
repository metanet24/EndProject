namespace MBEAUTY.Models
{
    public class AdditionalInfo : BaseEntity
    {
        public string SkinType { get; set; }
        public string Shades { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
