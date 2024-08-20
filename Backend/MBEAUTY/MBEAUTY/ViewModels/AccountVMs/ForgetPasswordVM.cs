using System.ComponentModel.DataAnnotations;

namespace MBEAUTY.ViewModels.AccountVMs
{
    public class ForgetPasswordVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
