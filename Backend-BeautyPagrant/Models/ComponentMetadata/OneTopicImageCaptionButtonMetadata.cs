using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class OneTopicImageCaptionButton 
    {
        public static OneTopicImageCaptionButton CreateFromDto(OneTopicImageCaptionButtonDto dto, string userName)
        {
            OneTopicImageCaptionButton one = new OneTopicImageCaptionButton
            {
                Title = dto.Title,
                Image = dto.Image,
                TextDesc = dto.TextDesc,
                TextOnButton = dto.TextOnButton,
                IsActive = dto.IsActive,
                Url = dto.Url
            }.WithCreateAudit(userName);

            return one;
        }
        public void UpdateFromDto(OneTopicImageCaptionButtonDto dto, string userName)
        {
            this.Title = dto.Title;
            this.Image = dto.Image;
            this.TextDesc = dto.TextDesc;
            this.TextOnButton = dto.TextOnButton;
            this.IsActive = dto.IsActive;
            this.Url = dto.Url;
            this.WithUpdateAudit(userName);
        }
        public OneTopicImageCaptionButton Duplicate(string userName)
        {
            return new OneTopicImageCaptionButton
            {
                Title = this.Title,
                Image = this.Image,
                TextDesc = this.TextDesc,
                TextOnButton = this.TextOnButton,
                IsActive = this.IsActive,
                Url = this.Url
            }.WithCreateAudit(userName);
        }
    }
}
