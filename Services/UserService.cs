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

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        private async Task ValidateUniqueEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

            if (user is not null)
            {
                throw new ConflictException("Email ja vinculado a um usuário ativo");
            }
        }

        public async Task<List<UserResponseDto>> FindAllUsers()
        {
            return await _context.Users
            .Select(user => new UserResponseDto(
                user.Id,
                user.Name,
                user.Email,
                user.PhoneNumber
            ))
            .ToListAsync();
        }

        public async Task<UserResponseDto> CreateUser(UserRequestDto dto)
        {
            await ValidateUniqueEmail(dto.Email);

            UserModel user = new UserModel
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
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
    }
}
