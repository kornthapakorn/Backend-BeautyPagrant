using Backend_BeautyPagrant.Dto;


namespace Backend_BeautyPagrant.Models
{
    public partial class AboutU 
    {
        public static AboutU CreateFromDto(AboutUDto dto, string userName)
        {
            AboutU aboutU = new AboutU
            {
                ImageTopic = dto.ImageTopic,
                TextTopic = dto.TextTopic,
                TextDesc = dto.TextDesc,
                LeftImage = dto.LeftImage,
                LeftText = dto.LeftText,
                LeftUrl = dto.LeftUrl,
                RightImage = dto.RightImage,
                RightText = dto.RightText,
                RightUrl = dto.RightUrl
            }.WithCreateAudit(userName);

            return aboutU;
        }
        public void UpdateFromDto(AboutUDto dto, string userName)
        {
            this.ImageTopic = dto.ImageTopic;
            this.TextTopic = dto.TextTopic;
            this.TextDesc = dto.TextDesc;
            this.LeftImage = dto.LeftImage;
            this.LeftText = dto.LeftText;
            this.LeftUrl = dto.LeftUrl;
            this.RightImage = dto.RightImage;
            this.RightText = dto.RightText;
            this.RightUrl = dto.RightUrl;
            this.WithUpdateAudit(userName);
        }
        public AboutU Duplicate(string userName)
        {
            return new AboutU
            {
                ImageTopic = this.ImageTopic,
                TextTopic = this.TextTopic,
                TextDesc = this.TextDesc,
                LeftImage = this.LeftImage,
                LeftText = this.LeftText,
                LeftUrl = this.LeftUrl,
                RightImage = this.RightImage,
                RightText = this.RightText,
                RightUrl = this.RightUrl
            }.WithCreateAudit(userName);
        }
    }
}
