using System.ComponentModel.DataAnnotations;

namespace MnemonicBuilder.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Укажите свою почту.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пароль обязателен.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
