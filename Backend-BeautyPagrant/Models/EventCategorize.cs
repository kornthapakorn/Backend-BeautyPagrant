using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("EventCategorize")]
public partial class EventCategorize
{
    [Key]
    public int Id { get; set; }

    public int EventId { get; set; }

    public int CategoryId { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("EventCategorizes")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("EventId")]
    [InverseProperty("EventCategorizes")]
    public virtual Event Event { get; set; } = null!;
}
