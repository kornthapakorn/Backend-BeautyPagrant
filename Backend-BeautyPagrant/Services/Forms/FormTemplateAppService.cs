using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;

namespace Backend_BeautyPagrant.Services
{
    public class FormTemplateAppService : IFormTemplateAppService
    {
        private readonly IFormComponentImageBinder _binder;

        public FormTemplateAppService(IFormComponentImageBinder binder)
        {
            _binder = binder;
        }

        public FormTemplate Create(BeautyPagrantContext context, FormTemplateDto dto, string userName,
                                   IDictionary<string, IFormFile> files)
        {
            _binder.BindMany(dto.Components, files);
            FormTemplate form = FormTemplate.CreateFromDto(context, dto, userName, baseUrl: string.Empty);
            context.SaveChanges();
            return form;
        }

        public FormTemplate Update(BeautyPagrantContext context, int id, FormTemplateDto dto, string userName,
                                   IDictionary<string, IFormFile> files)
        {
            _binder.BindMany(dto.Components, files);
            // มีเมธอด UpdateFromDto(form) ในโมเดลของคุณแล้ว
            FormTemplate form = context.FormTemplates.First(x => x.Id == id && !x.IsDelete);
            form.UpdateFromDto(context, dto, userName);
            context.SaveChanges();
            return form;
        }
    }
}
