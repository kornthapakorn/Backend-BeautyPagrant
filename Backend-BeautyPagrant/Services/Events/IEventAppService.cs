using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;

namespace Backend_BeautyPagrant.Services
{
    public interface IEventAppService
    {
        Event Create(BeautyPagrantContext context, EventCreateDto dto, string userName, string baseUrl,
                     IFormFileCollection files);   // <-- รับ IFormFileCollection
        Event Update(BeautyPagrantContext context, int id, EventCreateDto dto, string userName, string baseUrl,
                     IFormFileCollection files);   // <-- เช่นกัน
    }
}
