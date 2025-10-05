using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class Button 
    {
        public static Button CreateFromDto(ButtonDto dto, string userName)
        {
            Button button = new Button
            {
                TextOnButton = dto.TextOnButton,
                IsActive = dto.IsActive,
                Url = dto.Url
            }.WithCreateAudit(userName);

            return button;
        }
        public void UpdateFromDto(ButtonDto dto, string userName)
        {
            this.TextOnButton = dto.TextOnButton;
            this.IsActive = dto.IsActive;
            this.Url = dto.Url;
            this.WithUpdateAudit(userName);
        }
        public Button Duplicate(string userName)
        {
            return new Button
            {
                TextOnButton = this.TextOnButton,
                IsActive = this.IsActive,
                Url = this.Url
            }.WithCreateAudit(userName);
        }
    }
}
