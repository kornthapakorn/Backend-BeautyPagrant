using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("Banner")]
public partial class Banner
{
    [Key]
    public int Id { get; set; }

    public string? Image { get; set; }

    public string? TextDesc { get; set; }

    [StringLength(50)]
    public string? TextOnButton { get; set; }

    public bool IsActive { get; set; }

    public string? UrlButton { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("Banner")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
