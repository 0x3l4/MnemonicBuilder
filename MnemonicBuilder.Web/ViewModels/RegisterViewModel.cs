using System.ComponentModel.DataAnnotations;

namespace MnemonicBuilder.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Имя обязательно.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите почту.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пароль обязателен.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Пароль должен содержать от 8 до 40 символов")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Пароль не совпадает.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Подтверждение пароля обязателено.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }
}
