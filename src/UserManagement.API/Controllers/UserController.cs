using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagement.API.Responses;
using UserManagement.Application.Contracts;
using UserManagement.Application.DTOs.User;
using UserManagement.Application.Notifications;

namespace UserManagement.API.Controllers;

[Authorize]
[Route("user")]
public class UserController : MainController
{
    private readonly IUserService _userService;

    public UserController(INotificator notificator, IUserService userService) : base(notificator)
    {
        _userService = userService;
    }
    
    [HttpPost("create")]
    [SwaggerOperation(Summary = "Create a user")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var createUser = await _userService.Create(dto);
        return CreatedResponse("", createUser);
    }

    [HttpPut("update/{id}")]
    [SwaggerOperation(Summary = "Update a user")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var updateUser = await _userService.Update(id, dto);
        return OkResponse(updateUser);
    }

    [HttpDelete("delete/{id}")]
    [SwaggerOperation(Summary = "Delete a user")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return NoContentResponse();
    }
    
    [HttpGet("get-by-id/{id}")]
    [SwaggerOperation(Summary = "Get by ID a user")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var getUser = await _userService.GetById(id);
        return OkResponse(getUser);
    }
    
    [HttpGet("get-by-email/{email}")]
    [SwaggerOperation(Summary = "Get by email a user")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var getUser = await _userService.GetByEmail(email);
        return OkResponse(getUser);
    }

    [HttpGet("get-all")]
    [SwaggerOperation(Summary = "Get all users")]
    [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var getUserList = await _userService.GetAll();
        return OkResponse(getUserList);
    }
}