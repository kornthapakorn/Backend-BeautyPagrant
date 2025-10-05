using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class GridFourImage 
    {
        public static GridFourImage CreateFromDto(GridFourImageDto dto, string userName)
        {
            GridFourImage grid = new GridFourImage
            {
                Image1 = dto.Image1,
                Image2 = dto.Image2,
                Image3 = dto.Image3,
                Image4 = dto.Image4
            }.WithCreateAudit(userName);

            return grid;
        }
        public void UpdateFromDto(GridFourImageDto dto, string userName)
        {
            this.Image1 = dto.Image1;
            this.Image2 = dto.Image2;
            this.Image3 = dto.Image3;
            this.Image4 = dto.Image4;
            this.WithUpdateAudit(userName);
        }
        public GridFourImage Duplicate(string userName)
        {
            return new GridFourImage
            {
                Image1 = this.Image1,
                Image2 = this.Image2,
                Image3 = this.Image3,
                Image4 = this.Image4
            }.WithCreateAudit(userName);
        }
    }
}
