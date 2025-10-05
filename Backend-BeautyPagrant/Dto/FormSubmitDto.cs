namespace Backend_BeautyPagrant.Dto
{
    public class FormSubmitDto
    {
        public int FormId { get; set; }
        public List<FormComponentSubmitDto> Components { get; set; } = new List<FormComponentSubmitDto>();
    }

    public class FormComponentSubmitDto
    {
        public int FormComponentId { get; set; }
        public string? Value { get; set; }
        public bool? IsActive { get; set; }
        public string? FilePath { get; set; }
    }
    public class FormSubmitCreateDto
    {
        public int FormTemplateId { get; set; }   
        public List<FormComponentSubmitCreateDto> Components { get; set; } = new List<FormComponentSubmitCreateDto>();
    }

    public class FormComponentSubmitCreateDto
    {
        public int? FormComponentId { get; set; }
        public int? FormComponentTemplateId { get; set; }
        public string ComponentType { get; set; } = string.Empty;
        public string? Value { get; set; }
        public bool? IsActive { get; set; }
        public string? FilePath { get; set; }
    }


}
