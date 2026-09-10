using Microsoft.AspNetCore.Mvc;
using NotebookApi.Dtos;
using NotebookApi.Exceptions;
using NotebookApi.Services;

namespace NotebookApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
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
