using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class Sale 
    {
        public static Sale CreateFromDto(SaleDto dto, string userName)
        {
            Sale sale = new Sale
            {
                Title = dto.Title,
                Text = dto.Text,
                PromoPrice = dto.PromoPrice,
                Price = dto.Price,
                EndDate = dto.EndDate,
                TextDesc = dto.TextDesc,
                TextOnButton = dto.TextOnButton,
                TextFooter = dto.TextFooter,
                LeftImage = dto.LeftImage,
                LeftText = dto.LeftText,
                RightImage = dto.RightImage,
                RightText = dto.RightText,
                IsActive = dto.IsActive,
                Url = dto.Url
            }.WithCreateAudit(userName);

            return sale;
        }
        public void UpdateFromDto(SaleDto dto, string userName)
        {
            this.Title = dto.Title;
            this.Text = dto.Text;
            this.PromoPrice = dto.PromoPrice;
            this.Price = dto.Price;
            this.EndDate = dto.EndDate;
            this.TextDesc = dto.TextDesc;
            this.TextOnButton = dto.TextOnButton;
            this.TextFooter = dto.TextFooter;
            this.LeftImage = dto.LeftImage;
            this.LeftText = dto.LeftText;
            this.RightImage = dto.RightImage;
            this.RightText = dto.RightText;
            this.IsActive = dto.IsActive;
            this.Url = dto.Url;
            this.WithUpdateAudit(userName);
        }
        public Sale Duplicate(string userName)
        {
            return new Sale
            {
                Title = this.Title,
                Text = this.Text,
                PromoPrice = this.PromoPrice,
                Price = this.Price,
                EndDate = this.EndDate,
                TextDesc = this.TextDesc,
                TextOnButton = this.TextOnButton,
                TextFooter = this.TextFooter,
                LeftImage = this.LeftImage,
                LeftText = this.LeftText,
                RightImage = this.RightImage,
                RightText = this.RightText,
                IsActive = this.IsActive,
                Url = this.Url
            }.WithCreateAudit(userName);
        }
    }
}
