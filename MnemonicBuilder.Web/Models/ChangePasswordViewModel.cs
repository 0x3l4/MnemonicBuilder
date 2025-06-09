using System.ComponentModel.DataAnnotations;

namespace MnemonicBuilder.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Укажите свою почту.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Пароль обязателен.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Пароль должен содержать от 8 до 40 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Введите новый пароль.")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Пароль не совпадает.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Подтверждение пароля обязателено.")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите новый пароль.")]
        public string ConfirmNewPassword { get; set; }
    }
}
