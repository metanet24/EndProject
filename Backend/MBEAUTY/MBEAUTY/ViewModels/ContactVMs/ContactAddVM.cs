using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.ContactVMs
{
    public class ContactAddVM
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
