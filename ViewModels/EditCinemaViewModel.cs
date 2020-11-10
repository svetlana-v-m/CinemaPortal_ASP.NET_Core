using CinemaPortal_ASP.NET_Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPortal_ASP.NET_Core.ViewModels
{
    public class EditCinemaViewModel
    {
        public Cinema Cinema { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        [DisplayName("Постер:")]
        public IFormFile PostedFile { get; set; }
    }
}
