using CinemaPortal_ASP.NET_Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPortal_ASP.NET_Core.ViewModels
{
    public class ManageAccountViewModel
    {
        public bool HasPassword { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        
    }
}
