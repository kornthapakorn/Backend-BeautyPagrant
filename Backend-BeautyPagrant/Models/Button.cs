using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("Button")]
public partial class Button
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? TextOnButton { get; set; }

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

    [InverseProperty("Button")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
