using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CinemaPortal_ASP.NET_Core.Models;
using Microsoft.EntityFrameworkCore;
using CinemaPortal_ASP.NET_Core.ViewModels;

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
        public async Task<IActionResult> Index(int page=1)
        {
            int pageSize = 3;
            var collection = await db.CinemaCollection.ToListAsync();
            var count = collection.Count();
            var items = collection.Skip((page - 1) + pageSize).Take(pageSize).ToList();
            PageViewModel pvm = new PageViewModel(count, page, pageSize);
            IndexViewModel ivm = new IndexViewModel
            {
                PageViewModel = pvm,
                CinemaCollection = items
            };
            return View(ivm);
        }

        
    }
}
