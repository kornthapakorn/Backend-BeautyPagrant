using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class ImageWithCaption
    {
        public static ImageWithCaption CreateFromDto(ImageWithCaptionDto dto, string userName)
        {
            ImageWithCaption img = new ImageWithCaption
            {
                Image = dto.Image,
                Text = dto.Text
            }.WithCreateAudit(userName);

            return img;
        }
        public void UpdateFromDto(ImageWithCaptionDto dto, string userName)
        {
            this.Image = dto.Image;
            this.Text = dto.Text;
            this.WithUpdateAudit(userName);
        }
        public ImageWithCaption Duplicate(string userName)
        {
            return new ImageWithCaption
            {
                Image = this.Image,
                Text = this.Text
            }.WithCreateAudit(userName);
        }
    }
}
