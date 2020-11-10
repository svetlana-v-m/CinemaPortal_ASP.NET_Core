using CinemaPortal_ASP.NET_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaPortal_ASP.NET_Core.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Cinema> CinemaCollection { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
