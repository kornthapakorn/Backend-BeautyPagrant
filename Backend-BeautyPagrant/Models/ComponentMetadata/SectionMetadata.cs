namespace Backend_BeautyPagrant.Models
{
    public partial class Section 
    {
        public static Section CreateFromDto(string userName)
        {
            Section section = new Section
            {
            }.WithCreateAudit(userName);

            return section;
        }
        public void UpdateFromDto(string userName)
        {
            this.WithUpdateAudit(userName);
        }
        public Section Duplicate(string userName)
        {
            return new Section
            {
            }.WithCreateAudit(userName);
        }
    }
}
