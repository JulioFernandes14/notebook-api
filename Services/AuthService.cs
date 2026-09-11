using Microsoft.EntityFrameworkCore;
using NotebookApi.Data;
using NotebookApi.Dtos;
using NotebookApi.Exceptions;
using NotebookApi.Models;

namespace NotebookApi.Services
{
    public class AuthService
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public AuthService(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            var user = await _userService.FindUserByEmail(dto.Email);

            if (user is null)
            {
                throw new BadRequestException("Email ou senha inválido(s)");
            }

            var compareHash = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!compareHash)
            {
                throw new BadRequestException("Email ou senha inválido(s)");
            }

            var userDto = new UserResponseDto(user.Id, user.Name, user.Email, user.PhoneNumber);
            var accessToken = _jwtService.GenerateToken(user);

            return new LoginResponseDto(userDto, accessToken);

        }

        public async Task<UserResponseDto> Register(UserRequestDto dto) 
        {
            return await _userService.CreateUser(new UserRequestDto
            (
                dto.Name,
                dto.Email,
                dto.PhoneNumber,
                BCrypt.Net.BCrypt.HashPassword(dto.Password)
            ));
        }
    }
}
