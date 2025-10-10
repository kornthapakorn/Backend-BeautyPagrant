using Backend_BeautyPagrant.Models;

namespace Backend_BeautyPagrant.Services
{
    public interface IFormResultAppService
    {
        // อัปโหลดไฟล์ที่ผู้ใช้ส่งตอน submit ผลลัพธ์ของฟอร์ม
        // key mapping ตามที่ Front ส่ง
        void SaveUploadFiles(BeautyPagrantContext context, int formTemplateId,
                             IDictionary<string, IFormFile> files, string userName);
    }
}
