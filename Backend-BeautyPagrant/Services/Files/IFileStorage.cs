namespace Backend_BeautyPagrant.Services
{
    public interface IFileStorage
    {
        // บันทึกไฟล์ไปยังโฟลเดอร์ย่อย เช่น "components/banner"
        // คืนค่าเป็น relative path ที่เก็บไว้ใน DB ได้ เช่น "/uploads/components/banner/xxxx.png"
        string Save(IFormFile file, string subfolder);
    }
}
