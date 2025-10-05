using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("GridTwoColumn")]
public partial class GridTwoColumn
{
    [Key]
    public int Id { get; set; }

    public string? LeftImage { get; set; }

    public string? LeftText { get; set; }

    public string? LeftUrl { get; set; }

    public string? RightImage { get; set; }

    public string? RightText { get; set; }

    public string? RightUrl { get; set; }

    [StringLength(50)]
    public string Createby { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("GridTwoColumn")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
