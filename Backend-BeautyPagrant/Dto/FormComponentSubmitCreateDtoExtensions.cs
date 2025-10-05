namespace Backend_BeautyPagrant.Dto
{
    public static class FormComponentSubmitCreateDtoExtensions
    {
        public static TextFieldResultCreateDto ToTextFieldResultCreateDto(this FormComponentSubmitCreateDto dto)
        {
            return new TextFieldResultCreateDto
            {
                Value = dto.Value ?? string.Empty
            };
        }

        public static DateResultCreateDto ToDateResultCreateDto(this FormComponentSubmitCreateDto dto)
        {
            return new DateResultCreateDto
            {
                Value = DateTime.TryParse(dto.Value, out DateTime parsed)
                        ? parsed
                        : DateTime.Now
            };
        }

        public static BirthDateResultCreateDto ToBirthDateResultCreateDto(this FormComponentSubmitCreateDto dto)
        {
            return new BirthDateResultCreateDto
            {
                Value = DateTime.TryParse(dto.Value, out DateTime parsed)
                        ? parsed
                        : DateTime.Now
            };
        }

        public static SingleSelectionResultCreateDto ToSingleSelectionResultCreateDto(this FormComponentSubmitCreateDto dto)
        {
            return new SingleSelectionResultCreateDto
            {
                IsActive = dto.IsActive ?? false
            };
        }

        public static ImageUploadResultCreateDto ToImageUploadResultCreateDto(this FormComponentSubmitCreateDto dto)
        {
            return new ImageUploadResultCreateDto
            {
                FilePath = dto.FilePath ?? string.Empty
            };
        }

        public static ImageUploadWithImageContentResultCreateDto ToImageUploadWithImageContentResultCreateDto(this FormComponentSubmitCreateDto dto)
        {
            return new ImageUploadWithImageContentResultCreateDto
            {
                FilePath = dto.FilePath ?? string.Empty
            };
        }
    }
}
