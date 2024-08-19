using MBEAUTY.Models;

namespace MBEAUTY.ViewModels.BlogVMs
{
    public class BlogDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<BlogImage> BlogImages { get; set; }
        public IEnumerable<BlogListVM> Blogs { get; set; }
    }
}
