using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("AboutU")]
public partial class AboutU
{
    [Key]
    public int Id { get; set; }

    public string? ImageTopic { get; set; }

    public string? TextTopic { get; set; }

    public string? TextDesc { get; set; }

    public string? LeftImage { get; set; }

    public string? LeftText { get; set; }

    public string? LeftUrl { get; set; }

    public string? RightImage { get; set; }

    public string? RightText { get; set; }

    public string? RightUrl { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("AboutU")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
