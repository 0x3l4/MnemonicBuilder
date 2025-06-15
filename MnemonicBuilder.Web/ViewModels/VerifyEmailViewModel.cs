using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MnemonicBuilder.Web.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Напишите свою почту.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
