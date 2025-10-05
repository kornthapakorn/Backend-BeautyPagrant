using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class ImageDesc 
    {
        public static ImageDesc CreateFromDto(ImageDescDto dto, string userName)
        {
            ImageDesc img = new ImageDesc
            {
                Image = dto.Image,
                Text = dto.Text
            }.WithCreateAudit(userName);

            return img;
        }
        public void UpdateFromDto(ImageDescDto dto, string userName)
        {
            this.Image = dto.Image;
            this.Text = dto.Text;
            this.WithUpdateAudit(userName);
        }
        public ImageDesc Duplicate(string userName)
        {
            return new ImageDesc
            {
                Image = this.Image,
                Text = this.Text
            }.WithCreateAudit(userName);
        }
    }
}
