using System.Collections.Generic;
using System.Linq;
using Backend_BeautyPagrant.Dto;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models
{
    public partial class EventComponent
    {
        // ==============================
        // CREATE (recursive section)
        // ==============================
        public static void Create(
            BeautyPagrantContext context,
            Event ev,
            List<EventComponentDto> dtoList,
            string userName,
            string baseUrl,
            int? parentSectionId = null)
        {
            foreach (EventComponentDto dto in dtoList)
            {
                string componentType = (dto.ComponentType ?? string.Empty).ToLowerInvariant();
                int? createdId = null;

                // ---------- section ----------
                if (componentType == "section")
                {
                    Section section = Section.CreateFromDto(userName);
                    context.Sections.Add(section);
                    context.SaveChanges();

                    EventComponent sectionComponent = new EventComponent
                    {
                        EventId = ev.Id,
                        ComponentType = "section", // เก็บเป็นตัวเล็กเสมอ
                        SectionId = section.Id,
                        SortOrder = dto.SortOrder,
                        IsOutPage = dto.IsOutPage
                    }.WithCreateAudit(userName);

                    context.EventComponents.Add(sectionComponent);
                    context.SaveChanges();

                    // recursive สร้างลูกใน section นี้
                    if (dto.Section != null && dto.Section.Components.Count > 0)
                    {
                        Create(context, ev, dto.Section.Components, userName, baseUrl, section.Id);
                    }

                    continue;
                }

                // ---------- component อื่น ๆ ----------
                if (componentType == "aboutu")
                {
                    AboutU aboutU = AboutU.CreateFromDto(dto.AboutU!, userName);
                    context.AboutUs.Add(aboutU);
                    context.SaveChanges();
                    createdId = aboutU.Id;
                }
                else if (componentType == "banner")
                {
                    Banner banner = Banner.CreateFromDto(dto.Banner!, userName);
                    context.Banners.Add(banner);
                    context.SaveChanges();
                    createdId = banner.Id;
                }
                else if (componentType == "button")
                {
                    Button button = Button.CreateFromDto(dto.Button!, userName);
                    context.Buttons.Add(button);
                    context.SaveChanges();
                    createdId = button.Id;
                }
                else if (componentType == "formtemplate")
                {
                    FormTemplate formTemplate = FormTemplate.CreateFromDto(context, dto.FormTemplate!, userName, baseUrl);
                    createdId = formTemplate.Id;
                }
                else if (componentType == "gridfourimage")
                {
                    GridFourImage grid4 = GridFourImage.CreateFromDto(dto.GridFourImage!, userName);
                    context.GridFourImages.Add(grid4);
                    context.SaveChanges();
                    createdId = grid4.Id;
                }
                else if (componentType == "gridtwocolumn")
                {
                    GridTwoColumn grid2 = GridTwoColumn.CreateFromDto(dto.GridTwoColumn!, userName);
                    context.GridTwoColumns.Add(grid2);
                    context.SaveChanges();
                    createdId = grid2.Id;
                }
                else if (componentType == "imagedesc")
                {
                    ImageDesc imgDesc = ImageDesc.CreateFromDto(dto.ImageDesc!, userName);
                    context.ImageDescs.Add(imgDesc);
                    context.SaveChanges();
                    createdId = imgDesc.Id;
                }
                else if (componentType == "imagewithcaption")
                {
                    ImageWithCaption imgCap = ImageWithCaption.CreateFromDto(dto.ImageWithCaption!, userName);
                    context.ImageWithCaptions.Add(imgCap);
                    context.SaveChanges();
                    createdId = imgCap.Id;
                }
                else if (componentType == "onetopicimagecaptionbutton")
                {
                    OneTopicImageCaptionButton oneTopic = OneTopicImageCaptionButton.CreateFromDto(dto.OneTopicImageCaptionButton!, userName);
                    context.OneTopicImageCaptionButtons.Add(oneTopic);
                    context.SaveChanges();
                    createdId = oneTopic.Id;
                }
                else if (componentType == "sale")
                {
                    Sale sale = Sale.CreateFromDto(dto.Sale!, userName);
                    context.Sales.Add(sale);
                    context.SaveChanges();
                    createdId = sale.Id;
                }
                else if (componentType == "tablewithtopicanddesc")
                {
                    TableWithTopicAndDesc table = TableWithTopicAndDesc.CreateFromDto(dto.TableWithTopicAndDesc!, userName);
                    context.TableWithTopicAndDescs.Add(table);
                    context.SaveChanges();
                    createdId = table.Id;
                }
                else if (componentType == "textbox")
                {
                    TextBox textBox = TextBox.CreateFromDto(dto.TextBox!, userName);
                    context.TextBoxes.Add(textBox);
                    context.SaveChanges();
                    createdId = textBox.Id;
                }
                else if (componentType == "twotopicimagecaptionbutton")
                {
                    TwoTopicImageCaptionButton twoTopic = TwoTopicImageCaptionButton.CreateFromDto(dto.TwoTopicImageCaptionButton!, userName);
                    context.TwoTopicImageCaptionButtons.Add(twoTopic);
                    context.SaveChanges();
                    createdId = twoTopic.Id;
                }
                else
                {
                    // ไม่รู้จักชนิด → ข้าม
                    continue;
                }

                // ---------- บันทึก eventcomponent ----------
                EventComponent comp = new EventComponent
                {
                    EventId = ev.Id,
                    ComponentType = componentType, // เก็บเป็นตัวเล็กเสมอ
                    SectionId = parentSectionId,

                    BannerId = componentType == "banner" ? createdId : null,
                    TextboxId = componentType == "textbox" ? createdId : null,
                    ImageWithCaptionId = componentType == "imagewithcaption" ? createdId : null,
                    GridTwoColumnId = componentType == "gridtwocolumn" ? createdId : null,
                    ImageDescId = componentType == "imagedesc" ? createdId : null,
                    GridFourImageId = componentType == "gridfourimage" ? createdId : null,
                    TableWithTopicAndDescId = componentType == "tablewithtopicanddesc" ? createdId : null,
                    OneTopicImageCationButtonId = componentType == "onetopicimagecaptionbutton" ? createdId : null,
                    TwoTopicImageCationButtonId = componentType == "twotopicimagecaptionbutton" ? createdId : null,
                    SaleId = componentType == "sale" ? createdId : null,
                    ButtonId = componentType == "button" ? createdId : null,
                    AboutUId = componentType == "aboutu" ? createdId : null,
                    FormTemplateId = componentType == "formtemplate" ? createdId : null,

                    SortOrder = dto.SortOrder,
                    IsOutPage = dto.IsOutPage
                }.WithCreateAudit(userName);

                context.EventComponents.Add(comp);
                context.SaveChanges();
            }
        }

        // ==============================
        // UPDATE (recursive section)
        // ==============================
        public static void Update(
            BeautyPagrantContext context,
            Event ev,
            List<EventComponentDto> dtoList,
            string userName,
            string baseUrl,
            int? parentSectionId = null)
        {
            List<EventComponent> existingComponents = context.EventComponents
                .Where(c => c.EventId == ev.Id && !c.IsDelete)
                .ToList();

            foreach (EventComponentDto dto in dtoList)
            {
                string dtoType = (dto.ComponentType ?? string.Empty).ToLowerInvariant();

                EventComponent comp = existingComponents.FirstOrDefault(c =>
                    c.Id == dto.Id &&
                    (c.ComponentType ?? string.Empty).ToLowerInvariant() == dtoType &&
                    c.SectionId == parentSectionId);

                if (comp != null)
                {
                    switch (dtoType)
                    {
                        case "aboutu": if (comp.AboutU != null) { comp.AboutU.UpdateFromDto(dto.AboutU!, userName); } break;
                        case "banner": if (comp.Banner != null) { comp.Banner.UpdateFromDto(dto.Banner!, userName); } break;
                        case "button": if (comp.Button != null) { comp.Button.UpdateFromDto(dto.Button!, userName); } break;
                        case "formtemplate": if (comp.FormTemplate != null) { comp.FormTemplate.UpdateFromDto(context, dto.FormTemplate!, userName); } break;
                        case "gridfourimage": if (comp.GridFourImage != null) { comp.GridFourImage.UpdateFromDto(dto.GridFourImage!, userName); } break;
                        case "gridtwocolumn": if (comp.GridTwoColumn != null) { comp.GridTwoColumn.UpdateFromDto(dto.GridTwoColumn!, userName); } break;
                        case "imagedesc": if (comp.ImageDesc != null) { comp.ImageDesc.UpdateFromDto(dto.ImageDesc!, userName); } break;
                        case "imagewithcaption": if (comp.ImageWithCaption != null) { comp.ImageWithCaption.UpdateFromDto(dto.ImageWithCaption!, userName); } break;
                        case "onetopicimagecaptionbutton": if (comp.OneTopicImageCationButton != null) { comp.OneTopicImageCationButton.UpdateFromDto(dto.OneTopicImageCaptionButton!, userName); } break;
                        case "twotopicimagecaptionbutton": if (comp.TwoTopicImageCationButton != null) { comp.TwoTopicImageCationButton.UpdateFromDto(dto.TwoTopicImageCaptionButton!, userName); } break;
                        case "sale": if (comp.Sale != null) { comp.Sale.UpdateFromDto(dto.Sale!, userName); } break;
                        case "section":
                            if (comp.Section != null) { comp.Section.UpdateFromDto(userName); }
                            if (dto.Section != null && dto.Section.Components.Count > 0)
                            {
                                Update(context, ev, dto.Section.Components, userName, baseUrl, comp.SectionId);
                            }
                            break;
                        case "tablewithtopicanddesc": if (comp.TableWithTopicAndDesc != null) { comp.TableWithTopicAndDesc.UpdateFromDto(dto.TableWithTopicAndDesc!, userName); } break;
                        case "textbox": if (comp.Textbox != null) { comp.Textbox.UpdateFromDto(dto.TextBox!, userName); } break;
                    }

                    comp.SortOrder = dto.SortOrder;
                    comp.IsOutPage = dto.IsOutPage;
                    comp.WithUpdateAudit(userName);
                }
                else
                {
                    // ไม่พบของเดิม ⇒ สร้างใหม่ใน scope นี้
                    Create(context, ev, new List<EventComponentDto> { dto }, userName, baseUrl, parentSectionId);
                }
            }

            // ลบของเก่าที่ไม่อยู่ใน dto ภายใน scope นี้ (cascade)
            HashSet<int> dtoIds = new HashSet<int>(dtoList.Where(d => d.Id > 0).Select(d => d.Id));
            foreach (EventComponent old in existingComponents.Where(c =>
                c.SectionId == parentSectionId &&
                !string.Equals((c.ComponentType ?? string.Empty), "section", StringComparison.OrdinalIgnoreCase)))
            {
                if (!dtoIds.Contains(old.Id))
                {
                    old.Delete(context, userName); // ต้องใช้ Delete() เพื่อ cascade
                }
            }
        }

        // ==============================
        // DELETE (cascade, lowercase switch)
        // ==============================
        public void Delete(BeautyPagrantContext context, string userName)
        {
            this.WithDeleteAudit(userName);

            string type = (this.ComponentType ?? string.Empty).ToLowerInvariant();

            switch (type)
            {
                case "formtemplate":
                    if (this.FormTemplate != null)
                    {
                        this.FormTemplate.WithDeleteAudit(userName);

                        List<FormComponentTemplate> formComponents = context.FormComponentTemplates
                            .Where(fc => fc.FormId == this.FormTemplate.Id && !fc.IsDelete)
                            .ToList();

                        foreach (FormComponentTemplate fc in formComponents)
                        {
                            // ใช้เวอร์ชันที่มี context เพื่อ fallback ไปโหลดจาก FK ได้
                            fc.Delete(context, userName);
                        }
                    }
                    break;

                case "section":
                    if (this.Section != null)
                    {
                        this.Section.WithDeleteAudit(userName);
                    }

                    if (this.SectionId.HasValue)
                    {
                        // ตัดหัวตัวเองออก ไม่งั้นจะวนซ้ำ
                        List<EventComponent> sectionChildren = context.EventComponents
                            .Where(c => c.SectionId == this.SectionId.Value && !c.IsDelete && c.Id != this.Id)
                            .ToList();

                        foreach (EventComponent child in sectionChildren)
                        {
                            child.Delete(context, userName);
                        }
                    }
                    break;

                case "aboutu": if (this.AboutU != null) { this.AboutU.WithDeleteAudit(userName); } break;
                case "banner": if (this.Banner != null) { this.Banner.WithDeleteAudit(userName); } break;
                case "button": if (this.Button != null) { this.Button.WithDeleteAudit(userName); } break;
                case "gridfourimage": if (this.GridFourImage != null) { this.GridFourImage.WithDeleteAudit(userName); } break;
                case "gridtwocolumn": if (this.GridTwoColumn != null) { this.GridTwoColumn.WithDeleteAudit(userName); } break;
                case "imagedesc": if (this.ImageDesc != null) { this.ImageDesc.WithDeleteAudit(userName); } break;
                case "imagewithcaption": if (this.ImageWithCaption != null) { this.ImageWithCaption.WithDeleteAudit(userName); } break;
                case "onetopicimagecaptionbutton": if (this.OneTopicImageCationButton != null) { this.OneTopicImageCationButton.WithDeleteAudit(userName); } break;
                case "twotopicimagecaptionbutton": if (this.TwoTopicImageCationButton != null) { this.TwoTopicImageCationButton.WithDeleteAudit(userName); } break;
                case "sale": if (this.Sale != null) { this.Sale.WithDeleteAudit(userName); } break;
                case "tablewithtopicanddesc": if (this.TableWithTopicAndDesc != null) { this.TableWithTopicAndDesc.WithDeleteAudit(userName); } break;
                case "textbox": if (this.Textbox != null) { this.Textbox.WithDeleteAudit(userName); } break;
            }

            context.SaveChanges();
        }

        public static void Duplicate(BeautyPagrantContext context, Event newEvent, EventComponent original, string userName)
        {
            if (original == null || original.IsDelete) return;

            // กัน recursion ซ้ำในงานเดียวกัน
            HashSet<int> visited = new HashSet<int>();

            // ===== ทำงานจริง (คงเมธอดเดียว แต่มี local fn เพื่อส่ง parentSectionId) =====
            void DuplicateCore(EventComponent src, int? parentSectionId)
            {
                if (src == null || src.IsDelete) return;
                if (visited.Contains(src.Id)) return;
                visited.Add(src.Id);

                string type = (src.ComponentType ?? string.Empty).ToLowerInvariant();

                if (parentSectionId == null &&
                    src.SectionId.HasValue &&
                    type != "section")
                {
                    return;
                }

                int? createdId = null;

                switch (type)
                {
                    case "aboutu":
                        {
                            AboutU newAboutU = src.AboutU.Duplicate(userName);
                            context.AboutUs.Add(newAboutU);
                            context.SaveChanges();
                            createdId = newAboutU.Id;
                            break;
                        }

                    case "banner":
                        {
                            Banner newBanner = src.Banner.Duplicate(userName);
                            context.Banners.Add(newBanner);
                            context.SaveChanges();
                            createdId = newBanner.Id;
                            break;
                        }

                    case "button":
                        {
                            Button newButton = src.Button.Duplicate(userName);
                            context.Buttons.Add(newButton);
                            context.SaveChanges();
                            createdId = newButton.Id;
                            break;
                        }

                    case "formtemplate":
                        {
                            if (src.FormTemplate != null && !src.FormTemplate.IsDelete)
                            {
                                // 1) duplicate ตัวฟอร์ม (หัว)
                                FormTemplate newForm = src.FormTemplate.Duplicate(context, userName);
                                context.SaveChanges();

                                // 2) กันก็อปลูกซ้ำ: ถ้า newForm มีลูกแล้ว ไม่ต้องทำซ้ำ
                                bool hasChildren = context.FormComponentTemplates
                                    .Any(fc => fc.FormId == newForm.Id && !fc.IsDelete);

                                if (!hasChildren)
                                {
                                    List<FormComponentTemplate> originals = context.FormComponentTemplates
                                        .Where(fc => fc.FormId == src.FormTemplate.Id && !fc.IsDelete)
                                        .ToList();

                                    foreach (FormComponentTemplate fc in originals)
                                    {
                                        FormComponentTemplate.Duplicate(context, newForm, fc, userName);
                                    }
                                }

                                createdId = newForm.Id;
                            }
                            break;
                        }

                    case "gridfourimage":
                        {
                            GridFourImage newGrid4 = src.GridFourImage.Duplicate(userName);
                            context.GridFourImages.Add(newGrid4);
                            context.SaveChanges();
                            createdId = newGrid4.Id;
                            break;
                        }

                    case "gridtwocolumn":
                        {
                            GridTwoColumn newGrid2 = src.GridTwoColumn.Duplicate(userName);
                            context.GridTwoColumns.Add(newGrid2);
                            context.SaveChanges();
                            createdId = newGrid2.Id;
                            break;
                        }

                    case "imagedesc":
                        {
                            ImageDesc newImgDesc = src.ImageDesc.Duplicate(userName);
                            context.ImageDescs.Add(newImgDesc);
                            context.SaveChanges();
                            createdId = newImgDesc.Id;
                            break;
                        }

                    case "imagewithcaption":
                        {
                            ImageWithCaption newImgCap = src.ImageWithCaption.Duplicate(userName);
                            context.ImageWithCaptions.Add(newImgCap);
                            context.SaveChanges();
                            createdId = newImgCap.Id;
                            break;
                        }

                    case "onetopicimagecaptionbutton":
                        {
                            OneTopicImageCaptionButton newOne = src.OneTopicImageCationButton.Duplicate(userName);
                            context.OneTopicImageCaptionButtons.Add(newOne);
                            context.SaveChanges();
                            createdId = newOne.Id;
                            break;
                        }

                    case "twotopicimagecaptionbutton":
                        {
                            TwoTopicImageCaptionButton newTwo = src.TwoTopicImageCationButton.Duplicate(userName);
                            context.TwoTopicImageCaptionButtons.Add(newTwo);
                            context.SaveChanges();
                            createdId = newTwo.Id;
                            break;
                        }

                    case "sale":
                        {
                            Sale newSale = src.Sale.Duplicate(userName);
                            context.Sales.Add(newSale);
                            context.SaveChanges();
                            createdId = newSale.Id;
                            break;
                        }

                    case "section":
                        {
                            Section newSection = src.Section.Duplicate(userName);
                            context.Sections.Add(newSection);
                            context.SaveChanges();
                            createdId = newSection.Id;
                            break;
                        }

                    case "tablewithtopicanddesc":
                        {
                            TableWithTopicAndDesc newTable = src.TableWithTopicAndDesc.Duplicate(userName);
                            context.TableWithTopicAndDescs.Add(newTable);
                            context.SaveChanges();
                            createdId = newTable.Id;
                            break;
                        }

                    case "textbox":
                        {
                            TextBox newText = src.Textbox.Duplicate(userName);
                            context.TextBoxes.Add(newText);
                            context.SaveChanges();
                            createdId = newText.Id;
                            break;
                        }
                }

                // แถว eventcomponent ใหม่ (componenttype เก็บเป็นตัวเล็ก)
                EventComponent copy = new EventComponent
                {
                    Event = newEvent,
                    ComponentType = type,                         // เก็บเป็นตัวเล็ก

                    // FK: ใช้ชื่อพร็อพตามโมเดล (PascalCase)
                    SectionId = (type == "section" ? createdId : parentSectionId),

                    AboutUId = (type == "aboutu") ? createdId : null,
                    BannerId = (type == "banner") ? createdId : null,
                    ButtonId = (type == "button") ? createdId : null,
                    FormTemplateId = (type == "formtemplate") ? createdId : null,
                    GridFourImageId = (type == "gridfourimage") ? createdId : null,
                    GridTwoColumnId = (type == "gridtwocolumn") ? createdId : null,
                    ImageDescId = (type == "imagedesc") ? createdId : null,
                    ImageWithCaptionId = (type == "imagewithcaption") ? createdId : null,
                    OneTopicImageCationButtonId = (type == "onetopicimagecaptionbutton") ? createdId : null,
                    TwoTopicImageCationButtonId = (type == "twotopicimagecaptionbutton") ? createdId : null,
                    SaleId = (type == "sale") ? createdId : null,
                    TableWithTopicAndDescId = (type == "tablewithtopicanddesc") ? createdId : null,
                    TextboxId = (type == "textbox") ? createdId : null,

                    SortOrder = src.SortOrder,
                    IsOutPage = src.IsOutPage
                }.WithCreateAudit(userName);


                context.EventComponents.Add(copy);
                context.SaveChanges();

                // recursive: section → ก็อปลูก โดยส่ง parentSectionId = id ของ section ใหม่
                if (type == "section" && createdId.HasValue)
                {
                    int newSectionId = createdId.Value;

                    List<EventComponent> children = context.EventComponents
                        .Where(c => c.SectionId == src.SectionId && !c.IsDelete && c.Id != src.Id)
                        .ToList();

                    foreach (EventComponent child in children)
                    {
                        DuplicateCore(child, newSectionId);
                    }
                }
            }

            // เริ่มจาก original (top-level) parentSectionId = null
            DuplicateCore(original, null);
        }
    }
}
