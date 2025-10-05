using Backend_BeautyPagrant.Dto;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models
{
    public partial class Event
    {
        public static Event Create(BeautyPagrantContext context, EventCreateDto dto, string userName, string baseUrl)
        {
            DateTime now = DateTime.Now;

            if (dto.EndDate <= now)
            {
                throw new ArgumentException("EndDate ต้องมากกว่า StartDate (เวลาปัจจุบัน)");
            }

            Event ev = new Event
            {
                Name = dto.Name,
                IsFavorite = dto.IsFavorite,
                FileImage = dto.FileImage,
                StartDate = now,
                EndDate = dto.EndDate
            }.WithCreateAudit(userName);

            context.Events.Add(ev);

            if (dto.CategoryIds != null)
            {
                EventCategorize.Create(context, ev, dto.CategoryIds, userName);
            }

            if (dto.Components != null)
            {
                EventComponent.Create(context, ev, dto.Components, userName, baseUrl);
            }

            return ev;
        }
        public static Event? GetById(BeautyPagrantContext context, int id)
        {
            return context.Events
                .Include(e => e.EventComponents).ThenInclude(ec => ec.AboutU)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.Banner)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.Textbox)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.ImageWithCaption)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.FormTemplate)
                    .ThenInclude(ft => ft.FormComponentTemplates)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.GridFourImage)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.GridTwoColumn)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.ImageDesc)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.OneTopicImageCationButton)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.TwoTopicImageCationButton)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.Sale)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.Section)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.TableWithTopicAndDesc)
                .Include(e => e.EventCategorizes)
                .FirstOrDefault(e => e.Id == id && !e.IsDelete);
        }
        public static List<Event> GetAll(BeautyPagrantContext context)
        {
            return context.Events
                .Include(e => e.EventComponents).ThenInclude(ec => ec.AboutU)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.Banner)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.Textbox)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.ImageWithCaption)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.FormTemplate)
                    .ThenInclude(ft => ft.FormComponentTemplates)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.GridFourImage)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.GridTwoColumn)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.ImageDesc)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.OneTopicImageCationButton)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.TwoTopicImageCationButton)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.Sale)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.Section)
                .Include(e => e.EventComponents).ThenInclude(ec => ec.TableWithTopicAndDesc)
                .Include(e => e.EventCategorizes)
                .Where(e => !e.IsDelete)
                .ToList();
        }
        public static void Update(BeautyPagrantContext context, int id, EventCreateDto dto, string userName, string baseUrl)
        {
            Event? ev = GetById(context, id);
            if (ev == null)
            {
                throw new KeyNotFoundException($"Event with id {id} not found");
            }

            DateTime now = DateTime.Now;
            if (dto.EndDate <= now)
            {
                throw new ArgumentException("EndDate ต้องมากกว่าเวลาปัจจุบัน");
            }

            ev.Name = dto.Name;
            ev.IsFavorite = dto.IsFavorite;
            ev.FileImage = dto.FileImage;
            ev.EndDate = dto.EndDate;
            ev.WithUpdateAudit(userName);

            if (dto.CategoryIds != null)
            {
                EventCategorize.Update(context, ev, dto.CategoryIds, userName);
            }

            if (dto.Components != null)
            {
                EventComponent.Update(context, ev, dto.Components, userName, baseUrl);
            }

        }
        public static void Delete(BeautyPagrantContext context, int id, string userName)
        {
            Event? ev = GetById(context, id);
            if (ev == null)
            {
                throw new KeyNotFoundException($"Event with id {id} not found");
            }

            ev.WithDeleteAudit(userName);

            foreach (EventCategorize cat in ev.EventCategorizes.Where(c => !c.IsDelete))
            {
                cat.WithDeleteAudit(userName);
            }

            foreach (EventComponent comp in ev.EventComponents.Where(c => !c.IsDelete))
            {
                comp.Delete(context, userName);
            }

        }
        public static Event Duplicate(BeautyPagrantContext context, int originalEventId, string userName)
        {
            Event? original = GetById(context, originalEventId);
            if (original == null)
                throw new ArgumentException("Event not found");

            Event newEvent = new Event
            {
                Name = original.Name + " (Copy)",
                IsFavorite = original.IsFavorite,
                FileImage = original.FileImage,
                StartDate = DateTime.Now,
                EndDate = original.EndDate,
            }.WithCreateAudit(userName);

            context.Events.Add(newEvent);
            context.SaveChanges();

            foreach (EventCategorize ec in original.EventCategorizes.Where(c => !c.IsDelete))
            {
                EventCategorize copy = EventCategorize.Duplicate(newEvent, ec, userName);
                context.EventCategorizes.Add(copy);
            }

            foreach (EventComponent comp in original.EventComponents.Where(c => !c.IsDelete))
            {
                EventComponent.Duplicate(context, newEvent, comp, userName);
            }

            return newEvent;
        }

    }
}
