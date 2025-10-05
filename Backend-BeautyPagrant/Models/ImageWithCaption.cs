using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("ImageWithCaption")]
public partial class ImageWithCaption
{
    [Key]
    public int Id { get; set; }

    public string? Image { get; set; }

    public string? Text { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("ImageWithCaption")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
