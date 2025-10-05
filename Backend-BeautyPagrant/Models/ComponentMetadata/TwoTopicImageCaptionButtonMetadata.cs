using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class TwoTopicImageCaptionButton 
    {
        public static TwoTopicImageCaptionButton CreateFromDto(TwoTopicImageCaptionButtonDto dto, string userName)
        {
            TwoTopicImageCaptionButton two = new TwoTopicImageCaptionButton
            {
                LeftTitle = dto.LeftTitle,
                LeftImage = dto.LeftImage,
                LeftTextDesc = dto.LeftTextDesc,
                LeftTextOnButton = dto.LeftTextOnButton,
                LeftIsActive = dto.LeftIsActive,
                LeftUrl = dto.LeftUrl,
                RightTitle = dto.RightTitle,
                RightImage = dto.RightImage,
                RightTextDesc = dto.RightTextDesc,
                RightTextOnButton = dto.RightTextOnButton,
                RightIsActive = dto.RightIsActive,
                RightUrl = dto.RightUrl
            }.WithCreateAudit(userName);

            return two;
        }
        public void UpdateFromDto(TwoTopicImageCaptionButtonDto dto, string userName)
        {
            this.LeftTitle = dto.LeftTitle;
            this.LeftImage = dto.LeftImage;
            this.LeftTextDesc = dto.LeftTextDesc;
            this.LeftTextOnButton = dto.LeftTextOnButton;
            this.LeftIsActive = dto.LeftIsActive;
            this.LeftUrl = dto.LeftUrl;
            this.RightTitle = dto.RightTitle;
            this.RightImage = dto.RightImage;
            this.RightTextDesc = dto.RightTextDesc;
            this.RightTextOnButton = dto.RightTextOnButton;
            this.RightIsActive = dto.RightIsActive;
            this.RightUrl = dto.RightUrl;
            this.WithUpdateAudit(userName);
        }
        public TwoTopicImageCaptionButton Duplicate(string userName)
        {
            return new TwoTopicImageCaptionButton
            {
                LeftTitle = this.LeftTitle,
                LeftImage = this.LeftImage,
                LeftTextDesc = this.LeftTextDesc,
                LeftTextOnButton = this.LeftTextOnButton,
                LeftIsActive = this.LeftIsActive,
                LeftUrl = this.LeftUrl,
                RightTitle = this.RightTitle,
                RightImage = this.RightImage,
                RightTextDesc = this.RightTextDesc,
                RightTextOnButton = this.RightTextOnButton,
                RightIsActive = this.RightIsActive,
                RightUrl = this.RightUrl
            }.WithCreateAudit(userName);
        }
    }
}
