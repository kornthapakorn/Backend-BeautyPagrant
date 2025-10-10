using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class FormComponentTemplate
    {
        // ==============================
        // CREATE
        // ==============================
        public static void Create(BeautyPagrantContext context, FormTemplate form, List<FormComponentTemplateDto> dtoList, string userName)
        {
            foreach (FormComponentTemplateDto dto in dtoList)
            {
                string componentType = (dto.ComponentType ?? string.Empty).ToLowerInvariant();
                int? createdId = null;

                switch (componentType)
                {
                    case "singleselection":
                        SingleSelection single = SingleSelection.CreateFromDto(dto.SingleSelection!, userName);
                        context.SingleSelections.Add(single);
                        context.SaveChanges();
                        createdId = single.Id;
                        break;

                    case "textfield":
                        TextField textField = Models.TextField.CreateFromDto(dto.TextField!, userName);
                        context.TextFields.Add(textField);
                        context.SaveChanges();
                        createdId = textField.Id;
                        break;

                    case "date":
                        Date date = Date.CreateFromDto(dto.Date!, userName);
                        context.Dates.Add(date);
                        context.SaveChanges();
                        createdId = date.Id;
                        break;

                    case "birthdate":
                        BirthDate birth = BirthDate.CreateFromDto(dto.BirthDate!, userName);
                        context.BirthDates.Add(birth);
                        context.SaveChanges();
                        createdId = birth.Id;
                        break;

                    case "imageupload":
                        ImageUpload img = ImageUpload.CreateFromDto(dto.ImageUpload!, userName);
                        context.ImageUploads.Add(img);
                        context.SaveChanges();
                        createdId = img.Id;
                        break;

                    case "imageuploadwithimagecontent":
                        ImageUploadWithImageContent img2 = ImageUploadWithImageContent.CreateFromDto(dto.ImageUploadWithImageContent!, userName);
                        context.ImageUploadWithImageContents.Add(img2);
                        context.SaveChanges();
                        createdId = img2.Id;
                        break;

                    case "formbutton":
                        FormButton button = FormButton.CreateFromDto(dto.FormButton!, userName);
                        context.FormButtons.Add(button);
                        context.SaveChanges();
                        createdId = button.Id;
                        break;

                    default:
                        continue;
                }

                FormComponentTemplate comp = new FormComponentTemplate
                {
                    Form = form,
                    ComponentType = componentType, // เก็บเป็นตัวเล็กเสมอ

                    SingleSelectionId = (componentType == "singleselection") ? createdId : null,
                    TextField = (componentType == "textfield") ? createdId : null,
                    DateId = (componentType == "date") ? createdId : null,
                    BirthDateId = (componentType == "birthdate") ? createdId : null,
                    ImageUploadId = (componentType == "imageupload") ? createdId : null,
                    ImageUploadWithImageContentId = (componentType == "imageuploadwithimagecontent") ? createdId : null,
                    FormButtonId = (componentType == "formbutton") ? createdId : null
                }.WithCreateAudit(userName);

                context.FormComponentTemplates.Add(comp);
                context.SaveChanges();
            }
        }

        // ==============================
        // UPDATE
        // ==============================
        public void Update(FormComponentTemplateDto dto, string userName)
        {
            string componentType = (dto.ComponentType ?? string.Empty).ToLowerInvariant();
            ComponentType = componentType;

            switch (componentType)
            {
                case "singleselection":
                    SingleSelection.Update(this, dto, userName);
                    break;

                case "textfield":
                    Models.TextField.Update(this, dto, userName);
                    break;

                case "date":
                    Date.Update(this, dto, userName);
                    break;

                case "birthdate":
                    BirthDate.Update(this, dto, userName);
                    break;

                case "imageupload":
                    ImageUpload.Update(this, dto, userName);
                    break;

                case "imageuploadwithimagecontent":
                    ImageUploadWithImageContent.Update(this, dto, userName);
                    break;

                case "formbutton":
                    FormButton.Update(this, dto, userName);
                    break;

                default:
                    break;
            }

            this.WithUpdateAudit(userName);
        }

        public static void Update(BeautyPagrantContext context, FormTemplate form, List<FormComponentTemplateDto> dtoList, string userName)
        {
            List<FormComponentTemplate> existingComponents = context.FormComponentTemplates
                .Where(c => c.Form.Id == form.Id && !c.IsDelete)
                .ToList();

            foreach (FormComponentTemplateDto dto in dtoList)
            {
                FormComponentTemplate existing = existingComponents.FirstOrDefault(c => c.Id == dto.Id);

                if (existing != null)
                {
                    existing.Update(dto, userName);
                }
                else
                {
                    FormComponentTemplate.Create(context, form, new List<FormComponentTemplateDto> { dto }, userName);
                }
            }

            HashSet<int> dtoIds = new HashSet<int>(dtoList.Select(d => d.Id));

            foreach (FormComponentTemplate old in existingComponents)
            {
                if (!dtoIds.Contains(old.Id))
                {
                    // เดิม: old.Delete(userName);
                    old.Delete(context, userName); // ✅ ให้ cascade ลูกแม้ nav ไม่ได้โหลด
                }
            }


            context.SaveChanges();
        }

        // ==============================
        // DUPLICATE  (โหลดลูกจาก FK ถ้า nav ว่าง)
        // ==============================
        public static void Duplicate(BeautyPagrantContext context, FormTemplate newForm, FormComponentTemplate original, string userName)
        {
            if (original == null || original.IsDelete) return;

            string type = (original.ComponentType ?? string.Empty).ToLowerInvariant();
            int? createdId = null;

            switch (type)
            {
                case "singleselection":
                    {
                        SingleSelection src = original.SingleSelection
                            ?? (original.SingleSelectionId.HasValue
                                ? context.SingleSelections.FirstOrDefault(x => x.Id == original.SingleSelectionId.Value && !x.IsDelete)
                                : null);
                        if (src != null)
                        {
                            SingleSelection newSingle = src.Duplicate(userName);
                            context.SingleSelections.Add(newSingle);
                            context.SaveChanges();
                            createdId = newSingle.Id;
                        }
                        break;
                    }

                case "textfield":
                    {
                        TextField src = original.TextFieldNavigation
                            ?? (original.TextField.HasValue
                                ? context.TextFields.FirstOrDefault(x => x.Id == original.TextField.Value && !x.IsDelete)
                                : null);
                        if (src != null)
                        {
                            TextField newTextField = src.Duplicate(userName);
                            context.TextFields.Add(newTextField);
                            context.SaveChanges();
                            createdId = newTextField.Id;
                        }
                        break;
                    }

                case "date":
                    {
                        Date src = original.Date
                            ?? (original.DateId.HasValue
                                ? context.Dates.FirstOrDefault(x => x.Id == original.DateId.Value && !x.IsDelete)
                                : null);
                        if (src != null)
                        {
                            Date newDate = src.Duplicate(userName);
                            context.Dates.Add(newDate);
                            context.SaveChanges();
                            createdId = newDate.Id;
                        }
                        break;
                    }

                case "birthdate":
                    {
                        BirthDate src = original.BirthDate
                            ?? (original.BirthDateId.HasValue
                                ? context.BirthDates.FirstOrDefault(x => x.Id == original.BirthDateId.Value && !x.IsDelete)
                                : null);
                        if (src != null)
                        {
                            BirthDate newBirth = src.Duplicate(userName);
                            context.BirthDates.Add(newBirth);
                            context.SaveChanges();
                            createdId = newBirth.Id;
                        }
                        break;
                    }

                case "imageupload":
                    {
                        ImageUpload src = original.ImageUpload
                            ?? (original.ImageUploadId.HasValue
                                ? context.ImageUploads.FirstOrDefault(x => x.Id == original.ImageUploadId.Value && !x.IsDelete)
                                : null);
                        if (src != null)
                        {
                            ImageUpload newImg = src.Duplicate(userName);
                            context.ImageUploads.Add(newImg);
                            context.SaveChanges();
                            createdId = newImg.Id;
                        }
                        break;
                    }

                case "imageuploadwithimagecontent":
                    {
                        ImageUploadWithImageContent src = original.ImageUploadWithImageContent
                            ?? (original.ImageUploadWithImageContentId.HasValue
                                ? context.ImageUploadWithImageContents.FirstOrDefault(x => x.Id == original.ImageUploadWithImageContentId.Value && !x.IsDelete)
                                : null);
                        if (src != null)
                        {
                            ImageUploadWithImageContent newImgWith = src.Duplicate(userName);
                            context.ImageUploadWithImageContents.Add(newImgWith);
                            context.SaveChanges();
                            createdId = newImgWith.Id;
                        }
                        break;
                    }

                case "formbutton":
                    {
                        FormButton src = original.FormButton
                            ?? (original.FormButtonId.HasValue
                                ? context.FormButtons.FirstOrDefault(x => x.Id == original.FormButtonId.Value && !x.IsDelete)
                                : null);
                        if (src != null)
                        {
                            FormButton newButton = src.Duplicate(userName);
                            context.FormButtons.Add(newButton);
                            context.SaveChanges();
                            createdId = newButton.Id;
                        }
                        break;
                    }

                default:
                    break;
            }

            FormComponentTemplate copy = new FormComponentTemplate
            {
                Form = newForm,
                ComponentType = type,

                SingleSelectionId = (type == "singleselection") ? createdId : null,
                TextField = (type == "textfield") ? createdId : null,
                DateId = (type == "date") ? createdId : null,
                BirthDateId = (type == "birthdate") ? createdId : null,
                ImageUploadId = (type == "imageupload") ? createdId : null,
                ImageUploadWithImageContentId = (type == "imageuploadwithimagecontent") ? createdId : null,
                FormButtonId = (type == "formbutton") ? createdId : null
            }.WithCreateAudit(userName);

            context.FormComponentTemplates.Add(copy);
            context.SaveChanges();
        }


        // ==============================
        // DELETE (cascade)  *ไม่เปลี่ยน signature*
        // ==============================
        public void Delete(BeautyPagrantContext context, string userName)
        {
            this.WithDeleteAudit(userName);

            // singleselection
            SingleSelection single = this.SingleSelection
                ?? (this.SingleSelectionId.HasValue
                    ? context.SingleSelections.FirstOrDefault(x => x.Id == this.SingleSelectionId.Value && !x.IsDelete)
                    : null);
            if (single != null) single.Delete(userName);

            // textfield
            TextField textField = this.TextFieldNavigation
                ?? (this.TextField.HasValue
                    ? context.TextFields.FirstOrDefault(x => x.Id == this.TextField.Value && !x.IsDelete)
                    : null);
            if (textField != null) textField.Delete(userName);

            // date
            Date date = this.Date
                ?? (this.DateId.HasValue
                    ? context.Dates.FirstOrDefault(x => x.Id == this.DateId.Value && !x.IsDelete)
                    : null);
            if (date != null) date.Delete(userName);

            // birthdate
            BirthDate birth = this.BirthDate
                ?? (this.BirthDateId.HasValue
                    ? context.BirthDates.FirstOrDefault(x => x.Id == this.BirthDateId.Value && !x.IsDelete)
                    : null);
            if (birth != null) birth.Delete(userName);

            // imageupload
            ImageUpload img = this.ImageUpload
                ?? (this.ImageUploadId.HasValue
                    ? context.ImageUploads.FirstOrDefault(x => x.Id == this.ImageUploadId.Value && !x.IsDelete)
                    : null);
            if (img != null) img.Delete(userName);

            // imageuploadwithimagecontent
            ImageUploadWithImageContent imgWith = this.ImageUploadWithImageContent
                ?? (this.ImageUploadWithImageContentId.HasValue
                    ? context.ImageUploadWithImageContents.FirstOrDefault(x => x.Id == this.ImageUploadWithImageContentId.Value && !x.IsDelete)
                    : null);
            if (imgWith != null) imgWith.Delete(userName);

            // formbutton
            FormButton button = this.FormButton
                ?? (this.FormButtonId.HasValue
                    ? context.FormButtons.FirstOrDefault(x => x.Id == this.FormButtonId.Value && !x.IsDelete)
                    : null);
            if (button != null) button.Delete(userName);
        }

    }
}
