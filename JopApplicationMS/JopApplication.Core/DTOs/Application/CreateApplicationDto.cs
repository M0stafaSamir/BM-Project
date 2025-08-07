using JopApplication.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace JopApplication.Core.DTOs.Application
{
    public class CreateApplicationDto
    {
     

    

        public int JobId { get; set; }

        public string? CoverLetter { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AppliedAt { get; set; } = DateTime.Now;


        public IFormFile ResumeFile { get; set; } = null!;


    }
}

