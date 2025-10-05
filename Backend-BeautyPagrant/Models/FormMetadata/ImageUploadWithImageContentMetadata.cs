using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class ImageUploadWithImageContent
    {
        public static ImageUploadWithImageContent CreateFromDto(ImageUploadWithImageContentDto dto, string userName)
        {
            ImageUploadWithImageContent img = new ImageUploadWithImageContent
            {
                TextDesc = dto.TextDesc,
                Image = dto.Image
            }.WithCreateAudit(userName);

            return img;
        }
        public static void Update(FormComponentTemplate template, FormComponentTemplateDto dto, string userName)
        {
            if (template.ImageUploadWithImageContent == null)
                template.ImageUploadWithImageContent = new ImageUploadWithImageContent().WithCreateAudit(userName);

            template.ImageUploadWithImageContent.TextDesc = dto.ImageUploadWithImageContent?.TextDesc;
            template.ImageUploadWithImageContent.Image = dto.ImageUploadWithImageContent?.Image;
            template.ImageUploadWithImageContent.WithUpdateAudit(userName);
        }
        public ImageUploadWithImageContent Duplicate(string userName)
        {
            return new ImageUploadWithImageContent
            {
                TextDesc = this.TextDesc,
                Image = this.Image
            }.WithCreateAudit(userName);
        }
        public void Delete(string userName)
        {
            this.WithDeleteAudit(userName);
        }
    }
}
