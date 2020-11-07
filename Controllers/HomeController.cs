using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CinemaPortal_ASP.NET_Core.Models;

namespace CinemaPortal_ASP.NET_Core.Controllers
{
    public class HomeController : Controller
    {
        CinemaDbContext db;

        public HomeController(CinemaDbContext context)
        {
            db = context;
            CinemaDbInitializer.Initialize(context,Startup.hostEnvironment.ContentRootPath);
        }
        public IActionResult Index()
        {
            return View(db.CinemaCollection.ToList());
        }

        
    }
}
