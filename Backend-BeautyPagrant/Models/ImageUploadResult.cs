using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("ImageUploadResult")]
public partial class ImageUploadResult
{
    [Key]
    public int Id { get; set; }

    public string FilePath { get; set; } = null!;

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("ImageUploadResult")]
    public virtual ICollection<FormComponent> FormComponents { get; set; } = new List<FormComponent>();
}
