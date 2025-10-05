namespace Backend_BeautyPagrant.Models
{
    public partial class EventCategorize
    {
        public static void Create(BeautyPagrantContext context, Event ev, List<int> categoryIds,string userName)
        {
            DateTime now = DateTime.Now;

            foreach (int categoryId in categoryIds)
            {
                EventCategorize ec = new EventCategorize
                {
                    Event = ev,
                    CategoryId = categoryId
                }.WithCreateAudit(userName);

                context.EventCategorizes.Add(ec);
            }
        }
        public static void Update(BeautyPagrantContext context, Event ev, List<int> categoryIds, string userName)
        {
            List<EventCategorize> existing = context.EventCategorizes
                .Where(ec => ec.Event.Id == ev.Id && !ec.IsDelete)
                .ToList();

            HashSet<int> existingIds = new HashSet<int>(existing.Select(ec => ec.CategoryId));
            List<int> toAdd = categoryIds.Where(catId => !existingIds.Contains(catId)).ToList();

            foreach (int catId in toAdd)
            {
                EventCategorize ec = new EventCategorize
                {
                    Event = ev,
                    CategoryId = catId
                }.WithCreateAudit(userName);

                context.EventCategorizes.Add(ec);
            }

            List<EventCategorize> toDelete = existing
                .Where(ec => !categoryIds.Contains(ec.CategoryId))
                .ToList();

            foreach (EventCategorize ec in toDelete)
            {
                ec.WithDeleteAudit(userName);
            }
        }
        public static void Delete(BeautyPagrantContext context, Event ev, List<int> categoryIds, string userName)
        {
            List<EventCategorize> existing = context.EventCategorizes
                .Where(ec => ec.Event.Id == ev.Id && !ec.IsDelete)
                .ToList();

            foreach (EventCategorize ec in existing)
            {
                if (categoryIds.Contains(ec.CategoryId))
                {
                    ec.WithDeleteAudit(userName);
                }
            }
        }
        public static EventCategorize Duplicate(Event newEvent, EventCategorize original, string userName)
        {
            EventCategorize copy = new EventCategorize
            {
                EventId = newEvent.Id,
                CategoryId = original.CategoryId
            }.WithCreateAudit(userName);

            return copy;
        }


    }
}
