using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Services
{
    public interface IFormComponentImageBinder
    {
        void Bind(FormComponentTemplateDto dto, IDictionary<string, IFormFile> files);
        void BindMany(IList<FormComponentTemplateDto> dtos, IDictionary<string, IFormFile> files);
    }
}
