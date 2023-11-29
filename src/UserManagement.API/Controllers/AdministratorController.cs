using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UserManagement.API.Responses;
using UserManagement.Application.Contracts;
using UserManagement.Application.DTOs.Administrator;
using UserManagement.Application.Notifications;

namespace UserManagement.API.Controllers;

[Route("administrator")]
public class AdministratorController : MainController
{
    private readonly IAdministratorService _administratorService;

    public AdministratorController(
        INotificator notificator, 
        IAdministratorService administratorService) 
        : base(notificator)
    {
        _administratorService = administratorService;
    }
    
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Administrator login")]
    [ProducesResponseType(typeof(AdministratorTokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Login([FromBody] AdministratorLoginDto dto)
    {
        var token = _administratorService.Login(dto);
        return OkResponse(token);
    }
}