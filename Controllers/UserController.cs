using Microsoft.AspNetCore.Mvc;
using NotebookApi.Dtos;
using NotebookApi.Exceptions;
using NotebookApi.Services;

namespace NotebookApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> FindAll()
    {
        var users = await _userService.FindAllUsers();

        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> Create(
        [FromBody] UserRequestDto dto)
    {
        try
        {
            var user = await _userService.CreateUser(dto);

            return Created("", user);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message);
        }
    }
}