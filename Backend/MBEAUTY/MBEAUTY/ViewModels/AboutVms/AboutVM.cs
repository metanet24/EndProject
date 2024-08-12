using MBEAUTY.ViewModels.FamousVms;

namespace MBEAUTY.ViewModels.AboutVms
{
    public class AboutVM
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<FamousListVM> Famous { get; set; }
    }
}
