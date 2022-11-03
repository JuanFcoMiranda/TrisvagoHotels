using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TrisvagoHotels.DataContext.Context;
using TrisvagoHotels.DataContracts.IRepository;

namespace TrisvagoHotels.Repositories.EFRepository;

public sealed class Repository<T> : IRepository<T> where T : class
{
    public Repository(MyDataContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DBSet = Context.Set<T>();
    }

    private DbSet<T> DBSet { get; set; }

    private DbContext Context { get; set; }

    public async ValueTask<T> GetById(int id) => await Context.Set<T>().FindAsync(id);

    public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate) => await DBSet.FirstOrDefaultAsync(predicate);

    public async Task<ICollection<T>> GetAll(Expression<Func<T, bool>> filter = null,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                            string includeProperties = "")
    {
        var query = DBSet.AsQueryable();
        if (filter != null)
        {
            query = query.Where(filter);
        }
        foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
    }

    public async Task<int> CountAll() => await DBSet.CountAsync();

    public async Task<int> CountWhere(Expression<Func<T, bool>> predicate) =>
        await DBSet.CountAsync(predicate);

    public async Task<IReadOnlyCollection<T>> GetAllByCondition(Expression<Func<T, bool>> expression) =>
        await DBSet.Where(expression).AsNoTracking().ToListAsync();

    public async Task Add(T entity)
    {
        EntityEntry dbEntity = Context.Entry(entity);
        if (dbEntity.State != EntityState.Detached)
            dbEntity.State = EntityState.Added;
        else
        {
            await DBSet.AddAsync(entity);
        }
    }

    public async Task Add(ICollection<T> entities)
    {
        foreach (var obj in entities)
        {
            EntityEntry dbEntity = Context.Entry(obj);
            if (dbEntity.State != EntityState.Detached)
                dbEntity.State = EntityState.Added;
        }
        await DBSet.AddRangeAsync(entities);
    }

    public async Task Update(T entity)
    {
        EntityEntry dbEntity = Context.Entry(entity);
        if (dbEntity.State == EntityState.Detached)
            DBSet.Attach(entity);
        dbEntity.State = EntityState.Modified;
        await Task.Run(() => {
            DBSet.Update(entity);
        });
    }

    public async Task Delete(T entity)
    {
        EntityEntry dbEntity = Context.Entry(entity);
        if (dbEntity.State != EntityState.Deleted)
            dbEntity.State = EntityState.Deleted;
        else
        {
            await Task.Run(() => {
                DBSet.Attach(entity);
                DBSet.Remove(entity);
            });
        }
    }

    public async Task Delete(int id)
    {
        T entity = await GetById(id);
        if (entity == null)
            return;
        await Delete(entity);
    }

    public async Task Delete(ICollection<T> entities)
    {
        await Task.Run(() => {
            //foreach (var obj in entities)
            //{
            //    var dbEntity = Context.Entry(obj);
            //    if (dbEntity.State != EntityState.Deleted)
            //    {
            //        dbEntity.State = EntityState.Deleted;
            //    }
            //    else
            //    {
            //        DBSet.Attach(obj);
            //    }
            //}

            DBSet.RemoveRange(entities);
        });
    }
}