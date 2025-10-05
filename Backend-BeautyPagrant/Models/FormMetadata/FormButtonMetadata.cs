using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class FormButton
    {
        public static FormButton CreateFromDto(FormButtonDto dto, string userName)
        {
            FormButton button = new FormButton
            {
                TextOnButton = dto.TextOnButton,
                IsActive = dto.IsActive,
                Url = dto.Url
            }.WithCreateAudit(userName);

            return button;
        }
        public static void Update(FormComponentTemplate template, FormComponentTemplateDto dto, string userName)
        {
            if (template.FormButton == null)
                template.FormButton = new FormButton().WithCreateAudit(userName);

            template.FormButton.TextOnButton = dto.FormButton?.TextOnButton;
            template.FormButton.IsActive = dto.FormButton?.IsActive ?? false;
            template.FormButton.Url = dto.FormButton?.Url;
            template.FormButton.WithUpdateAudit(userName);
        }
        public FormButton Duplicate(string userName)
        {
            return new FormButton
            {
                TextOnButton = this.TextOnButton,
                IsActive = this.IsActive,
                Url = this.Url
            }.WithCreateAudit(userName);
        }
        public void Delete(string userName)
        {
            this.WithDeleteAudit(userName);
        }
    }
}
