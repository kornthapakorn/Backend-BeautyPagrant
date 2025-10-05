using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class Date 
    {
        public static Date CreateFromDto(DateDto dto, string userName)
        {
            Date date = new Date
            {
                Text = dto.Text
            }.WithCreateAudit(userName);

            return date;
        }
        public static void Update(FormComponentTemplate template, FormComponentTemplateDto dto, string userName)
        {
            if (template.Date == null)
                template.Date = new Date().WithCreateAudit(userName);

            template.Date.Text = dto.Date?.Text;
            template.Date.WithUpdateAudit(userName);
        }
        public Date Duplicate(string userName)
        {
            return new Date
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
