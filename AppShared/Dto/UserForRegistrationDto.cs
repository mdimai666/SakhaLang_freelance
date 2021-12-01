using AppShared.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppShared.AuthProviders.Dto
{
    public class UserForRegistrationDto
    {
        [EmailAddress]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Email { get; set; }

        [Category("Security")]
        [Description("Account password")]
        [PasswordPropertyText(true)]
        [Required(ErrorMessageResourceName = nameof(AppRes.v_required), ErrorMessageResourceType = typeof(AppRes))]
        public string Password { get; set; }

        [Category("Security")]
        [PasswordPropertyText(true)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
