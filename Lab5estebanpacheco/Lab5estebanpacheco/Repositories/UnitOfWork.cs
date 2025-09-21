using System.Collections;
using Lab5estebanpacheco.Data;
using Lab5estebanpacheco.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5estebanpacheco.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly TiendaDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(TiendaDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[type];
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}