using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class FormTemplate 
    {
        public static FormTemplate CreateFromDto(BeautyPagrantContext context,FormTemplateDto dto,string userName,string baseUrl)
        {
            FormTemplate formTemplate = new FormTemplate
            {
                Topic = dto.Topic,
                TextOnButton = dto.TextOnButton,
                PopupImage = dto.PopupImage,
                PopupText = dto.PopupText,
                PublicUrl = baseUrl.TrimEnd('/') + "/form/" + Guid.NewGuid().ToString("N")
            }.WithCreateAudit(userName);

            context.FormTemplates.Add(formTemplate);
            context.SaveChanges();

            if (dto.Components != null)
            {
                FormComponentTemplate.Create(context, formTemplate, dto.Components, userName);
                context.SaveChanges();
            }

            return formTemplate;
        }

        public void UpdateFromDto(BeautyPagrantContext context, FormTemplateDto dto, string userName)
        {
            this.Topic = dto.Topic;
            this.TextOnButton = dto.TextOnButton;
            this.PopupImage = dto.PopupImage;
            this.PopupText = dto.PopupText;
            this.WithUpdateAudit(userName);

        }
        public FormTemplate Duplicate(BeautyPagrantContext context, string userName)
        {
            FormTemplate copy = new FormTemplate
            {
                PublicUrl = this.PublicUrl,
                Topic = this.Topic,
                TextOnButton = this.TextOnButton,
                PopupImage = this.PopupImage,
                PopupText = this.PopupText,
            }.WithCreateAudit(userName);

            copy.Id = 0;

            context.FormTemplates.Add(copy);
            context.SaveChanges();

            foreach (FormComponentTemplate originalChild in this.FormComponentTemplates.Where(fc => !fc.IsDelete))
            {
                FormComponentTemplate.Duplicate(context, copy, originalChild, userName);
            }

            return copy;
        }

    }
}
