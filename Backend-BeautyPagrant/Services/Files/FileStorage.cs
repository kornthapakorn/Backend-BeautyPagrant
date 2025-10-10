namespace Backend_BeautyPagrant.Services
{
    public class FileStorage : IFileStorage
    {
        private readonly string _baseUploadPath;

        public FileStorage(IWebHostEnvironment env)
        {
            // เก็บไฟล์ใต้ {ContentRoot}/uploads
            _baseUploadPath = Path.Combine(env.ContentRootPath, "uploads");
            if (!Directory.Exists(_baseUploadPath))
            {
                Directory.CreateDirectory(_baseUploadPath);
            }
        }

        public string Save(IFormFile file, string subfolder)
        {
            if (file == null || file.Length == 0) return string.Empty;

            // โฟลเดอร์ย่อย
            string targetDir = string.IsNullOrWhiteSpace(subfolder)
                ? _baseUploadPath
                : Path.Combine(_baseUploadPath, subfolder.Replace('\\', '/'));

            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            string fileName = System.Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
            string physicalPath = Path.Combine(targetDir, fileName);

            using (FileStream stream = new FileStream(physicalPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // relative path สำหรับเก็บใน DB/ส่งให้ front
            string rel = string.IsNullOrWhiteSpace(subfolder)
                ? $"/uploads/{fileName}"
                : $"/uploads/{subfolder.Replace('\\', '/')}/{fileName}";

            return rel;
        }
    }
}
