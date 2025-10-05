namespace Backend_BeautyPagrant.Dto
{
    public class EventCreateDto
    {
        public string Name { get; set; } = null!;
        public bool IsFavorite { get; set; }
        public string FileImage { get; set; } = null!;

        public DateTime? EndDate { get; set; }

        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<EventComponentDto> Components { get; set; } = new List<EventComponentDto>();

    }

    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsFavorite { get; set; }
        public string FileImage { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
