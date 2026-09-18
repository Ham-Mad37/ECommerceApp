using ECommerceApp.Api.DTOs.Users;
namespace ECommerceApp.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponceDto?> RegisterAsync(RegisterDto dto);
        Task<AuthResponceDto?> Login(LoginDto dto);

    }
}