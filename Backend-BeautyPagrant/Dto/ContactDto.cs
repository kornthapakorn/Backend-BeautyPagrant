namespace Backend_BeautyPagrant.Dto
{
    public class ContactDto
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<ContactPictureDto> Pictures { get; set; } = new();
    }

    public class ContactPictureDto
    {
        public int? Id { get; set; }
        public string ImageId { get; set; } = string.Empty; 
        public string Url { get; set; } = string.Empty;
    }
}
