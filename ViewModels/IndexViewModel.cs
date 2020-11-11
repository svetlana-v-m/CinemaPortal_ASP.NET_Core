using CinemaPortal_ASP.NET_Core.Models;
using System.Collections.Generic;

namespace CinemaPortal_ASP.NET_Core.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Cinema> CinemaCollection { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
