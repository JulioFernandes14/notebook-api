using Microsoft.AspNetCore.Mvc;
using NotebookApi.Dtos;
using NotebookApi.Exceptions;
using NotebookApi.Services;

namespace NotebookApi.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserResponseDto>> Register(
        [FromBody] UserRequestDto dto)
        {
            try
            {
                var user = await _authService.Register(dto);

                return Created("", user);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto login)
        {
            try
            {
                var response = await _authService.Login(login);
                return Ok(response);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
