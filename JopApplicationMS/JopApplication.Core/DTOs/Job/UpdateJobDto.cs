using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Core.DTOs.Job
{
    public class UpdateJobDto
    {
        [StringLength(100)]
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [StringLength(100)]
        public string? Location { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }


        [StringLength(100)]
        [Required]
        public string Company { get; set; } = null!;
    }
}
