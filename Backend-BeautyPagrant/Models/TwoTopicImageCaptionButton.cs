using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("TwoTopicImageCaptionButton")]
public partial class TwoTopicImageCaptionButton
{
    [Key]
    public int Id { get; set; }

    public string? LeftTitle { get; set; }

    public string? LeftImage { get; set; }

    public string? LeftTextDesc { get; set; }

    public string? LeftTextOnButton { get; set; }

    public bool LeftIsActive { get; set; }

    public string? LeftUrl { get; set; }

    public string? RightTitle { get; set; }

    public string? RightImage { get; set; }

    public string? RightTextDesc { get; set; }

    public string? RightTextOnButton { get; set; }

    public bool RightIsActive { get; set; }

    public string? RightUrl { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [InverseProperty("TwoTopicImageCationButton")]
    public virtual ICollection<EventComponent> EventComponents { get; set; } = new List<EventComponent>();
}
