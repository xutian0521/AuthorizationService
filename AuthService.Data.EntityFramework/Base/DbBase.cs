using AuthService.Data.EntityFramework.Context;
using AuthService.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Data.EntityFramework.Base
{
    public class DbBase<T> where T: EntityBase
    {
        //todo:写EF 常用怎删改查方法。EF实体仓储DbSet实例

        protected DbContext db;
        public DbBase(EFContext dbContext)
        {
            this.db = dbContext;
        }

        public async Task AddAsync(T model)
        {
            await this.db.AddAsync(model);
        }

        public void Add(T model)
        {
            db.Set<T>().Add(model);
        }

        public void Add(List<T> models)
        {
            foreach (var item in models)
            {
                db.Set<T>().Add(item);
            }
        }

        public void Modify<TProperty>(T model, Expression<Func<T, TProperty>> property)
        {
            var entry = db.Entry(model);
            entry.Property(property).IsModified = true;

        }
        public void Modifies(T model)
        {
            var enrty = db.Entry(model);
            db.Set<T>().Attach(model);
            enrty.State = EntityState.Unchanged;
            enrty.State = EntityState.Modified;

        }
        public void Delete(T model)
        {
            db.Set<T>().Attach(model);
            db.Set<T>().Remove(model);
        }

        public async Task DeleteByAsync(Expression<Func<T, bool>> predicate)
        {
            var listDeleting = await db.Set<T>().Where(predicate).ToListAsync();
            listDeleting.ForEach(t =>
            {
                db.Set<T>().Remove(t);
            });
        }


        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            var model = await db.Set<T>().AsNoTracking().Where(predicate).FirstOrDefaultAsync();
            return model;
        }

        public async Task<List<T>> GetListByAsync(Expression<Func<T, bool>> predicate)
        {
            var listModel = await db.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
            return listModel;
        }
        public async Task<T> GetModelByIdAsync(long id)
        {
            var model= await db.Set<T>().Where(t => t.IsDeleted == false && t.Id == id).FirstOrDefaultAsync();
            return model;
        }
        /// <summary>
        /// 返回AsNoTracking where 的IQueryable
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().AsNoTracking().Where(predicate);
        }
        public IQueryable<T> Query()
        {
            return db.Set<T>().AsQueryable();
        }

        public void Attch(T model)
        {
            db.Set<T>().Attach(model);
        }
        public async Task<int> SaveChangesAsync()
        {
            var row = await db.SaveChangesAsync();
            return row;
        }
    }
}
