namespace UserManagement.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}