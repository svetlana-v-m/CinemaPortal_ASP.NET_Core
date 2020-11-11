using CinemaPortal_ASP.NET_Core.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace CinemaPortal_ASP.NET_Core.ViewModels
{
    public class EditCinemaViewModel
    {
        public Cinema Cinema { get; set; }
        [DisplayName("Постер:")]
        public IFormFile PostedFile { get; set; }
        public string ListName { get; set; }
    }
}
