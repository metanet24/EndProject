namespace MBEAUTY.Models
{
    public class Blog : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BlogImage> BlogImages { get; set; }
    }
}
