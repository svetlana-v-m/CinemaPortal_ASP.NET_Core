using System.ComponentModel.DataAnnotations;

namespace CinemaPortal_ASP.NET_Core.ViewModels
{

    public class ChangeUserNameViewModel
     {
            [Display(Name = "Текущее имя")]
            public string OldUserName { get; set; }

            [Required]
            [Display(Name = "Новое имя")]
            public string NewUserName { get; set; }
    }
}
