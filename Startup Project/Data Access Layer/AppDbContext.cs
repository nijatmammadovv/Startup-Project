using Microsoft.EntityFrameworkCore;
using Startup_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Startup_Project.Data_Access_Layer
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
    }
}
