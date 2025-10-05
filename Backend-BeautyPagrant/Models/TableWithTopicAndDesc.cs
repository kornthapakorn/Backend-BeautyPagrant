using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("TableWithTopicAndDesc")]
public partial class TableWithTopicAndDesc
{
    [Key]
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? TextDesc { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("TableWithTopicAndDesc")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
