using Backend_BeautyPagrant.Models;

namespace Backend_BeautyPagrant.Services
{
    public class FormResultAppService : IFormResultAppService
    {
        private readonly IFileStorage _storage;

        public FormResultAppService(IFileStorage storage)
        {
            _storage = storage;
        }

        public void SaveUploadFiles(BeautyPagrantContext context, int formTemplateId,
                                    IDictionary<string, IFormFile> files, string userName)
        {
            if (files == null || files.Count == 0) return;

            // ตัวอย่าง: คีย์ "result.{componentId}" -> เก็บไฟล์แล้วเขียนลงตารางผลลัพธ์ตามโมเดลของคุณ
            foreach (KeyValuePair<string, IFormFile> kv in files)
            {
                string key = kv.Key.ToLowerInvariant();
                IFormFile file = kv.Value;
                // บันทึกไฟล์
                string path = _storage.Save(file, $"forms/results/{formTemplateId}");
                // TODO: เซฟ path เข้า entity ผลลัพธ์ตาม schema ของคุณ
                // เช่น context.ImageUploadResults.Add(new ImageUploadResult { FilePath = path, ... }.WithCreateAudit(userName));
            }

            context.SaveChanges();
        }
    }
}
