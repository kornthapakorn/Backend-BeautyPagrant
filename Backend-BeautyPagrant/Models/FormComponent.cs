using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("FormComponent")]
public partial class FormComponent
{
    [Key]
    public int Id { get; set; }

    public int FormId { get; set; }

    public int FormComponentTemplateId { get; set; }

    [StringLength(50)]
    public string ComponentType { get; set; } = null!;

    public int? SingleSelectionResultId { get; set; }

    public int? TextFieldResultId { get; set; }

    public int? DateResultId { get; set; }

    public int? BirthDateResultId { get; set; }

    public int? ImageUploadResultId { get; set; }

    public int? ImageUploadWithImageContentResultId { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("BirthDateResultId")]
    [InverseProperty("FormComponents")]
    public virtual BirthDateResult? BirthDateResult { get; set; }

    [ForeignKey("DateResultId")]
    [InverseProperty("FormComponents")]
    public virtual DateResult? DateResult { get; set; }

    [ForeignKey("FormId")]
    [InverseProperty("FormComponents")]
    public virtual Form Form { get; set; } = null!;

    [ForeignKey("FormComponentTemplateId")]
    [InverseProperty("FormComponents")]
    public virtual FormComponentTemplate FormComponentTemplate { get; set; } = null!;

    [ForeignKey("ImageUploadResultId")]
    [InverseProperty("FormComponents")]
    public virtual ImageUploadResult? ImageUploadResult { get; set; }

    [ForeignKey("ImageUploadWithImageContentResultId")]
    [InverseProperty("FormComponents")]
    public virtual ImageUploadWithImageContentResult? ImageUploadWithImageContentResult { get; set; }

    [ForeignKey("SingleSelectionResultId")]
    [InverseProperty("FormComponents")]
    public virtual SingleSelectionResult? SingleSelectionResult { get; set; }

    [ForeignKey("TextFieldResultId")]
    [InverseProperty("FormComponents")]
    public virtual TextFieldResult? TextFieldResult { get; set; }
}
