namespace Backend_BeautyPagrant.Models
{
    public static class AuditExtensions
    {
        public static T WithCreateAudit<T>(this T entity, string userName) where T : class
        {
            DateTime now = DateTime.Now;
            Type type = entity.GetType();

            type.GetProperty("CreateBy")?.SetValue(entity, userName);
            type.GetProperty("CreateDate")?.SetValue(entity, now);
            type.GetProperty("UpdateBy")?.SetValue(entity, userName);
            type.GetProperty("UpdateDate")?.SetValue(entity, now);
            type.GetProperty("IsDelete")?.SetValue(entity, false);

            return entity;
        }

        public static T WithUpdateAudit<T>(this T entity, string userName) where T : class
        {
            DateTime now = DateTime.Now;
            Type type = entity.GetType();

            type.GetProperty("UpdateBy")?.SetValue(entity, userName);
            type.GetProperty("UpdateDate")?.SetValue(entity, now);

            return entity;
        }
        public static T WithDeleteAudit<T>(this T entity, string userName) where T : class
        {
            DateTime now = DateTime.Now;
            Type type = entity.GetType();

            type.GetProperty("UpdateBy")?.SetValue(entity, userName);
            type.GetProperty("UpdateDate")?.SetValue(entity, now);
            type.GetProperty("IsDelete")?.SetValue(entity, true);

            return entity;
        }
    }
}
