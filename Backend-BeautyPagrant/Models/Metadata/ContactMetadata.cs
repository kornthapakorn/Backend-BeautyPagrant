using Backend_BeautyPagrant.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend_BeautyPagrant.Models
{
    public partial class Contact
    {
        public static List<Contact> GetAll(BeautyPagrantContext context)
        {
            List<Contact> contacts = context.Contacts
                .Where(c => !c.IsDelete)
                .OrderBy(c => c.Id)
                .ToList();

            return contacts;
        }

        public static Contact? GetById(BeautyPagrantContext context, int id)
        {
            Contact? contact = context.Contacts
                .FirstOrDefault(c => c.Id == id && !c.IsDelete);

            return contact;
        }

        public static Contact Create(BeautyPagrantContext context, ContactDto dto, string userName)
        {
            Contact contact = new Contact
            {
                Title = dto.Title
            }.WithCreateAudit(userName);

            context.Contacts.Add(contact);
            context.SaveChanges();

            if (dto.Pictures != null )
            {
                foreach (ContactPictureDto pdto in dto.Pictures)
                {
                    ContactPicture.Create(context, contact, pdto, userName);
                }
            }

            return contact;
        }

        public static void Update(BeautyPagrantContext context, int id, ContactDto dto, string userName)
        {
            Contact? contact = GetById(context, id);
            if (contact == null) return;

            contact.Title = dto.Title;
            contact.WithUpdateAudit(userName);

            List<ContactPictureDto> incoming = dto.Pictures ?? new List<ContactPictureDto>();
            ContactPicture.Update(context, contact.Id, incoming, userName);

        }

        public static void Delete(BeautyPagrantContext context, int id, string userName)
        {
            Contact? contact = GetById(context, id);
            if (contact == null) return;

            contact.WithDeleteAudit(userName);

            List<ContactPicture> pictures = context.ContactPictures
                .Where(p => p.ContactId == contact.Id && !p.IsDelete)
                .ToList();

            foreach (ContactPicture p in pictures)
            {
                p.WithDeleteAudit(userName);
            }
        }
    }
}
