using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend_BeautyPagrant.Services
{
    public class AuthService
    {
        private readonly BeautyPagrantContext _context;
        private readonly IConfiguration _config;
        private const int TokenExpireMinutes = 60;
        private const int RefreshTokenExpireDays = 7;

        public AuthService(BeautyPagrantContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto, string createdBy)
        {
            bool exists = await _context.userrs.AnyAsync(u => u.Username == dto.Username && !u.IsDelete);
            if (exists)
            {
                throw new Exception("Username already exists.");
            }

            userr newUser = new userr
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                IsActive = true
            }.WithCreateAudit(createdBy);

            _context.userrs.Add(newUser);
            await _context.SaveChangesAsync();

            UserDto userDto = new UserDto
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Role = newUser.Role,
                IsActive = newUser.IsActive
            };

            return userDto;
        }

        public async Task<(string Token, UserDto User)> LoginAsync(LoginDto dto)
        {
            userr? user = await _context.userrs
                .FirstOrDefaultAsync(u => u.Username == dto.Username && !u.IsDelete && u.IsActive);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string token = GenerateToken(user.Username, user.Role);

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                IsActive = user.IsActive
            };

            return (token, userDto);
        }

        public string GenerateToken(string username, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(TokenExpireMinutes),
                signingCredentials: creds
            );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public (DateTime Expiration, int RemainingMinutes, IEnumerable<Claim> Claims) CheckToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                throw new Exception("Invalid token format");
            }

            JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

            DateTime expiration = jwtToken.ValidTo;
            int remainingMinutes = (int)(expiration - DateTime.UtcNow).TotalMinutes;

            return (expiration, remainingMinutes < 0 ? 0 : remainingMinutes, jwtToken.Claims);
        }
        public async Task<(string AccessToken, string RefreshToken, UserDto User)> LoginWithRefreshAsync(LoginDto dto)
        {
            userr? user = await _context.userrs
                .FirstOrDefaultAsync(u => u.Username == dto.Username && !u.IsDelete && u.IsActive);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            string accessToken = GenerateToken(user.Username, user.Role);
            string refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            RefreshToken entity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenExpireDays)
            };

            _context.Set<RefreshToken>().Add(entity);
            await _context.SaveChangesAsync();

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                IsActive = user.IsActive
            };

            return (accessToken, refreshToken, userDto);
        }

        public async Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            RefreshToken? tokenEntity = await _context.Set<RefreshToken>()
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == refreshToken && !r.IsRevoked);

            if (tokenEntity == null || tokenEntity.ExpiresAt < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token");
            }

            return GenerateToken(tokenEntity.User.Username, tokenEntity.User.Role);
        }
    }
}
