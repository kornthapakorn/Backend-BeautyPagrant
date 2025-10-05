using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class ImageUploadResult
    {
        public static ImageUploadResult Create(ImageUploadResultCreateDto dto, string userName)
        {
            ImageUploadResult entity = new ImageUploadResult
            {
                FilePath = dto.FilePath
            };
            return entity.WithCreateAudit(userName);
        }
    }
}
