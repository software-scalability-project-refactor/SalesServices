using Microsoft.EntityFrameworkCore;
using SalesService.Entities;

namespace SalesService.Repositories;

public class SalesRepository : IRepository<Sale>
{
    private readonly SalesDbContext _context;

    public SalesRepository(SalesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales.ToListAsync();
    }

    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        return await _context.Sales.FindAsync(id);
    }

    public async Task<Sale> AddAsync(Sale entity)
    {
        _context.Sales.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Sale entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Sales.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.Sales.AnyAsync(e => e.IdSale == id);
    }
}