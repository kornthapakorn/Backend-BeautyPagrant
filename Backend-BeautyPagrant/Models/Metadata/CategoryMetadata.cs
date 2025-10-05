using Backend_BeautyPagrant.Dto;

namespace Backend_BeautyPagrant.Models
{
    public partial class Category
    {
        public static Category Create(BeautyPagrantContext context, CategoryCreateDto dto, string userName)
        {
            Category category = new Category
            {
                Name = dto.Name
            }.WithCreateAudit(userName);

            context.Categories.Add(category);
            return category;
        }

        public static void Update(BeautyPagrantContext context, List<CategoryCreateDto> dtoList, string userName)
        {
            List<Category> existingCategories = context.Categories
                .Where(c => !c.IsDelete)
                .ToList();

            List<CategoryCreateDto> toCreate = new List<CategoryCreateDto>();

            foreach (CategoryCreateDto dto in dtoList)
            {
                Category existing = existingCategories.FirstOrDefault(c => c.Id == dto.Id);

                if (existing != null)
                {
                    existing.Name = dto.Name;
                    existing.WithUpdateAudit(userName);
                }
                else
                {
                    toCreate.Add(dto);
                }
            }

            foreach (CategoryCreateDto dto in toCreate)
            {
                Create(context, dto, userName);
            }

            HashSet<int> dtoIds = new HashSet<int>(
                dtoList.Where(d => d.Id.HasValue).Select(d => d.Id.Value)
            );

            foreach (Category old in existingCategories)
            {
                if (!dtoIds.Contains(old.Id))
                {
                    old.WithDeleteAudit(userName);
                }
            }
        }

        public void Delete(string userName)
        {
            this.WithDeleteAudit(userName);
        }
        public static List<Category> GetAll(BeautyPagrantContext context)
        {
            return context.Categories
                .Where(c => !c.IsDelete)
                .ToList();
        }
        public static Category? GetById(BeautyPagrantContext context, int id)
        {
            return context.Categories.FirstOrDefault(c => c.Id == id && !c.IsDelete);
        }

    }
}
