using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Core.DTOs.Job
{
    public class GetJobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Email { get; set; } = null!;

        public string? Location { get; set; }

        public decimal? Salary { get; set; }

   
        public DateTime? PostedAt { get; set; } 

       
        public string? CreatedByName { get; set; } 

        public int? Applied { get; set; }

        public string Company { get; set; } 
    }
}
