using UserManagement.Application.DTOs.User;

namespace UserManagement.Application.Contracts;

public interface IUserService
{
    Task<UserDto?> Create(CreateUserDto dto);
    Task<UserDto?> Update(int id, UpdateUserDto dto);
    Task Delete(int id);
    Task<UserDto?> GetById(int id);
    Task<UserDto?> GetByEmail(string email);
    Task<List<UserDto>> GetAll();
}