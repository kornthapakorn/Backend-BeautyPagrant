using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class EventComponent
    {
        public static void Create(BeautyPagrantContext context, Event ev, List<EventComponentDto> dtoList, string userName, string baseUrl)
        {
            foreach (EventComponentDto dto in dtoList)
            {
                int? createdId = null;

                switch (dto.ComponentType)
                {
                    case "aboutu":
                        AboutU aboutU = AboutU.CreateFromDto(dto.AboutU!, userName);
                        context.AboutUs.Add(aboutU);
                        context.SaveChanges();
                        createdId = aboutU.Id;
                        break;

                    case "banner":
                        Banner banner = Banner.CreateFromDto(dto.Banner!, userName);
                        context.Banners.Add(banner);
                        context.SaveChanges();
                        createdId = banner.Id;
                        break;

                    case "button":
                        Button button = Button.CreateFromDto(dto.Button!, userName);
                        context.Buttons.Add(button);
                        context.SaveChanges();
                        createdId = button.Id;
                        break;

                    case "formtemplate":
                        FormTemplate formTemplate = FormTemplate.CreateFromDto(context,dto.FormTemplate!,userName,baseUrl);
                        createdId = formTemplate.Id;

                        break;

                    case "gridfourimage":
                        GridFourImage grid4 = GridFourImage.CreateFromDto(dto.GridFourImage!, userName);
                        context.GridFourImages.Add(grid4);
                        context.SaveChanges();
                        createdId = grid4.Id;
                        break;

                    case "gridtwocolumn":
                        GridTwoColumn grid2 = GridTwoColumn.CreateFromDto(dto.GridTwoColumn!, userName);
                        context.GridTwoColumns.Add(grid2);
                        context.SaveChanges();
                        createdId = grid2.Id;
                        break;

                    case "imagedesc":
                        ImageDesc imgDesc = ImageDesc.CreateFromDto(dto.ImageDesc!, userName);
                        context.ImageDescs.Add(imgDesc);
                        context.SaveChanges();
                        createdId = imgDesc.Id;
                        break;

                    case "imagewithcaption":
                        ImageWithCaption imgCap = ImageWithCaption.CreateFromDto(dto.ImageWithCaption!, userName);
                        context.ImageWithCaptions.Add(imgCap);
                        context.SaveChanges();
                        createdId = imgCap.Id;
                        break;

                    case "onetopicimagecaptionbutton":
                        OneTopicImageCaptionButton oneTopic = OneTopicImageCaptionButton.CreateFromDto(dto.OneTopicImageCaptionButton!, userName);
                        context.OneTopicImageCaptionButtons.Add(oneTopic);
                        context.SaveChanges();
                        createdId = oneTopic.Id;
                        break;

                    case "sale":
                        Sale sale = Sale.CreateFromDto(dto.Sale!, userName);
                        context.Sales.Add(sale);
                        context.SaveChanges();
                        createdId = sale.Id;
                        break;

                    case "section":
                        Section section = Section.CreateFromDto(userName);
                        context.Sections.Add(section);
                        context.SaveChanges();
                        createdId = section.Id;
                        break;

                    case "tablewithtopicanddesc":
                        TableWithTopicAndDesc table = TableWithTopicAndDesc.CreateFromDto(dto.TableWithTopicAndDesc!, userName);
                        context.TableWithTopicAndDescs.Add(table);
                        context.SaveChanges();
                        createdId = table.Id;
                        break;

                    case "textbox":
                        TextBox textBox = TextBox.CreateFromDto(dto.TextBox!, userName);
                        context.TextBoxes.Add(textBox);
                        context.SaveChanges();
                        createdId = textBox.Id;
                        break;

                    case "twotopicimagecaptionbutton":
                        TwoTopicImageCaptionButton twoTopic = TwoTopicImageCaptionButton.CreateFromDto(dto.TwoTopicImageCaptionButton!, userName);
                        context.TwoTopicImageCaptionButtons.Add(twoTopic);
                        context.SaveChanges();
                        createdId = twoTopic.Id;
                        break;
                }

                EventComponent comp = new EventComponent
                {
                    Event = ev,
                    ComponentType = dto.ComponentType,
                    SectionId = dto.ComponentType == "section" ? createdId : null,
                    BannerId = dto.ComponentType == "banner" ? createdId : null,
                    TextboxId = dto.ComponentType == "textBox" ? createdId : null,
                    ImageWithCaptionId = dto.ComponentType == "imagewithcaption" ? createdId : null,
                    GridTwoColumnId = dto.ComponentType == "gridtwocolumn" ? createdId : null,
                    ImageDescId = dto.ComponentType == "imagedesc" ? createdId : null,
                    GridFourImageId = dto.ComponentType == "gridfourimage" ? createdId : null,
                    TableWithTopicAndDescId = dto.ComponentType == "tablewithtopicanddesc" ? createdId : null,
                    OneTopicImageCationButtonId = dto.ComponentType == "onetopicimagecaptionbutton" ? createdId : null,
                    TwoTopicImageCationButtonId = dto.ComponentType == "twotopicimagecaptionbutton" ? createdId : null,
                    SaleId = dto.ComponentType == "sale" ? createdId : null,
                    ButtonId = dto.ComponentType == "button" ? createdId : null,
                    AboutUId = dto.ComponentType == "aboutu" ? createdId : null,
                    FormTemplateId = dto.ComponentType == "formtemplate" ? createdId : null,
                    SortOrder = dto.SortOrder,
                    IsOutPage = dto.IsOutPage
                }.WithCreateAudit(userName);

                context.EventComponents.Add(comp);
            }
        }
        public static void Update(BeautyPagrantContext context, Event ev, List<EventComponentDto> dtoList, string userName, string baseUrl)
        {
            List<EventComponent> existingComponents = context.EventComponents
                .Where(c => c.Event.Id == ev.Id && !c.IsDelete)
                .ToList();

            foreach (EventComponentDto dto in dtoList)
            {
                EventComponent comp = existingComponents.FirstOrDefault(c => c.Id == dto.Id && c.ComponentType == dto.ComponentType);

                if (comp != null)
                {
                    switch (dto.ComponentType)
                    {
                        case "aboutu": comp.AboutU?.UpdateFromDto(dto.AboutU!, userName); break;
                        case "banner": comp.Banner?.UpdateFromDto(dto.Banner!, userName); break;
                        case "button": comp.Button?.UpdateFromDto(dto.Button!, userName); break;
                        case "formtemplate": comp.FormTemplate?.UpdateFromDto(context, dto.FormTemplate!, userName); break;
                        case "gridfourimage": comp.GridFourImage?.UpdateFromDto(dto.GridFourImage!, userName); break;
                        case "gridtwocolumn": comp.GridTwoColumn?.UpdateFromDto(dto.GridTwoColumn!, userName); break;
                        case "imagedesc": comp.ImageDesc?.UpdateFromDto(dto.ImageDesc!, userName); break;
                        case "imagewithcaption": comp.ImageWithCaption?.UpdateFromDto(dto.ImageWithCaption!, userName); break;
                        case "onetopicimagecaptionbutton": comp.OneTopicImageCationButton?.UpdateFromDto(dto.OneTopicImageCaptionButton!, userName); break;
                        case "twotopicimagecaptionbutton": comp.TwoTopicImageCationButton?.UpdateFromDto(dto.TwoTopicImageCaptionButton!, userName); break;
                        case "sale": comp.Sale?.UpdateFromDto(dto.Sale!, userName); break;
                        case "section": comp.Section?.UpdateFromDto(userName); break;
                        case "tablewithtopicanddesc": comp.TableWithTopicAndDesc?.UpdateFromDto(dto.TableWithTopicAndDesc!, userName); break;
                        case "textbox": comp.Textbox?.UpdateFromDto(dto.TextBox!, userName); break;
                    }

                    comp.SortOrder = dto.SortOrder;
                    comp.IsOutPage = dto.IsOutPage;
                    comp.WithUpdateAudit(userName);
                }
                else
                {
                    Create(context, ev, new List<EventComponentDto> { dto }, userName, baseUrl);
                }
            }

            HashSet<int> dtoIds = new HashSet<int>(dtoList.Where(d => d.Id > 0).Select(d => d.Id));
            foreach (EventComponent old in existingComponents)
            {
                if (!dtoIds.Contains(old.Id))
                {
                    old.WithDeleteAudit(userName);
                }
            }
        }
        public void Delete(BeautyPagrantContext context, string userName)
        {
            this.WithDeleteAudit(userName);

            switch (this.ComponentType)
            {
                case "FormTemplate":
                    if (this.FormTemplate != null)
                    {
                        this.FormTemplate.WithDeleteAudit(userName);

                        List<FormComponentTemplate> formComponents = context.FormComponentTemplates
                            .Where(fc => fc.FormId == this.FormTemplate.Id && !fc.IsDelete)
                            .ToList();

                        foreach (FormComponentTemplate fc in formComponents)
                        {
                            fc.Delete(userName);
                        }
                    }
                    break;
                case "AboutU": this.AboutU?.WithDeleteAudit(userName); break;
                case "Banner": this.Banner?.WithDeleteAudit(userName); break;
                case "Button": this.Button?.WithDeleteAudit(userName); break;
                case "GridFourImage": this.GridFourImage?.WithDeleteAudit(userName); break;
                case "GridTwoColumn": this.GridTwoColumn?.WithDeleteAudit(userName); break;
                case "ImageDesc": this.ImageDesc?.WithDeleteAudit(userName); break;
                case "ImageWithCaption": this.ImageWithCaption?.WithDeleteAudit(userName); break;
                case "OneTopicImageCaptionButton": this.OneTopicImageCationButton?.WithDeleteAudit(userName); break;
                case "TwoTopicImageCaptionButton": this.TwoTopicImageCationButton?.WithDeleteAudit(userName); break;
                case "Sale": this.Sale?.WithDeleteAudit(userName); break;
                case "Section": this.Section?.WithDeleteAudit(userName); break;
                case "TableWithTopicAndDesc": this.TableWithTopicAndDesc?.WithDeleteAudit(userName); break;
                case "TextBox": this.Textbox?.WithDeleteAudit(userName); break;
            }
        }
        public static void Duplicate(BeautyPagrantContext context, Event newEvent, EventComponent original, string userName)
        {
            int? createdId = null;

            switch (original.ComponentType)
            {
                case "AboutU":
                    AboutU newAboutU = original.AboutU.Duplicate(userName);
                    context.AboutUs.Add(newAboutU);
                    context.SaveChanges();
                    createdId = newAboutU.Id;
                    break;

                case "Banner":
                    Banner newBanner = original.Banner.Duplicate(userName);
                    context.Banners.Add(newBanner);
                    context.SaveChanges();
                    createdId = newBanner.Id;
                    break;

                case "Button":
                    Button newButton = original.Button.Duplicate(userName);
                    context.Buttons.Add(newButton);
                    context.SaveChanges();
                    createdId = newButton.Id;
                    break;

                case "FormTemplate":
                    if (original.FormTemplate != null)
                    {
                        FormTemplate newForm = original.FormTemplate.Duplicate(context, userName);
                        createdId = newForm.Id;
                    }
                    break;



                case "GridFourImage":
                    GridFourImage newGrid4 = original.GridFourImage.Duplicate(userName);
                    context.GridFourImages.Add(newGrid4);
                    context.SaveChanges();
                    createdId = newGrid4.Id;
                    break;

                case "GridTwoColumn":
                    GridTwoColumn newGrid2 = original.GridTwoColumn.Duplicate(userName);
                    context.GridTwoColumns.Add(newGrid2);
                    context.SaveChanges();
                    createdId = newGrid2.Id;
                    break;

                case "ImageDesc":
                    ImageDesc newImgDesc = original.ImageDesc.Duplicate(userName);
                    context.ImageDescs.Add(newImgDesc);
                    context.SaveChanges();
                    createdId = newImgDesc.Id;
                    break;

                case "ImageWithCaption":
                    ImageWithCaption newImgCap = original.ImageWithCaption.Duplicate(userName);
                    context.ImageWithCaptions.Add(newImgCap);
                    context.SaveChanges();
                    createdId = newImgCap.Id;
                    break;

                case "OneTopicImageCaptionButton":
                    OneTopicImageCaptionButton newOneTopic = original.OneTopicImageCationButton.Duplicate(userName);
                    context.OneTopicImageCaptionButtons.Add(newOneTopic);
                    context.SaveChanges();
                    createdId = newOneTopic.Id;
                    break;

                case "Sale":
                    Sale newSale = original.Sale.Duplicate(userName);
                    context.Sales.Add(newSale);
                    context.SaveChanges();
                    createdId = newSale.Id;
                    break;

                case "Section":
                    Section newSection = original.Section.Duplicate(userName);
                    context.Sections.Add(newSection);
                    context.SaveChanges();
                    createdId = newSection.Id;
                    break;

                case "TableWithTopicAndDesc":
                    TableWithTopicAndDesc newTable = original.TableWithTopicAndDesc.Duplicate(userName);
                    context.TableWithTopicAndDescs.Add(newTable);
                    context.SaveChanges();
                    createdId = newTable.Id;
                    break;

                case "TextBox":
                    TextBox newTextBox = original.Textbox.Duplicate(userName);
                    context.TextBoxes.Add(newTextBox);
                    context.SaveChanges();
                    createdId = newTextBox.Id;
                    break;

                case "TwoTopicImageCaptionButton":
                    TwoTopicImageCaptionButton newTwoTopic = original.TwoTopicImageCationButton.Duplicate(userName);
                    context.TwoTopicImageCaptionButtons.Add(newTwoTopic);
                    context.SaveChanges();
                    createdId = newTwoTopic.Id;
                    break;
            }

            EventComponent copy = new EventComponent
            {
                Event = newEvent,
                ComponentType = original.ComponentType,
                AboutUId = original.ComponentType == "AboutU" ? createdId : null,
                BannerId = original.ComponentType == "Banner" ? createdId : null,
                ButtonId = original.ComponentType == "Button" ? createdId : null,
                FormTemplateId = original.ComponentType == "FormTemplate" ? createdId : null,
                GridFourImageId = original.ComponentType == "GridFourImage" ? createdId : null,
                GridTwoColumnId = original.ComponentType == "GridTwoColumn" ? createdId : null,
                ImageDescId = original.ComponentType == "ImageDesc" ? createdId : null,
                ImageWithCaptionId = original.ComponentType == "ImageWithCaption" ? createdId : null,
                OneTopicImageCationButtonId = original.ComponentType == "OneTopicImageCaptionButton" ? createdId : null,
                SaleId = original.ComponentType == "Sale" ? createdId : null,
                SectionId = original.ComponentType == "Section" ? createdId : null,
                TableWithTopicAndDescId = original.ComponentType == "TableWithTopicAndDesc" ? createdId : null,
                TextboxId = original.ComponentType == "TextBox" ? createdId : null,
                TwoTopicImageCationButtonId = original.ComponentType == "TwoTopicImageCaptionButton" ? createdId : null,
                SortOrder = original.SortOrder,
                IsOutPage = original.IsOutPage
            }.WithCreateAudit(userName);

            context.EventComponents.Add(copy);
        }
    }
}
