using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class TextField
    {
        public static TextField CreateFromDto(TextFieldDto dto, string userName)
        {
            TextField textField = new TextField
            {
                Text = dto.Text
            }.WithCreateAudit(userName);

            return textField;
        }
        public static void Update(FormComponentTemplate template, FormComponentTemplateDto dto, string userName)
        {
            if (template.TextFieldNavigation == null)
                template.TextFieldNavigation = new TextField().WithCreateAudit(userName);

            template.TextFieldNavigation.Text = dto.TextField?.Text;
            template.TextFieldNavigation.WithUpdateAudit(userName);
        }
        public TextField Duplicate(string userName)
        {
            return new TextField
            {
                Text = this.Text
            }.WithCreateAudit(userName);
        }
        public void Delete(string userName)
        {
            this.WithDeleteAudit(userName);
        }
    }
}
