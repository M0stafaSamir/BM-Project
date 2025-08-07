using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JopApplication.Core.Models { 

[Table("Job")]
[Index("Id", Name = "IX_Job", IsUnique = true)]
public partial class Job
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string? Location { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Salary { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PostedAt { get; set; }

    [StringLength(450)]
    public string CreatedById { get; set; } = null!;

    public int? Applied { get; set; }

    [StringLength(100)]
    public string Company { get; set; } = null!;

    [InverseProperty("Job")]
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    [ForeignKey("CreatedById")]
    public ApplicationUser CreatedBy { get; set; } = null!;

    }
}