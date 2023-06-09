using System.ComponentModel.DataAnnotations;

namespace P329CodeFirstApproach.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
       
        [DataType(DataType.Password),Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set;}
    }
}
