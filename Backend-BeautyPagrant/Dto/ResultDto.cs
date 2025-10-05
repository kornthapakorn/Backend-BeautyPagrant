namespace Backend_BeautyPagrant.Dto
{
    public class TextFieldResultCreateDto
    {
        public string Value { get; set; } = string.Empty;
    }

    public class DateResultCreateDto
    {
        public DateTime Value { get; set; }
    }

    public class BirthDateResultCreateDto
    {
        public DateTime Value { get; set; }
    }

    public class SingleSelectionResultCreateDto
    {
        public bool IsActive { get; set; }
    }

    public class ImageUploadResultCreateDto
    {
        public string FilePath { get; set; } = string.Empty;
    }

    public class ImageUploadWithImageContentResultCreateDto
    {
        public string FilePath { get; set; } = string.Empty;
    }
    //
    public class TextFieldResultDto
    {
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
    }

    public class DateResultDto
    {
        public int Id { get; set; }
        public DateTime Value { get; set; }
    }

    public class BirthDateResultDto
    {
        public int Id { get; set; }
        public DateTime Value { get; set; }
    }

    public class SingleSelectionResultDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }

    public class ImageUploadResultDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }

    public class ImageUploadWithImageContentResultDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}
