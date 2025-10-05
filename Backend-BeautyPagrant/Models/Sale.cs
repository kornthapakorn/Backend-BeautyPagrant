using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("Sale")]
public partial class Sale
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Text { get; set; }

    public int? PromoPrice { get; set; }

    public int? Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    public string? TextDesc { get; set; }

    public string? TextOnButton { get; set; }

    public string? TextFooter { get; set; }

    public string? LeftImage { get; set; }

    public string? LeftText { get; set; }

    public string? RightImage { get; set; }

    public string? RightText { get; set; }

    public bool IsActive { get; set; }

    public string? Url { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("Sale")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
