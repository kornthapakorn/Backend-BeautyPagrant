using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;

namespace Backend_BeautyPagrant.Services
{
    public class EventAppService : IEventAppService
    {
        private readonly IEventComponentImageBinder _binder;

        public EventAppService(IEventComponentImageBinder binder)
        {
            _binder = binder;
        }

        public Event Create(BeautyPagrantContext context, EventCreateDto dto, string userName, string baseUrl,
                            IFormFileCollection files)
        {
            Dictionary<string, IFormFile> map = ToMap(files);
            _binder.BindMany(dto.Components, map);

            Event ev = Event.Create(context, dto, userName, baseUrl);
            context.SaveChanges();
            return ev;
        }

        public Event Update(BeautyPagrantContext context, int id, EventCreateDto dto, string userName, string baseUrl,
                            IFormFileCollection files)
        {
            Dictionary<string, IFormFile> map = ToMap(files);
            _binder.BindMany(dto.Components, map);

            Event.Update(context, id, dto, userName, baseUrl);
            context.SaveChanges();
            return Event.GetById(context, id);
        }

        private static Dictionary<string, IFormFile> ToMap(IFormFileCollection files)
        {
            Dictionary<string, IFormFile> dict = new Dictionary<string, IFormFile>();
            if (files == null) return dict;

            foreach (IFormFile f in files)
            {
                // f.Name = ชื่อคีย์ที่ front ส่งมา เช่น "banner.image", "gridfourimage.image1" ฯลฯ
                string key = (f.Name ?? string.Empty).ToLowerInvariant();
                dict[key] = f; // ถ้าคีย์ซ้ำ ให้ตัวหลังทับตัวก่อน
            }
            return dict;
        }
    }
}
