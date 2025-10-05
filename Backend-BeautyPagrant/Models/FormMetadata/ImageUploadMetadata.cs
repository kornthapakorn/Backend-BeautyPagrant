using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class ImageUpload
    {
        public static ImageUpload CreateFromDto(ImageUploadDto dto, string userName)
        {
            ImageUpload img = new ImageUpload
            {
                Text = dto.Text
            }.WithCreateAudit(userName);

            return img;
        }
        public static void Update(FormComponentTemplate template, FormComponentTemplateDto dto, string userName)
        {
            if (template.ImageUpload == null)
                template.ImageUpload = new ImageUpload().WithCreateAudit(userName);

            template.ImageUpload.Text = dto.ImageUpload?.Text;
            template.ImageUpload.WithUpdateAudit(userName);
        }
        public ImageUpload Duplicate(string userName)
        {
            return new ImageUpload
            {
                Text = this.Text
            }.WithCreateAudit(userName);
        }
        public void Delete(string userName)
        {
            this.WithDeleteAudit(userName);
        }
    }
}
