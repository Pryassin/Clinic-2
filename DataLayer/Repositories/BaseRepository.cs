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
            return context.SaveChanges();
        }
        public bool Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            return context.SaveChanges() > 0;

        }

        public bool DoesExist(int id)
        {
           var entity= context.Set<T>().Find(id);
            return entity != null;
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