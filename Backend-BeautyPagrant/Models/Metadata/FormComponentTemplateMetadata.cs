using System.Runtime.CompilerServices;
using Backend_BeautyPagrant.Dto;
namespace Backend_BeautyPagrant.Models
{
    public partial class FormComponentTemplate
    {
        public static void Create(BeautyPagrantContext context, FormTemplate form, List<FormComponentTemplateDto> dtoList, string userName)
        {
            foreach (FormComponentTemplateDto dto in dtoList)
            {
                int? createdId = null;

                switch (dto.ComponentType)
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

                    case "birthDate":
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
                }

                FormComponentTemplate comp = new FormComponentTemplate
                {
                    Form = form,
                    ComponentType = dto.ComponentType,
                    SingleSelectionId = (dto.ComponentType == "singleselection") ? createdId : null,
                    TextField = (dto.ComponentType == "textfield") ? createdId : null,
                    DateId = (dto.ComponentType == "date") ? createdId : null,
                    BirthDateId = (dto.ComponentType == "birthdate") ? createdId : null,
                    ImageUploadId = (dto.ComponentType == "imageupload") ? createdId : null,
                    ImageUploadWithImageContentId = (dto.ComponentType == "imageuploadwithimagecontent") ? createdId : null,
                    FormButtonId = (dto.ComponentType == "formbutton") ? createdId : null
                }.WithCreateAudit(userName);

                context.FormComponentTemplates.Add(comp);
            }
        }
        public void Update(FormComponentTemplateDto dto, string userName)
        {
            ComponentType = dto.ComponentType;

            switch (dto.ComponentType)
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
            }

            this.WithUpdateAudit(userName);
        }
        public static void Update(BeautyPagrantContext context,FormTemplate form,List<FormComponentTemplateDto> dtoList,string userName)
        {
            List<FormComponentTemplate> existingComponents = context.FormComponentTemplates
                .Where(c => c.Form.Id == form.Id && !c.IsDelete)
                .ToList();

            foreach (FormComponentTemplateDto dto in dtoList)
            {
                FormComponentTemplate existing = existingComponents
                    .FirstOrDefault(c => c.Id == dto.Id);

                if (existing != null)
                {
                    existing.Update(dto, userName);
                }
                else
                {
                    FormComponentTemplate.Create(context, form, new List<FormComponentTemplateDto> { dto }, userName);
                }
            }

            HashSet<int> dtoIds = new HashSet<int>(
                dtoList.Select(d => d.Id)
            );

            foreach (FormComponentTemplate old in existingComponents)
            {
                if (!dtoIds.Contains(old.Id))
                {
                    old.Delete(userName);
                }
            }
        }
        public static void Duplicate(BeautyPagrantContext context, FormTemplate newForm, FormComponentTemplate original, string userName)
        {
            int? createdId = null;

            switch (original.ComponentType)
            {
                case "SingleSelection":
                    if (original.SingleSelection != null)
                    {
                        SingleSelection newSingle = original.SingleSelection.Duplicate(userName);
                        context.SingleSelections.Add(newSingle);
                        context.SaveChanges();
                        createdId = newSingle.Id;
                    }
                    break;

                case "TextField":
                    if (original.TextFieldNavigation != null)
                    {
                        TextField newTextField = original.TextFieldNavigation.Duplicate(userName);
                        context.TextFields.Add(newTextField);
                        context.SaveChanges();
                        createdId = newTextField.Id;
                    }
                    break;

                case "Date":
                    if (original.Date != null)
                    {
                        Date newDate = original.Date.Duplicate(userName);
                        context.Dates.Add(newDate);
                        context.SaveChanges();
                        createdId = newDate.Id;
                    }
                    break;

                case "BirthDate":
                    if (original.BirthDate != null)
                    {
                        BirthDate newBirth = original.BirthDate.Duplicate(userName);
                        context.BirthDates.Add(newBirth);
                        context.SaveChanges();
                        createdId = newBirth.Id;
                    }
                    break;

                case "ImageUpload":
                    if (original.ImageUpload != null)
                    {
                        ImageUpload newImg = original.ImageUpload.Duplicate(userName);
                        context.ImageUploads.Add(newImg);
                        context.SaveChanges();
                        createdId = newImg.Id;
                    }
                    break;

                case "ImageUploadWithImageContent":
                    if (original.ImageUploadWithImageContent != null)
                    {
                        ImageUploadWithImageContent newImgWith = original.ImageUploadWithImageContent.Duplicate(userName);
                        context.ImageUploadWithImageContents.Add(newImgWith);
                        context.SaveChanges();
                        createdId = newImgWith.Id;
                    }
                    break;

                case "FormButton":
                    if (original.FormButton != null)
                    {
                        FormButton newButton = original.FormButton.Duplicate(userName);
                        context.FormButtons.Add(newButton);
                        context.SaveChanges();
                        createdId = newButton.Id;
                    }
                    break;

            }

            FormComponentTemplate copy = new FormComponentTemplate
            {
                Form = newForm,
                ComponentType = original.ComponentType,
                SingleSelectionId = original.ComponentType == "SingleSelection" ? createdId : null,
                TextField = original.ComponentType == "TextField" ? createdId : null,
                DateId = original.ComponentType == "Date" ? createdId : null,
                BirthDateId = original.ComponentType == "BirthDate" ? createdId : null,
                ImageUploadId = original.ComponentType == "ImageUpload" ? createdId : null,
                ImageUploadWithImageContentId = original.ComponentType == "ImageUploadWithImageContent" ? createdId : null,
                FormButtonId = original.ComponentType == "FormButton" ? createdId : null
            }.WithCreateAudit(userName);

            context.FormComponentTemplates.Add(copy);
            context.SaveChanges();
        }
        public void Delete(string userName)
        {
            this.WithDeleteAudit(userName);

            if (this.SingleSelection != null)
                this.SingleSelection.Delete(userName);

            if (this.TextFieldNavigation != null)
                this.TextFieldNavigation.Delete(userName);

            if (this.Date != null)
                this.Date.Delete(userName);

            if (this.BirthDate != null)
                this.BirthDate.Delete(userName);

            if (this.ImageUpload != null)
                this.ImageUpload.Delete(userName);

            if (this.ImageUploadWithImageContent != null)
                this.ImageUploadWithImageContent.Delete(userName);

            if (this.FormButton != null)
                this.FormButton.Delete(userName);
        }

    }
}
