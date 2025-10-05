namespace Backend_BeautyPagrant.Dto;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public RefreshTokenDto? RefreshToken { get; set; }
}
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsActive { get; set; }

}
public class RefreshTokenDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = null!;
}
public class LoginDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
public class RegisterDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "User";
}
