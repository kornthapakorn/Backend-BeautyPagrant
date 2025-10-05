using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class TextFieldResult
    {
        public static TextFieldResult Create(TextFieldResultCreateDto dto, string userName)
        {
            TextFieldResult entity = new TextFieldResult
            {
                Value = dto.Value
            };
            return entity.WithCreateAudit(userName);
        }
    }
}
