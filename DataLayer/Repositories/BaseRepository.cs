using DataLayer.Data;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private ClinicDbContext context;

        public BaseRepository(ClinicDbContext context)
        {
            this.context = context;
        }
        public int Add(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
            var prop = entity.GetType().GetProperties()
                         .FirstOrDefault(p => p.Name.EndsWith("ID", StringComparison.OrdinalIgnoreCase)
                         && p.PropertyType == typeof(int));

            if (prop != null)
            {
                return (int)(prop.GetValue(entity) ?? 0);
                //this will return the ID of the added Entity
            }
            //otherwise return 0
            return 0;
        }
        public bool Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            return context.SaveChanges() > 0;

        }
        public T GetByID(int id)
        {
            return context.Set<T>().Find(id);
        }
        public bool Update(T entity)
        {
            context.Set<T>().Update(entity);
            return context.SaveChanges() > 0;
        }
    }
}