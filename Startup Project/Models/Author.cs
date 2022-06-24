using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Startup_Project.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required,MaxLength(150)]
        public string Fullname { get; set; }
        [Required]
        public string SpecialtyName { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageUrl { get; set; }
    }
}
