namespace Campus.Repository.Helpers;

using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{

    protected readonly DataContext _context = null;
    protected readonly DbSet<T> table = null;

    protected GenericRepository(DataContext context)
    {
        _context = context;
        table = _context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync() => await table.ToListAsync();

    public async Task<T> GetByIdAsync(object id) => await table.FindAsync(id);

    public T GetById(object id) => table.Find(id);

    public async Task InsertAsync(T entity) => await table.AddAsync(entity);

    public async Task Update(T entity, object id)
    {
        var obj = await table.FindAsync(id);

        _context.Entry(obj).CurrentValues.SetValues(entity);
    }
    
    public void Delete(object id)
    {
        T existing = table.Find(id);
        table.Remove(existing);
    }

}