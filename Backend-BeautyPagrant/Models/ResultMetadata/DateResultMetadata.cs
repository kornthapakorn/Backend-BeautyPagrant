using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class DateResult
    {
        public static DateResult Create(DateResultCreateDto dto, string userName)
        {
            DateResult entity = new DateResult
            {
                Value = dto.Value
            };
            return entity.WithCreateAudit(userName);
        }
    }
}
