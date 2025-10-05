using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("ContactPicture")]
public partial class ContactPicture
{
    [Key]
    public int Id { get; set; }

    public string? Image { get; set; }

    public string? Url { get; set; }

    public int? ContactId { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("ContactId")]
    [InverseProperty("ContactPictures")]
    public virtual Contact? Contact { get; set; }
}
