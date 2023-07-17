using System.ComponentModel.DataAnnotations;

namespace P329CodeFirstApproach.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
