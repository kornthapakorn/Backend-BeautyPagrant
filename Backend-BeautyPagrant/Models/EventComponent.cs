using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

[Table("EventComponent")]
public partial class EventComponent
{
    [Key]
    public int Id { get; set; }

    public int EventId { get; set; }

    [StringLength(50)]
    public string ComponentType { get; set; } = null!;

    public int? SectionId { get; set; }

    public int? BannerId { get; set; }

    public int? TextboxId { get; set; }

    public int? ImageWithCaptionId { get; set; }

    public int? GridTwoColumnId { get; set; }

    public int? ImageDescId { get; set; }

    public int? GridFourImageId { get; set; }

    public int? TableWithTopicAndDescId { get; set; }

    public int? OneTopicImageCationButtonId { get; set; }

    public int? TwoTopicImageCationButtonId { get; set; }

    public int? SaleId { get; set; }

    public int? ButtonId { get; set; }

    public int? AboutUId { get; set; }

    public int? FormTemplateId { get; set; }

    public int SortOrder { get; set; }

    public bool IsOutPage { get; set; }

    [StringLength(50)]
    public string CreateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [StringLength(50)]
    public string UpdateBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }

    [ForeignKey("AboutUId")]
    [InverseProperty("EventComponents")]
    public virtual AboutU? AboutU { get; set; }

    [ForeignKey("BannerId")]
    [InverseProperty("EventComponents")]
    public virtual Banner? Banner { get; set; }

    [ForeignKey("ButtonId")]
    [InverseProperty("EventComponents")]
    public virtual Button? Button { get; set; }

    [ForeignKey("EventId")]
    [InverseProperty("EventComponents")]
    public virtual Event Event { get; set; } = null!;

    [ForeignKey("FormTemplateId")]
    [InverseProperty("EventComponents")]
    public virtual FormTemplate? FormTemplate { get; set; }

    [ForeignKey("GridFourImageId")]
    [InverseProperty("EventComponents")]
    public virtual GridFourImage? GridFourImage { get; set; }

    [ForeignKey("GridTwoColumnId")]
    [InverseProperty("EventComponents")]
    public virtual GridTwoColumn? GridTwoColumn { get; set; }

    [ForeignKey("ImageDescId")]
    [InverseProperty("EventComponents")]
    public virtual ImageDesc? ImageDesc { get; set; }

    [ForeignKey("ImageWithCaptionId")]
    [InverseProperty("EventComponents")]
    public virtual ImageWithCaption? ImageWithCaption { get; set; }

    [ForeignKey("OneTopicImageCationButtonId")]
    [InverseProperty("EventComponents")]
    public virtual OneTopicImageCaptionButton? OneTopicImageCationButton { get; set; }

    [ForeignKey("SaleId")]
    [InverseProperty("EventComponents")]
    public virtual Sale? Sale { get; set; }

    [ForeignKey("SectionId")]
    [InverseProperty("EventComponents")]
    public virtual Section? Section { get; set; }

    [ForeignKey("TableWithTopicAndDescId")]
    [InverseProperty("EventComponents")]
    public virtual TableWithTopicAndDesc? TableWithTopicAndDesc { get; set; }

    [ForeignKey("TextboxId")]
    [InverseProperty("EventComponents")]
    public virtual TextBox? Textbox { get; set; }

    [ForeignKey("TwoTopicImageCationButtonId")]
    [InverseProperty("EventComponents")]
    public virtual TwoTopicImageCaptionButton? TwoTopicImageCationButton { get; set; }
}
