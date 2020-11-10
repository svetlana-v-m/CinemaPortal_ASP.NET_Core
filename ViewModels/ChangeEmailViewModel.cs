using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPortal_ASP.NET_Core.ViewModels
{
    
    public class ChangeEmailViewModel
     {
            [Display(Name = "Текущий email")]
            public string OldEmail { get; set; }

            [Required]
            [Display(Name = "Новый email")]
            public string NewEmail { get; set; }
    }
}
