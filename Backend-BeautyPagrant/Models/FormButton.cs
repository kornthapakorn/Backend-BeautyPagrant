using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("FormButton")]
public partial class FormButton
{
    [Key]
    public int Id { get; set; }

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

    [InverseProperty("FormButton")]
    public virtual ICollection<FormComponentTemplate> FormComponentTemplates { get; set; } = new List<FormComponentTemplate>();
}
