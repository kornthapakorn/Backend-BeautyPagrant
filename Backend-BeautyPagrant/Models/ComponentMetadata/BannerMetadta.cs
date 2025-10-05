using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class Banner 
    {
        public static Banner CreateFromDto(BannerDto dto, string userName)
        {
            Banner banner = new Banner
            {
                Image = dto.Image,
                TextDesc = dto.TextDesc,
                TextOnButton = dto.TextOnButton,
                IsActive = dto.IsActive,
                UrlButton = dto.UrlButton
            }.WithCreateAudit(userName);

            return banner;
        }
        public void UpdateFromDto(BannerDto dto, string userName)
        {
            this.Image = dto.Image;
            this.TextDesc = dto.TextDesc;
            this.TextOnButton = dto.TextOnButton;
            this.IsActive = dto.IsActive;
            this.UrlButton = dto.UrlButton;
            this.WithUpdateAudit(userName);
        }
        public Banner Duplicate(string userName)
        {
            return new Banner
            {
                Image = this.Image,
                TextDesc = this.TextDesc,
                TextOnButton = this.TextOnButton,
                IsActive = this.IsActive,
                UrlButton = this.UrlButton
            }.WithCreateAudit(userName);
        }
    }
}
