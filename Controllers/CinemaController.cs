using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CinemaPortal_ASP.NET_Core.Models;
using CinemaPortal_ASP.NET_Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CinemaPortal_ASP.NET_Core.Controllers
{
    public class CinemaController : Controller
    {
        CinemaDbContext _context;
        Cinema tempCinema = new Cinema();
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

        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated) return View();
            else return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Cinema newCinema, IFormFile poster)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newCinema);
            }
            else
            {
                if (poster != null)
                {
                    newCinema.ImageMimeType = poster.ContentType;
                    newCinema.Poster = new byte[poster.Length];
                    using (var binaryReader = new BinaryReader(poster.OpenReadStream()))
                    {
                        newCinema.Poster = binaryReader.ReadBytes((int)poster.Length);
                    }

                }
                newCinema.UserName = User.Identity.Name;
                _context.Add(newCinema);
                _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = newCinema.CinemaID });
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(string name)
        {
            Cinema c = _context.FindCinemaByTitle(name);
            if (c == null) return NotFound();
            EditCinemaViewModel evm = new EditCinemaViewModel { Cinema = c };
            return View(evm);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditCinemaViewModel model)
        {
            int tempId = model.Cinema.CinemaID;
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            else
            {
                if (model.PostedFile != null)
                {
                    model.Cinema.ImageMimeType = model.PostedFile.ContentType;
                    model.Cinema.Poster = new byte[model.PostedFile.Length];
                    using (var binaryReader = new BinaryReader(model.PostedFile.OpenReadStream())) model.Cinema.Poster = binaryReader.ReadBytes((int)model.PostedFile.Length);
                }
                Cinema _cinema = _context.FindCinemaByID(model.Cinema.CinemaID);
                _cinema.Description = model.Cinema.Description;
                _cinema.FilmMaker = model.Cinema.FilmMaker;
                _cinema.Name = model.Cinema.Name;
                _cinema.UserName = model.Cinema.UserName;
                _cinema.Year = model.Cinema.Year;
                _cinema.ImageMimeType = model.Cinema.ImageMimeType;
                _cinema.Poster = model.Cinema.Poster;
                _context.SaveChangesAsync();
                
                return RedirectToAction("Details", new { cinema = _context.FindCinemaByID(tempId) });
            }
  
        }

    }
}