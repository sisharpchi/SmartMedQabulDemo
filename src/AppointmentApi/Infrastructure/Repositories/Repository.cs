using Microsoft.EntityFrameworkCore;

namespace AppointmentApi.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _db;
    public Repository(AppDbContext db) => _db = db;

    public async Task<T?> GetByIdAsync(long id) => await _db.Set<T>().FindAsync(id);

    public async Task<List<T>> ListAsync() => await _db.Set<T>().ToListAsync();

    public async Task AddAsync(T entity)
    {
        await _db.Set<T>().AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _db.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _db.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
}
