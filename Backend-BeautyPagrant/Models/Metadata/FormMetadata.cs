using Backend_BeautyPagrant.Dto;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models
{
    public partial class Form
    {
        public static Form Create(BeautyPagrantContext context, int formTemplateId, string userName)
        {
            FormTemplate? template = context.FormTemplates
                .Include(t => t.FormComponentTemplates)
                .FirstOrDefault(t => t.Id == formTemplateId);

            if (template == null)
                throw new ArgumentException("ไม่พบ FormTemplate");

            Form form = new Form
            {
                FormTemplateId = template.Id
            }.WithCreateAudit(userName);

            context.Forms.Add(form);
            context.SaveChanges();

            foreach (FormComponentTemplate compTemplate in template.FormComponentTemplates)
            {
                FormComponent component = new FormComponent
                {
                    FormId = form.Id,
                    FormComponentTemplateId = compTemplate.Id,
                    ComponentType = compTemplate.ComponentType
                }.WithCreateAudit(userName);

                context.FormComponents.Add(component);
            }

            context.SaveChanges();
            return form;
        }

        public void Submit(BeautyPagrantContext context, FormSubmitDto dto, string userName)
        {
            foreach (FormComponentSubmitDto input in dto.Components)
            {
                FormComponent? component = context.FormComponents
                    .FirstOrDefault(c => c.Id == input.FormComponentId && c.FormId == this.Id);

                if (component == null) continue;

                switch (component.ComponentType)
                {
                    case "TextField":
                        TextFieldResult tf = new TextFieldResult
                        {
                            Value = input.Value ?? string.Empty
                        }.WithCreateAudit(userName);
                        context.TextFieldResults.Add(tf);
                        context.SaveChanges();
                        component.TextFieldResultId = tf.Id;
                        break;

                    case "SingleSelection":
                        SingleSelectionResult ss = new SingleSelectionResult
                        {
                            IsActive = input.IsActive ?? false
                        }.WithCreateAudit(userName);
                        context.SingleSelectionResults.Add(ss);
                        context.SaveChanges();
                        component.SingleSelectionResultId = ss.Id;
                        break;

                    case "Date":
                        DateTime parsedDate;
                        DateResult dr = new DateResult
                        {
                            Value = DateTime.TryParse(input.Value, out parsedDate) ? parsedDate : DateTime.Now
                        }.WithCreateAudit(userName);
                        context.DateResults.Add(dr);
                        context.SaveChanges();
                        component.DateResultId = dr.Id;
                        break;

                    case "BirthDate":
                        DateTime parsedBirthDate;
                        BirthDateResult bd = new BirthDateResult
                        {
                            Value = DateTime.TryParse(input.Value, out parsedBirthDate) ? parsedBirthDate : DateTime.Now
                        }.WithCreateAudit(userName);
                        context.BirthDateResults.Add(bd);
                        context.SaveChanges();
                        component.BirthDateResultId = bd.Id;
                        break;

                    case "ImageUpload":
                        ImageUploadResult iu = new ImageUploadResult
                        {
                            FilePath = input.FilePath ?? string.Empty
                        }.WithCreateAudit(userName);
                        context.ImageUploadResults.Add(iu);
                        context.SaveChanges();
                        component.ImageUploadResultId = iu.Id;
                        break;

                    case "ImageUploadWithImageContent":
                        ImageUploadWithImageContentResult iuc = new ImageUploadWithImageContentResult
                        {
                            FilePath = input.FilePath ?? string.Empty
                        }.WithCreateAudit(userName);
                        context.ImageUploadWithImageContentResults.Add(iuc);
                        context.SaveChanges();
                        component.ImageUploadWithImageContentResultId = iuc.Id;
                        break;
                }

                component.WithUpdateAudit(userName);
            }

            context.SaveChanges();
        }

        public static object? GetTemplate(BeautyPagrantContext context, int formTemplateId)
        {
            FormTemplate? template = context.FormTemplates
                .Include(t => t.FormComponentTemplates)
                .FirstOrDefault(t => t.Id == formTemplateId);

            if (template == null) return null;

            List<object> components = new List<object>();
            foreach (FormComponentTemplate c in template.FormComponentTemplates)
            {
                components.Add(new
                {
                    Id = c.Id,
                    ComponentType = c.ComponentType
                });
            }

            return new
            {
                template.Id,
                template.Topic,
                Components = components
            };
        }

        public static object? GetForm(BeautyPagrantContext context, int formId)
        {
            Form? form = context.Forms
                .Include(f => f.FormComponents)
                .ThenInclude(fc => fc.TextFieldResult)
                .Include(f => f.FormComponents)
                .ThenInclude(fc => fc.SingleSelectionResult)
                .Include(f => f.FormComponents)
                .ThenInclude(fc => fc.DateResult)
                .Include(f => f.FormComponents)
                .ThenInclude(fc => fc.BirthDateResult)
                .Include(f => f.FormComponents)
                .ThenInclude(fc => fc.ImageUploadResult)
                .Include(f => f.FormComponents)
                .ThenInclude(fc => fc.ImageUploadWithImageContentResult)
                .FirstOrDefault(f => f.Id == formId);

            if (form == null) return null;

            List<object> components = new List<object>();
            foreach (FormComponent fc in form.FormComponents)
            {
                string? value = null;
                if (fc.TextFieldResult != null) value = fc.TextFieldResult.Value;
                else if (fc.DateResult != null) value = fc.DateResult.Value.ToString("yyyy-MM-dd");
                else if (fc.BirthDateResult != null) value = fc.BirthDateResult.Value.ToString("yyyy-MM-dd");
                else if (fc.ImageUploadResult != null) value = fc.ImageUploadResult.FilePath;
                else if (fc.ImageUploadWithImageContentResult != null) value = fc.ImageUploadWithImageContentResult.FilePath;

                components.Add(new
                {
                    fc.Id,
                    fc.ComponentType,
                    Value = value,
                    IsActive = fc.SingleSelectionResult != null ? (bool?)fc.SingleSelectionResult.IsActive : null
                });
            }

            return new
            {
                form.Id,
                form.FormTemplateId,
                Components = components
            };
        }
    }
}
