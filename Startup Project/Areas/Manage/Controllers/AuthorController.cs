using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Startup_Project.Data_Access_Layer;
using Startup_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Startup_Project.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private AppDbContext _context { get; }
        public IWebHostEnvironment _env { get; }
        public AuthorController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Authors.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid) return View(author);
            if (author.ImageUrl != null)
            {
                if (author.ImageUrl.ContentType != "image/jpeg" && author.ImageUrl.ContentType != "image/png" && author.ImageUrl.ContentType != "image/wepb")
                {
                    ModelState.AddModelError("", "Faylin tipi png ve ya jpeg olmalidir");
                    return View(author);
                }
                if (author.ImageUrl.Length / 1024 > 3000)
                {
                    ModelState.AddModelError("", "Faylin olcusu max 3mb ola biler");
                    return View(author);
                }
                string filename = author.ImageUrl.FileName;
                if (filename.Length > 64)
                {
                    filename.Substring(filename.Length - 64, 64);

                }
                string newFileName = Guid.NewGuid().ToString() + filename;
                string path = Path.Combine(_env.WebRootPath, "assets", "images", newFileName);
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    author.ImageUrl.CopyTo(fs);
                }
                author.Image = newFileName;
                _context.Authors.Add(author);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
           Author author = _context.Authors.Find(id);
            if (author == null) return NotFound();
            if (System.IO.File.Exists(Path.Combine(_env.WebRootPath, "assets", "images", author.Image)))
                System.IO.File.Delete(Path.Combine(_env.WebRootPath, "assets", "images", author.Image));
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Author author = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null) return NotFound();
            return View(author);
        }
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            Author author1 = _context.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (author == null) return NotFound();
            if (author.ImageUrl != null)
            {
                if (author.ImageUrl.ContentType != "image/jpeg" && author.ImageUrl.ContentType != "image/png" && author.ImageUrl.ContentType != "image/wepb")
                {
                    ModelState.AddModelError("", "Faylin tipi png ve ya jpeg olmalidir");
                    return View(author);
                }
                if (author.ImageUrl.Length / 1024 > 3000)
                {
                    ModelState.AddModelError("", "Faylin olcusu max 3mb ola biler");
                    return View(author);
                }
                string filename = author.ImageUrl.FileName;
                if (filename.Length > 64)
                {
                    filename.Substring(filename.Length - 64, 64);

                }
                string newFileName = Guid.NewGuid().ToString() + filename;
                string path = Path.Combine(_env.WebRootPath, "assets", "images", newFileName);
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    author.ImageUrl.CopyTo(fs);
                }
                author1.Image = newFileName;
                author1.Fullname = author.Fullname;
                author1.Description = author.Description;
                author1.SpecialtyName = author.SpecialtyName;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
