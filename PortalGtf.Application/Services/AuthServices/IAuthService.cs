using PortalGtf.Application.ViewModels.LoginVM;

namespace PortalGtf.Application.Services.AuthServices;

public interface IAuthService
{
    string GenerateJwtToken(int userId, string email, string role);
    string ComputeSha256Hash(string password);
    Task<LoginResponseViewModel?> LoginAsync(LoginRequestViewModel model);
}