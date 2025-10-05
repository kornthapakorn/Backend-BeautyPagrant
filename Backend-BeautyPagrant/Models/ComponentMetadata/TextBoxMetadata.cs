using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class TextBox 
    {
        public static TextBox CreateFromDto(TextBoxDto dto, string userName)
        {
            TextBox box = new TextBox
            {
                Text = dto.Text
            }.WithCreateAudit(userName);

            return box;
        }
        public void UpdateFromDto(TextBoxDto dto, string userName)
        {
            this.Text = dto.Text;
            this.WithUpdateAudit(userName);
        }
        public TextBox Duplicate(string userName)
        {
            return new TextBox
            {
                Text = this.Text
            }.WithCreateAudit(userName);
        }
    }
}
