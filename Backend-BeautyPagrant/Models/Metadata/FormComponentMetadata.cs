using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class FormComponent
    {
        public static FormTemplate CreateFromDto(BeautyPagrantContext context,FormTemplateDto dto,string userName,string baseUrl)
        {
            FormTemplate form = new FormTemplate
            {
                Topic = dto.Topic,
                PublicUrl = baseUrl.TrimEnd('/') + "/form/" + Guid.NewGuid().ToString("N")
            }.WithCreateAudit(userName);

            context.FormTemplates.Add(form);
            context.SaveChanges();

            return form;
        }
        public static void CreateResult(BeautyPagrantContext context, Form form, List<FormComponentSubmitCreateDto> dtoList, string userName)
        {
            foreach (FormComponentSubmitCreateDto dto in dtoList)
            {
                int? createdId = null;

                switch (dto.ComponentType)
                {
                    case "TextField":
                        TextFieldResult textResult = TextFieldResult.Create(dto.ToTextFieldResultCreateDto(), userName);
                        context.TextFieldResults.Add(textResult);
                        context.SaveChanges();
                        createdId = textResult.Id;
                        break;

                    case "Date":
                        DateResult dateResult = DateResult.Create(dto.ToDateResultCreateDto(), userName);
                        context.DateResults.Add(dateResult);
                        context.SaveChanges();
                        createdId = dateResult.Id;
                        break;

                    case "BirthDate":
                        BirthDateResult birthResult = BirthDateResult.Create(dto.ToBirthDateResultCreateDto(), userName);
                        context.BirthDateResults.Add(birthResult);
                        context.SaveChanges();
                        createdId = birthResult.Id;
                        break;

                    case "SingleSelection":
                        SingleSelectionResult selectResult = SingleSelectionResult.Create(dto.ToSingleSelectionResultCreateDto(), userName);
                        context.SingleSelectionResults.Add(selectResult);
                        context.SaveChanges();
                        createdId = selectResult.Id;
                        break;

                    case "ImageUpload":
                        ImageUploadResult uploadResult = ImageUploadResult.Create(dto.ToImageUploadResultCreateDto(), userName);
                        context.ImageUploadResults.Add(uploadResult);
                        context.SaveChanges();
                        createdId = uploadResult.Id;
                        break;

                    case "ImageUploadWithImageContent":
                        ImageUploadWithImageContentResult uploadWithContent = ImageUploadWithImageContentResult.Create(dto.ToImageUploadWithImageContentResultCreateDto(), userName);
                        context.ImageUploadWithImageContentResults.Add(uploadWithContent);
                        context.SaveChanges();
                        createdId = uploadWithContent.Id;
                        break;
                }

                FormComponent comp = new FormComponent
                {
                    Form = form,
                    ComponentType = dto.ComponentType,
                    TextFieldResultId = dto.ComponentType == "TextField" ? createdId : null,
                    DateResultId = dto.ComponentType == "Date" ? createdId : null,
                    BirthDateResultId = dto.ComponentType == "BirthDate" ? createdId : null,
                    SingleSelectionResultId = dto.ComponentType == "SingleSelection" ? createdId : null,
                    ImageUploadResultId = dto.ComponentType == "ImageUpload" ? createdId : null,
                    ImageUploadWithImageContentResultId = dto.ComponentType == "ImageUploadWithImageContent" ? createdId : null
                }.WithCreateAudit(userName);

                context.FormComponents.Add(comp);
            }

            context.SaveChanges();
        }

    }
}
