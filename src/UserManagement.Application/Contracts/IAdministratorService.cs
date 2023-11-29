using UserManagement.Application.DTOs.Administrator;

namespace UserManagement.Application.Contracts;

public interface IAdministratorService
{
    AdministratorTokenDto? Login(AdministratorLoginDto dto);
}