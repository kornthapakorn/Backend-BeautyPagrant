using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("Form")]
public partial class Form
{
    [Key]
    public int Id { get; set; }

    public int FormTemplateId { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("Form")]
    public virtual ICollection<FormComponent> FormComponents { get; set; } = new List<FormComponent>();

    [ForeignKey("FormTemplateId")]
    [InverseProperty("Forms")]
    public virtual FormTemplate FormTemplate { get; set; } = null!;
}
