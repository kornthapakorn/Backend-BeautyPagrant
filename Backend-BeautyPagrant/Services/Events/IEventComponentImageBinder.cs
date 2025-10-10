using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Services
{
    public interface IEventComponentImageBinder
    {
        void Bind(EventCreateDto dto, IDictionary<string, IFormFile> files);
        void BindMany(IList<EventComponentDto> components, IDictionary<string, IFormFile> files);
    }
}
