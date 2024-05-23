using System.ComponentModel.DataAnnotations;

namespace SpacDnya.ViewModels.Account
{
    public class RegisterVM
    {
        [Required, MinLength(3)]
        public string Name {  get; set; }
        [Required, MinLength(7)]
        public string Surname {  get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]

        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string RepeatPassword { get; set; }

    }
}