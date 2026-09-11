using Microsoft.EntityFrameworkCore;
using NotebookApi.Data;
using NotebookApi.Dtos;
using NotebookApi.Exceptions;
using NotebookApi.Models;

namespace NotebookApi.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public UserService(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        private async Task ValidateUniqueEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            if (user is not null)
            {
                throw new ConflictException("Email ja vinculado a um usuário ativo");
            }
        }

        public async Task<UserModel?> FindUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<UserResponseDto> FindUserById(int userId)
        {
            var user = await _context.Users
            .Select(user => new UserResponseDto(
                user.Id,
                user.Name,
                user.Email,
                user.PhoneNumber
            ))
            .FirstOrDefaultAsync(user => user.Id == userId) ?? throw new NotFoundException("Usuário não encontrado");
            return user;
        }

        public async Task<UserModel> GetUserModelById(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId) ?? throw new NotFoundException("Usuário não encontrado");

            return user;
        }

        public async Task<UserResponseDto> CreateUser(UserRequestDto dto)
        {
            await ValidateUniqueEmail(dto.Email);

            UserModel user = new UserModel
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserResponseDto(
                user.Id,
                user.Name,
                user.Email,
                user.PhoneNumber
             );
            
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            var user = await FindUserByEmail(dto.Email);

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
    }
}
