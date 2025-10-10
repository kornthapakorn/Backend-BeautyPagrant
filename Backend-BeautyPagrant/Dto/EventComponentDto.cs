namespace Backend_BeautyPagrant.Dto
{
    public class EventComponentDto
    {
        public int Id { get; set; }
        public string ComponentType { get; set; } = null!;
        public int SortOrder { get; set; }
        public bool IsOutPage { get; set; }

        public AboutUDto? AboutU { get; set; }
        public BannerDto? Banner { get; set; }
        public ButtonDto? Button { get; set; }
        public FormTemplateDto? FormTemplate { get; set; }
        public GridFourImageDto? GridFourImage { get; set; }
        public GridTwoColumnDto? GridTwoColumn { get; set; }
        public ImageDescDto? ImageDesc { get; set; }
        public ImageWithCaptionDto? ImageWithCaption { get; set; }
        public OneTopicImageCaptionButtonDto? OneTopicImageCaptionButton { get; set; }
        public SaleDto? Sale { get; set; }
        public SectionDto? Section { get; set; }
        public TableWithTopicAndDescDto? TableWithTopicAndDesc { get; set; }
        public TextBoxDto? TextBox { get; set; }
        public TwoTopicImageCaptionButtonDto? TwoTopicImageCaptionButton { get; set; }
    }
    public class AboutUDto
    {
        public string? ImageTopic { get; set; }
        public string? TextTopic { get; set; }
        public string? TextDesc { get; set; }
        public string? LeftImage { get; set; }
        public string? LeftText { get; set; }
        public string? LeftUrl { get; set; }
        public string? RightImage { get; set; }
        public string? RightText { get; set; }
        public string? RightUrl { get; set; }
    }

    public class BannerDto
    {
        public string? Image { get; set; }
        public string? TextDesc { get; set; }
        public string? TextOnButton { get; set; }
        public bool IsActive { get; set; }
        public string? UrlButton { get; set; }
    }

    public class ButtonDto
    {
        public string? TextOnButton { get; set; }
        public bool IsActive { get; set; }
        public string? Url { get; set; }
    }

    public class FormTemplateDto
    {
        public string Topic { get; set; } = null!;
        public string? TextOnButton { get; set; }
        public string? PopupImage { get; set; }
        public string? PopupText { get; set; }

        public List<FormComponentTemplateDto> Components { get; set; } = new();

    }

    public class GridFourImageDto
    {
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
    }

    public class GridTwoColumnDto
    {
        public string? LeftImage { get; set; }
        public string? LeftText { get; set; }
        public string? LeftUrl { get; set; }
        public string? RightImage { get; set; }
        public string? RightText { get; set; }
        public string? RightUrl { get; set; }
    }

    public class ImageDescDto
    {
        public string? Image { get; set; }
        public string? Text { get; set; }
    }

    public class ImageWithCaptionDto
    {
        public string? Image { get; set; }
        public string? Text { get; set; }
    }

    public class OneTopicImageCaptionButtonDto
    {
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? TextDesc { get; set; }
        public string? TextOnButton { get; set; }
        public bool IsActive { get; set; }
        public string? Url { get; set; }
    }

    public class SaleDto
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public int? PromoPrice { get; set; }
        public int? Price { get; set; }
        public DateTime? EndDate { get; set; }
        public string? TextDesc { get; set; }
        public string? TextOnButton { get; set; }
        public string? TextFooter { get; set; }
        public string? LeftImage { get; set; }
        public string? LeftText { get; set; }
        public string? RightImage { get; set; }
        public string? RightText { get; set; }
        public bool IsActive { get; set; }
        public string? Url { get; set; }
    }

    public class SectionDto 
    {
        public List<EventComponentDto> Components { get; set; } = new List<EventComponentDto>();
    }

    public class TableWithTopicAndDescDto
    {
        public string? Title { get; set; }
        public string? TextDesc { get; set; }
    }

    public class TextBoxDto
    {
        public string? Text { get; set; }
    }

    public class TwoTopicImageCaptionButtonDto
    {
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
    }
}
