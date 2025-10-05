using Backend_BeautyPagrant.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Backend_BeautyPagrant.Models
{
    public partial class ContactPicture
    {
        public static ContactPicture Create(BeautyPagrantContext context, Contact contact, ContactPictureDto dto, string userName)
        {

            ContactPicture picture = new ContactPicture
            {
                ContactId = contact.Id,
                Image = dto.ImageId,
                Url = dto.Url ?? string.Empty
            }.WithCreateAudit(userName);

            context.ContactPictures.Add(picture);
            return picture;
        }

        public static void Update(BeautyPagrantContext context, int contactId, List<ContactPictureDto> dtoList, string userName)
        {
            List<ContactPicture> existing = context.ContactPictures
                .Where(p => p.ContactId == contactId && !p.IsDelete)
                .ToList();

            HashSet<int> incomingIds = new HashSet<int>(
                dtoList != null
                    ? dtoList.Where(d => d != null && d.Id.HasValue).Select(d => d.Id!.Value)
                    : new List<int>());

            if (dtoList != null)
            {
                foreach (ContactPictureDto d in dtoList)
                {
                    if (d == null || !d.Id.HasValue) continue;

                    ContactPicture? match = existing.FirstOrDefault(p => p.Id == d.Id.Value);
                    if (match == null) continue;

                    match.Image = d.ImageId ?? string.Empty;
                    match.Url = d.Url ?? string.Empty;
                    match.WithUpdateAudit(userName);
                }
            }

            if (dtoList != null)
            {
                List<ContactPicture> toAdd = dtoList
                    .Where(d => d != null && !d.Id.HasValue && !string.IsNullOrWhiteSpace(d.ImageId))
                    .Select(d => new ContactPicture
                    {
                        ContactId = contactId,
                        Image = d.ImageId!,
                        Url = d.Url ?? string.Empty
                    }.WithCreateAudit(userName))
                    .ToList();

                if (toAdd.Count > 0)
                {
                    context.ContactPictures.AddRange(toAdd);
                }
            }

            foreach (ContactPicture oldPic in existing)
            {
                if (!incomingIds.Contains(oldPic.Id))
                {
                    oldPic.WithDeleteAudit(userName);
                }
            }
        }

        public static void Delete(BeautyPagrantContext context, int id, string userName)
        {
            ContactPicture? picture = context.ContactPictures
                .FirstOrDefault(p => p.Id == id && !p.IsDelete);
            if (picture == null) return;

            picture.WithDeleteAudit(userName);
        }

        public static ContactPicture Upload(BeautyPagrantContext context, Contact contact, IFormFile file, string? url, string userName, string rootPath)
        {
            if (contact == null || contact.IsDelete)
                throw new ArgumentException("Contact is invalid.");

            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty.");

            string uploadPath = Path.Combine(rootPath, "uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadPath, fileName);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            ContactPicture picture = new ContactPicture
            {
                ContactId = contact.Id,
                Image = "/uploads/" + fileName,
                Url = url ?? string.Empty
            }.WithCreateAudit(userName);

            context.ContactPictures.Add(picture);
            return picture;
        }
    }
}
