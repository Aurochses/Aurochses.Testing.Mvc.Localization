using System.ComponentModel.DataAnnotations;

namespace Aurochses.Testing.Mvc.Localization.Tests.Fakes.ModelLocalizationAssert.Validate.Models
{
    public class HomeViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Email.Prompt", Description = "Email.Description")]
        public string Email { get; set; }

        public string Token { get; set; }
    }
}