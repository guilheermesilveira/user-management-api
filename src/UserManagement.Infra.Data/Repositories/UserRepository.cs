using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Contracts.Repositories;
using UserManagement.Domain.Models;
using UserManagement.Infra.Data.Context;

namespace UserManagement.Infra.Data.Repositories;

public class UserRepository : EntityRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    { }
    
    public void Create(User user)
    {
        Context.Users.Add(user);
    }

    public void Update(User user)
    { 
        Context.Users.Update(user);
    }

    public void Delete(User user)
    {
        Context.Users.Remove(user);
    }

    public async Task<User?> GetById(int id)
    {
        return await Context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<User?> GetByEmail(string email)
    {
        return await Context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<List<User>> GetAll()
    {
        return await Context.Users.AsNoTracking().ToListAsync();
    }
}