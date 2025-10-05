using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class ImageUploadWithImageContentResult
    {
        public static ImageUploadWithImageContentResult Create(ImageUploadWithImageContentResultCreateDto dto, string userName)
        {
            ImageUploadWithImageContentResult entity = new ImageUploadWithImageContentResult
            {
                FilePath = dto.FilePath
            };
            return entity.WithCreateAudit(userName);
        }
    }
}
