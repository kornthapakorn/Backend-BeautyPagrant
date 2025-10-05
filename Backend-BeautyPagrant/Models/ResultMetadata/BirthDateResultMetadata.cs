using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class BirthDateResult
    {
        public static BirthDateResult Create(BirthDateResultCreateDto dto, string userName)
        {
            BirthDateResult entity = new BirthDateResult
            {
                Value = dto.Value
            };
            return entity.WithCreateAudit(userName);
        }
    }
}
