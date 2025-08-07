using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Core.DTOs.Application
{
    public class GetApplicationDto
    {
        public int Id { get; set; }

        public string Applicant { get; set; } 

        public string JobTitle { get; set; }

        public int JobId { get; set; }



        public string? CoverLetter { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AppliedAt { get; set; } = DateTime.Now;

        public string? Resume { get; set; }
    }
}
