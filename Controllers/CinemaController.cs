using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaPortal_ASP.NET_Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaPortal_ASP.NET_Core.Controllers
{
    public class CinemaController : Controller
    {
        CinemaDbContext _context;
        Cinema tempCinema=new Cinema();
        public CinemaController(CinemaDbContext context) 
        {
            _context = context;
        }
        public FileContentResult GetImage(string name)
        {
            Cinema cinema = _context.FindCinemaByTitle(name);

            if (cinema != null) return File(cinema.Poster, cinema.ImageMimeType);
            else return null;
        }
        
        public IActionResult Details(Cinema cinema)
        {
            return View(cinema);
        }
        
     }
}