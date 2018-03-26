
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RecruitmentSystem.Recruitment.Data;
namespace UnitOfWorkExtension
{
    public static partial class Extensions<TEntity>
        where TEntity : class

    {

        public static async Task<int> AddAsync(TEntity entity)
        {
            using (var context = new TalentAcquisitionEntities())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                dbSet.Add(entity);
                context.Entry(entity).State = EntityState.Added;
                return await context.SaveChangesAsync();
            }

        }
        public static async Task<int> UpdateAsync(TEntity entity)
        {
            using (var context = new TalentAcquisitionEntities())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }

        }
        public static TEntity Find(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            using (var context = new TalentAcquisitionEntities())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                if (includeProperties != "")
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        query = query.Include(includeProperty);
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return query.FirstOrDefault();
            }
        }
        public static async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            using (var context = new TalentAcquisitionEntities())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                if (includeProperties != "")
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        query = query.Include(includeProperty);
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return await query.FirstOrDefaultAsync();
            }
        }
        public static IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {

            DbSet<TEntity> dbSet = new TalentAcquisitionEntities().Set<TEntity>();
            IQueryable<TEntity> query = dbSet;
            if (includeProperties != "")
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            if (filter != null)
                query = query.Where(filter);

            return query;

        }
        public static async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TalentAcquisitionEntities())
            {
                var res = await FindAsync(filter: filter);
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                dbSet.Attach(res);
                dbSet.Remove(res);
                return await context.SaveChangesAsync();
            }
        }
        public static IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TEntity>> selector = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            using (var context = new TalentAcquisitionEntities())
            {
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                if (includeProperties != "")
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        query = query.Include(includeProperty);

                return query.ToList();
            }
        }

    }
    public partial class GenericRepository<TEntity>
       where TEntity : class

    {
        private TalentAcquisitionEntities context;

        public GenericRepository()
        {

        }
        public GenericRepository(TalentAcquisitionEntities context)
        {
            this.context = context;
        }
        public async Task<int> AddAsync(TEntity entity)
        {
           
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                dbSet.Add(entity);
                context.Entry(entity).State = EntityState.Added;
                return await context.SaveChangesAsync();
           

        }
        public async Task<int> UpdateAsync(TEntity entity, bool updated = false)
        {
         
                if (updated)
                    return await context.SaveChangesAsync();

                DbSet<TEntity> dbSet = context.Set<TEntity>();
                dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                return await context.SaveChangesAsync();
           

        }
        public TEntity Find(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                if (includeProperties != "")
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        query = query.Include(includeProperty);
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return query.FirstOrDefault();
        }
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                if (includeProperties != "")
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        query = query.Include(includeProperty);
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                return await query.FirstOrDefaultAsync();

        }
        public IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {

            DbSet<TEntity> dbSet = new TalentAcquisitionEntities().Set<TEntity>();
            IQueryable<TEntity> query = dbSet;
            if (includeProperties != "")
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);
            if (filter != null)
                query = query.Where(filter);

            return query;

        }
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            
                var res = await FindAsync(filter: filter);
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                dbSet.Attach(res);
                dbSet.Remove(res);
                return await context.SaveChangesAsync();
        }
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, TEntity>> selector = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
           
                DbSet<TEntity> dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                if (includeProperties != "")
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        query = query.Include(includeProperty);

                return query.ToList();
           
        }

    }
    public static class LinqEntension
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> enumerable)
        {

            return enumerable.OrderBy(m => Guid.NewGuid());
        }
        public static void Add<T>(this T entity)
        {

        }

    }

}
