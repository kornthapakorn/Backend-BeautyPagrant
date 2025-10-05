using Backend_BeautyPagrant.Models;

namespace Backend_BeautyPagrant.Dto
{
    public class FormComponentTemplateDto
    {
        public int Id { get; set; }

        public int FormTemplateId { get; set; }   

        public bool IsDelete { get; set; }
        public string ComponentType { get; set; } = null!;

        public SingleSelectionDto? SingleSelection { get; set; }
        public TextFieldDto? TextField { get; set; }
        public DateDto? Date { get; set; }
        public BirthDateDto? BirthDate { get; set; }
        public ImageUploadDto? ImageUpload { get; set; }
        public ImageUploadWithImageContentDto? ImageUploadWithImageContent { get; set; }
        public FormButtonDto? FormButton { get; set; }
    }


    public class SingleSelectionDto { public string? Value { get; set; } }
    public class TextFieldDto { public string? Text { get; set; } }
    public class DateDto { public string? Text { get; set; } }
    public class BirthDateDto { public string? Label { get; set; } }
    public class ImageUploadDto { public string? Text { get; set; } }
    public class ImageUploadWithImageContentDto { public string? TextDesc { get; set; } public string? Image { get; set; } }
    public class FormButtonDto { public string? TextOnButton { get; set; } public bool IsActive { get; set; } public string? Url { get; set; } }
}
