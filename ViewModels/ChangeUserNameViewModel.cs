using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
