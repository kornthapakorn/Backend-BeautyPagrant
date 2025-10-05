using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class GridTwoColumn 
    {
        public static GridTwoColumn CreateFromDto(GridTwoColumnDto dto, string userName)
        {
            GridTwoColumn grid = new GridTwoColumn
            {
                LeftImage = dto.LeftImage,
                LeftText = dto.LeftText,
                LeftUrl = dto.LeftUrl,
                RightImage = dto.RightImage,
                RightText = dto.RightText,
                RightUrl = dto.RightUrl
            }.WithCreateAudit(userName);

            return grid;
        }
        public void UpdateFromDto(GridTwoColumnDto dto, string userName)
        {
            this.LeftImage = dto.LeftImage;
            this.LeftText = dto.LeftText;
            this.LeftUrl = dto.LeftUrl;
            this.RightImage = dto.RightImage;
            this.RightText = dto.RightText;
            this.RightUrl = dto.RightUrl;
            this.WithUpdateAudit(userName);
        }
        public GridTwoColumn Duplicate(string userName)
        {
            return new GridTwoColumn
            {
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
