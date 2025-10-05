using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class SingleSelection
    {
        public static SingleSelection CreateFromDto(SingleSelectionDto dto, string userName)
        {
            SingleSelection single = new SingleSelection
            {
                Value = dto.Value
            }.WithCreateAudit(userName);

            return single;
        }
        public static void Update(FormComponentTemplate template, FormComponentTemplateDto dto, string userName)
        {
            if (template.SingleSelection == null)
                template.SingleSelection = new SingleSelection().WithCreateAudit(userName);

            template.SingleSelection.Value = dto.SingleSelection?.Value;
            template.SingleSelection.WithUpdateAudit(userName);
        }
        public SingleSelection Duplicate(string userName)
        {
            return new SingleSelection
            {
                Value = this.Value
            }.WithCreateAudit(userName);
        }
        public void Delete(string userName)
        {
            this.WithDeleteAudit(userName);
        }
    }
}
