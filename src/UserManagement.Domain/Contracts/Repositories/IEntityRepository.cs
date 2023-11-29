using UserManagement.Domain.Models;

namespace UserManagement.Domain.Contracts.Repositories;

public interface IEntityRepository<T> : IDisposable where T : Entity 
{
    IUnitOfWork UnitOfWork { get; }
}