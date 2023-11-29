using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManagement.Application.Contracts;
using UserManagement.Application.DTOs.User;
using UserManagement.Application.Notifications;
using UserManagement.Domain.Contracts.Repositories;
using UserManagement.Domain.Models;

namespace UserManagement.Application.Services;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IMapper mapper,
        INotificator notificator,
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher)
        : base(mapper, notificator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto?> Create(CreateUserDto dto)
    {
        var user = Mapper.Map<User>(dto);
        if (!await Validate(user))
        {
            return null;
        }

        user.Password = _passwordHasher.HashPassword(user, dto.Password);

        _userRepository.Create(user);

        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("Não foi possível cadastrar o usuário.");
        return null;
    }

    public async Task<UserDto?> Update(int id, UpdateUserDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("Os IDs não conferem.");
            return null;
        }
        
        var getUser = await _userRepository.GetById(dto.Id);
        if (getUser == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }
        
        var user = Mapper.Map<User>(dto);
        if (!await Validate(user))
        {
            return null;
        }
        
        user.Password = _passwordHasher.HashPassword(user, dto.Password);

        _userRepository.Update(user);

        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("Não foi possível atualizar o usuário.");
        return null;
    }

    public async Task Delete(int id)
    {
        var getUser = await _userRepository.GetById(id);
        if (getUser == null)
        {
            Notificator.HandleNotFoundResource();
            return;
        }

        _userRepository.Delete(getUser);

        if (!await _userRepository.UnitOfWork.Commit())
        {
            Notificator.Handle("Não foi possível deletar o usuário.");
        }
    }

    public async Task<UserDto?> GetById(int id)
    {
        var getUser = await _userRepository.GetById(id);
        if (getUser != null)
        {
            return Mapper.Map<UserDto>(getUser);
        }

        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<UserDto?> GetByEmail(string email)
    {
        var getUser = await _userRepository.GetByEmail(email);
        if (getUser != null)
        {
            return Mapper.Map<UserDto>(getUser);
        }

        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var getUserList = await _userRepository.GetAll();

        return Mapper.Map<List<UserDto>>(getUserList);
    }

    private async Task<bool> Validate(User user)
    {
        if (!user.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        var getUser = await _userRepository.GetByEmail(user.Email);
        if (getUser != null)
        {
            Notificator.Handle("Já existe um usuário cadastrado com o email informado.");
        }

        return !Notificator.HasNotification;
    }
}