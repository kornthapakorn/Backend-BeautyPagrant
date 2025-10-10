using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Services
{
    public class FormComponentImageBinder : IFormComponentImageBinder
    {
        private readonly IFileStorage _storage;

        public FormComponentImageBinder(IFileStorage storage)
        {
            _storage = storage;
        }

        public void BindMany(IList<FormComponentTemplateDto> dtos, IDictionary<string, IFormFile> files)
        {
            if (dtos == null || files == null) return;
            foreach (FormComponentTemplateDto dto in dtos) Bind(dto, files);
        }

        public void Bind(FormComponentTemplateDto dto, IDictionary<string, IFormFile> files)
        {
            if (dto == null || files == null) return;

            // form ส่วนที่มีรูปจริง ๆ คือ imageupload, imageuploadwithimagecontent
            if (dto.ImageUpload != null && files.TryGetValue($"form.imageupload.{dto.Id}", out IFormFile up))
                dto.ImageUpload.Text = _storage.Save(up, "forms/image-upload"); // เก็บ path ใน Text หรือเพิ่ม field ใหม่ตามโมเดล

            if (dto.ImageUploadWithImageContent != null &&
                files.TryGetValue($"form.imageuploadwithimagecontent.{dto.Id}", out IFormFile up2))
                dto.ImageUploadWithImageContent.Image = _storage.Save(up2, "forms/image-upload-with-content");
        }
    }
}
