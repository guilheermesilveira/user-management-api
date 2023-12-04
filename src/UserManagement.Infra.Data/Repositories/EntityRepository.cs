using UserManagement.Domain.Contracts;
using UserManagement.Domain.Contracts.Repositories;
using UserManagement.Domain.Models;
using UserManagement.Infra.Data.Context;

namespace UserManagement.Infra.Data.Repositories;

public abstract class EntityRepository<T> : IEntityRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext Context;
    private bool _isDisposed;

    protected EntityRepository(ApplicationDbContext context)
    {
        Context = context;
    }
    
    public IUnitOfWork UnitOfWork => Context;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) 
            return;

        if (disposing) 
            Context.Dispose();

        _isDisposed = true;
    }
    
    ~EntityRepository()
    {
        Dispose(false);
    }
}