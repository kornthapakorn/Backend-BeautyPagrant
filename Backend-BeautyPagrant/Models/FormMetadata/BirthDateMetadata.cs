using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class BirthDate 
    {
        public static BirthDate CreateFromDto(BirthDateDto dto, string userName)
        {
            BirthDate birth = new BirthDate
            {
                Label = dto.Label
            }.WithCreateAudit(userName);

            return birth;
        }
        public static void Update(FormComponentTemplate template, FormComponentTemplateDto dto, string userName)
        {
            if (template.BirthDate == null)
                template.BirthDate = new BirthDate().WithCreateAudit(userName);

            template.BirthDate.Label = dto.BirthDate?.Label;
            template.BirthDate.WithUpdateAudit(userName);
        }
        public BirthDate Duplicate(string userName)
        {
            return new BirthDate
            {
                Label = this.Label
            }.WithCreateAudit(userName);
        }
        public void Delete(string userName)
        {
            this.WithDeleteAudit(userName);
        }
    }
}
