using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class TableWithTopicAndDesc  
    {
        public static TableWithTopicAndDesc CreateFromDto(TableWithTopicAndDescDto dto, string userName)
        {
            TableWithTopicAndDesc table = new TableWithTopicAndDesc
            {
                Title = dto.Title,
                TextDesc = dto.TextDesc
            }.WithCreateAudit(userName);

            return table;
        }
        public void UpdateFromDto(TableWithTopicAndDescDto dto, string userName)
        {
            this.Title = dto.Title;
            this.TextDesc = dto.TextDesc;
            this.WithUpdateAudit(userName);
        }
        public TableWithTopicAndDesc Duplicate(string userName)
        {
            return new TableWithTopicAndDesc
            {
                Title = this.Title,
                TextDesc = this.TextDesc
            }.WithCreateAudit(userName);
        }
    }
}
