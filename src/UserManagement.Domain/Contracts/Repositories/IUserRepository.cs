using UserManagement.Domain.Models;

namespace UserManagement.Domain.Contracts.Repositories;

public interface IUserRepository : IEntityRepository<User>
{
    void Create(User user);
    void Update(User user);
    void Delete(User user);
    Task<User?> GetById(int id);
    Task<User?> GetByEmail(string email);
    Task<List<User>> GetAll(); 
}