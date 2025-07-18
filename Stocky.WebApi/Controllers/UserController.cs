using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stocky.Shared.Models;
using Stocky.WebApi.Application.UserData.Commands.CreateUser;
using Stocky.WebApi.Application.UserData.Commands.DeleteUser;
using Stocky.WebApi.Application.UserData.Commands.UpdateUser;
using Stocky.WebApi.Application.UserData.Queries.GetUser;
using Stocky.WebApi.Application.UserData.Queries.GetUserById;
using Stocky.WebApi.Application.UserData.Queries.UsersByPagination;
using Stocky.WebApi.Application.UserData.Queries.GetUserBySearch;

namespace Stocky.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]/[action]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand? user)
    {
        if (string.IsNullOrEmpty(user.Name))
        {
            return BadRequest("User cannot be null");
        }

        return Ok(await mediator.Send(user));
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        return Ok(await mediator.Send(new GetAllUserQuery()));
    }

    [HttpGet]
    public async Task<IActionResult> GetUserById([FromQuery] UserByIdQuery user)
    {
        return Ok(await mediator.Send(user));
    }

    [HttpGet]
    public async Task<ActionResult<PageData<User>>> UserPagination(
        [FromQuery] UsersByPaginationQuery usersByPaginationQuery)
    {
        var paginatedList = await mediator.Send(usersByPaginationQuery);

        return Ok(paginatedList);
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] SearchUserQuery userQuery)
    {
        return Ok(await mediator.Send(userQuery));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand user)
    {
        return Ok(await mediator.Send(user));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand user)
    {
        return Ok(await mediator.Send(user));
    }
}