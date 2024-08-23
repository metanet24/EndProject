using MBEAUTY.ViewModels.FamousVms;

namespace MBEAUTY.ViewModels.AboutVms
{
    public class AboutVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<FamousListVM> Famous { get; set; }
    }
}
