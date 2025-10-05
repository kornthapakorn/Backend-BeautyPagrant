using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("FormTemplate")]
public partial class FormTemplate
{
    [Key]
    public int Id { get; set; }

    public string? PublicUrl { get; set; }

    public string Topic { get; set; } = null!;

    public string? TextOnButton { get; set; }

    public string? PopupImage { get; set; }

    public string? PopupText { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("FormTemplate")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();

    [InverseProperty("Form")]
    public virtual ICollection<FormComponentTemplate> FormComponentTemplates { get; set; } = new List<FormComponentTemplate>();

    [InverseProperty("FormTemplate")]
    public virtual ICollection<Form> Forms { get; set; } = new List<Form>();
}
