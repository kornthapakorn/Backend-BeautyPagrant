using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("Event")]
public partial class Event
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsFavorite { get; set; }

    public string FileImage { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? EndDate { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("Event")]
    public virtual ICollection<EventCategorize> EventCategorizes { get; set; } = new List<EventCategorize>();

    [InverseProperty("Event")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
