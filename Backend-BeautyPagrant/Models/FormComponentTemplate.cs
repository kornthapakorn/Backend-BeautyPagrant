using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("FormComponentTemplate")]
public partial class FormComponentTemplate
{
    [Key]
    public int Id { get; set; }

    public int FormId { get; set; }

    [StringLength(50)]
    public string ComponentType { get; set; } = null!;

    public int? SingleSelectionId { get; set; }

    public int? TextField { get; set; }

    public int? DateId { get; set; }

    public int? BirthDateId { get; set; }

    public int? ImageUploadId { get; set; }

    public int? ImageUploadWithImageContentId { get; set; }

    public int? FormButtonId { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("BirthDateId")]
    [InverseProperty("FormComponentTemplates")]
    public virtual BirthDate? BirthDate { get; set; }

    [ForeignKey("DateId")]
    [InverseProperty("FormComponentTemplates")]
    public virtual Date? Date { get; set; }

    [ForeignKey("FormId")]
    [InverseProperty("FormComponentTemplates")]
    public virtual FormTemplate Form { get; set; } = null!;

    [ForeignKey("FormButtonId")]
    [InverseProperty("FormComponentTemplates")]
    public virtual FormButton? FormButton { get; set; }

    [InverseProperty("FormComponentTemplate")]
    public virtual ICollection<FormComponent> FormComponents { get; set; } = new List<FormComponent>();

    [ForeignKey("ImageUploadId")]
    [InverseProperty("FormComponentTemplates")]
    public virtual ImageUpload? ImageUpload { get; set; }

    [ForeignKey("ImageUploadWithImageContentId")]
    [InverseProperty("FormComponentTemplates")]
    public virtual ImageUploadWithImageContent? ImageUploadWithImageContent { get; set; }

    [ForeignKey("SingleSelectionId")]
    [InverseProperty("FormComponentTemplates")]
    public virtual SingleSelection? SingleSelection { get; set; }

    [ForeignKey("TextField")]
    [InverseProperty("FormComponentTemplates")]
    public virtual TextField? TextFieldNavigation { get; set; }
}
