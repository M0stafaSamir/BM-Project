using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JopApplication.Core.Models { 

[Table("Application")]
public partial class Application
{
    [Key]
    public int Id { get; set; }

    [StringLength(450)]
    public string UserId { get; set; } = null!;

    public int JobId { get; set; }

    public string? CoverLetter { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AppliedAt { get; set; }

    public string? Resume { get; set; }

    [ForeignKey("JobId")]
    [InverseProperty("Applications")]
    public virtual Job Job { get; set; } = null!;

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; } = null!;
    }
}