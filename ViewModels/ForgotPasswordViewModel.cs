using System.ComponentModel.DataAnnotations;

namespace CinemaPortal_ASP.NET_Core.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
