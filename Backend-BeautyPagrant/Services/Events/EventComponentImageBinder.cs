using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Services
{
    public class EventComponentImageBinder : IEventComponentImageBinder
    {
        private readonly IFileStorage _storage;

        public EventComponentImageBinder(IFileStorage storage)
        {
            _storage = storage;
        }

        public void Bind(EventCreateDto dto, IDictionary<string, IFormFile> files)
        {
            if (dto == null) return;

            // 1) รูป cover ของ Event
            IFormFile cover = Get(files, "event.fileImage");
            if (cover != null)
            {
                string saved = _storage.Save(cover, "events");
                dto.FileImage = saved;
            }

            // 2) คอมโพเนนต์ของ Event (recursive section)
            if (dto.Components != null && dto.Components.Count > 0)
            {
                BindMany(dto.Components, files);
            }
        }

        public void BindMany(IList<EventComponentDto> components, IDictionary<string, IFormFile> files)
        {
            if (components == null) return;

            for (int i = 0; i < components.Count; i++)
            {
                EventComponentDto c = components[i];
                if (c == null || string.IsNullOrEmpty(c.ComponentType)) continue;

                string type = c.ComponentType.ToLowerInvariant();

                // ------------ mapping ตามชนิด ------------

                if (type == "banner" && c.Banner != null)
                {
                    IFormFile f = Get(files, "components[" + i + "].banner.image");
                    if (f != null) c.Banner.Image = _storage.Save(f, "components/banner");
                }
                else if (type == "imagewithcaption" && c.ImageWithCaption != null)
                {
                    IFormFile f = Get(files, "components[" + i + "].imagewithcaption.image");
                    if (f != null) c.ImageWithCaption.Image = _storage.Save(f, "components/imagewithcaption");
                }
                else if (type == "imagedesc" && c.ImageDesc != null)
                {
                    IFormFile f = Get(files, "components[" + i + "].imagedesc.image");
                    if (f != null) c.ImageDesc.Image = _storage.Save(f, "components/imagedesc");
                }
                else if (type == "gridfourimage" && c.GridFourImage != null)
                {
                    IFormFile f1 = Get(files, "components[" + i + "].gridfourimage.image1");
                    IFormFile f2 = Get(files, "components[" + i + "].gridfourimage.image2");
                    IFormFile f3 = Get(files, "components[" + i + "].gridfourimage.image3");
                    IFormFile f4 = Get(files, "components[" + i + "].gridfourimage.image4");
                    if (f1 != null) c.GridFourImage.Image1 = _storage.Save(f1, "components/grid4");
                    if (f2 != null) c.GridFourImage.Image2 = _storage.Save(f2, "components/grid4");
                    if (f3 != null) c.GridFourImage.Image3 = _storage.Save(f3, "components/grid4");
                    if (f4 != null) c.GridFourImage.Image4 = _storage.Save(f4, "components/grid4");
                }
                else if (type == "gridtwocolumn" && c.GridTwoColumn != null)
                {
                    IFormFile fl = Get(files, "components[" + i + "].gridtwocolumn.leftimage");
                    IFormFile fr = Get(files, "components[" + i + "].gridtwocolumn.rightimage");
                    if (fl != null) c.GridTwoColumn.LeftImage = _storage.Save(fl, "components/grid2");
                    if (fr != null) c.GridTwoColumn.RightImage = _storage.Save(fr, "components/grid2");
                }
                else if (type == "aboutu" && c.AboutU != null)
                {
                    IFormFile fl = Get(files, "components[" + i + "].aboutu.leftimage");
                    IFormFile fr = Get(files, "components[" + i + "].aboutu.rightimage");
                    if (fl != null) c.AboutU.LeftImage = _storage.Save(fl, "components/aboutu");
                    if (fr != null) c.AboutU.RightImage = _storage.Save(fr, "components/aboutu");
                }
                else if (type == "sale" && c.Sale != null)
                {
                    IFormFile fl = Get(files, "components[" + i + "].sale.leftimage");
                    IFormFile fr = Get(files, "components[" + i + "].sale.rightimage");
                    if (fl != null) c.Sale.LeftImage = _storage.Save(fl, "components/sale");
                    if (fr != null) c.Sale.RightImage = _storage.Save(fr, "components/sale");
                }
                else if (type == "onetopicimagecaptionbutton" && c.OneTopicImageCaptionButton != null)
                {
                    IFormFile f = Get(files, "components[" + i + "].onetopicimagecaptionbutton.image");
                    if (f != null) c.OneTopicImageCaptionButton.Image = _storage.Save(f, "components/one-topic");
                }
                else if (type == "twotopicimagecaptionbutton" && c.TwoTopicImageCaptionButton != null)
                {
                    IFormFile fl = Get(files, "components[" + i + "].twotopicimagecaptionbutton.leftimage");
                    IFormFile fr = Get(files, "components[" + i + "].twotopicimagecaptionbutton.rightimage");
                    if (fl != null) c.TwoTopicImageCaptionButton.LeftImage = _storage.Save(fl, "components/two-topic");
                    if (fr != null) c.TwoTopicImageCaptionButton.RightImage = _storage.Save(fr, "components/two-topic");
                }
                else if (type == "formtemplate" && c.FormTemplate != null)
                {
                    // รูป popup ของ form
                    IFormFile f = Get(files, "components[" + i + "].formtemplate.popupimage");
                    if (f != null) c.FormTemplate.PopupImage = _storage.Save(f, "formtemplates/popup");

                    // ลูกของ FormTemplate (เฉพาะชนิดที่มีรูป)
                    if (c.FormTemplate.Components != null && c.FormTemplate.Components.Count > 0)
                    {
                        for (int childIndex = 0; childIndex < c.FormTemplate.Components.Count; childIndex++)
                        {
                            FormComponentTemplateDto fc = c.FormTemplate.Components[childIndex];
                            if (fc == null || string.IsNullOrEmpty(fc.ComponentType)) continue;

                            string ct = fc.ComponentType.ToLowerInvariant();

                            if (ct == "imageupload" && fc.ImageUpload != null)
                            {
                                IFormFile ff = Get(files, "components[" + i + "].formtemplate.components[" + childIndex + "].imageupload.file");
                                if (ff != null) fc.ImageUpload.Text = _storage.Save(ff, "forms/imageupload");
                            }
                            else if (ct == "imageuploadwithimagecontent" && fc.ImageUploadWithImageContent != null)
                            {
                                IFormFile ff = Get(files, "components[" + i + "].formtemplate.components[" + childIndex + "].imageuploadwithimagecontent.image");
                                if (ff != null) fc.ImageUploadWithImageContent.Image = _storage.Save(ff, "forms/imageuploadwithcontent");
                            }
                        }
                    }
                }
                else if (type == "section" && c.Section != null && c.Section.Components != null && c.Section.Components.Count > 0)
                {
                    // recursive ลงไปใน section
                    BindMany(c.Section.Components, files);
                }
            }
        }

        // ===== helper =====
        private static IFormFile Get(IDictionary<string, IFormFile> files, string key)
        {
            if (files == null) return null;
            IFormFile f;
            return files.TryGetValue(key, out f) ? f : null;
        }
    }
}
