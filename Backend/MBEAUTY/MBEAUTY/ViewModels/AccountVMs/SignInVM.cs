using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.AccountVMs
{
    public class SignInVM
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
