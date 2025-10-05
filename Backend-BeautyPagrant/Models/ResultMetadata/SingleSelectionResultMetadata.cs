using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class SingleSelectionResult
    {
        public static SingleSelectionResult Create(SingleSelectionResultCreateDto dto, string userName)
        {
            SingleSelectionResult entity = new SingleSelectionResult
            {
                IsActive = dto.IsActive
            };
            return entity.WithCreateAudit(userName);
        }
    }
}
