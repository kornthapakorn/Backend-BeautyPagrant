using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;

namespace Backend_BeautyPagrant.Services
{
    public interface IFormTemplateAppService
    {
        FormTemplate Create(BeautyPagrantContext context, FormTemplateDto dto, string userName,
                            IDictionary<string, IFormFile> files);
        FormTemplate Update(BeautyPagrantContext context, int id, FormTemplateDto dto, string userName,
                            IDictionary<string, IFormFile> files);
    }
}
