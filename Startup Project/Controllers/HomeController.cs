using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Startup_Project.Data_Access_Layer;
using Startup_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Startup_Project.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Authors.ToList());
        }
        public List<Author> Authors { get; set; }
    }
}
