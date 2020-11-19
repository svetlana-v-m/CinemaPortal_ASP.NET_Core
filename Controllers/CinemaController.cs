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
        public async Task<FileContentResult> GetImage(int id)
        {
            Cinema cinema = await _context.FindCinemaByIDAsync(id);
            if (cinema != null) return File(cinema.Poster, cinema.ImageMimeType);
            else return null;
        }

        [HttpGet]
        [Authorize]
        public IActionResult MyCinema(int page=1)
        {
            if(User.Identity.IsAuthenticated)
            {
                int pageSize = 3;
                var collection = _context.CinemaCollection.Where(c => c.UserName.Trim().ToUpper().Equals(User.Identity.Name.Trim().ToUpper())).ToList();
                var count = collection.Count();
                var items = collection.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pvm = new PageViewModel(count, page, pageSize,"MyCinema");
                IndexViewModel ivm = new IndexViewModel
                {
                    PageViewModel = pvm,
                    CinemaCollection = items
                };
                return View(ivm);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string listName)
        {
            Cinema cinema = await _context.FindCinemaByIDAsync(id);
            if (cinema == null) return NotFound();
            else
            {
                DetailsViewModel dvm = new DetailsViewModel { Cinema = cinema, ListName = listName };
                return View(dvm);
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated) return View();
            else return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Cinema newCinema, IFormFile poster)
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
                await _context.AddAsync(newCinema);
                await _context.SaveChangesAsync();
                                                
                return RedirectToAction("Details", new { id = newCinema.CinemaID, listName="AllCinema" });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit(int id, string listName)
        {
            Cinema c = await _context.FindCinemaByIDAsync(id);
            if (c == null) return NotFound();
            else
            {
                EditCinemaViewModel evm = new EditCinemaViewModel { Cinema = c, ListName = listName };
                return View(evm);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(Cinema cinema,IFormFile poster, string _listName)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", cinema);
            }
            else
            {
                if (poster != null)
                {
                    cinema.ImageMimeType = poster.ContentType;
                    cinema.Poster = new byte[poster.Length];
                    using (var binaryReader = new BinaryReader(poster.OpenReadStream())) cinema.Poster = binaryReader.ReadBytes((int)poster.Length);
                }
                _context.Update(cinema);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = cinema.CinemaID, listName= _listName});
            }
  
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id, string listName)
        {
            Cinema cinema = await _context.FindCinemaByIDAsync(id);
            if (cinema != null)
            {
                _context.Remove(cinema);
                await _context.SaveChangesAsync();
            }
            else return NotFound();
            if (listName == "AllCinema") return RedirectToAction("Index", "Home");
            else return RedirectToAction("MyCinema", "Cinema");
        }
    }
}