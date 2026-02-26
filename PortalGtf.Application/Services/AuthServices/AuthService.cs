using System.IdentityModel.Tokens.Jwt;
using PortalGtf.Application.ViewModels.LoginVM;
using PortalGtf.Core.Enums;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace PortalGtf.Application.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUsuarioRepository _userRepository;

    public AuthService(IConfiguration configuration, IUsuarioRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }
    public async Task<LoginResponseViewModel?> LoginAsync(LoginRequestViewModel model)
    {
        var passwordHash = ComputeSha256Hash(model.Senha);

        var user = await _userRepository.GetByEmailAndPasswordAsync(
            model.Email,
            passwordHash
        );

        if (user == null)
            return null;

        if (user.StatusUsuario != StatusUsuario.Ativo)
            throw new Exception("Usuário não está ativo");

        // Atualiza último acesso (se existir essa coluna futuramente)
        await _userRepository.UpdateAsync(user);

        var token = GenerateJwtToken(
            user.Id,
            user.Email,
            user.Funcao.TipoFuncao 
        );

        return new LoginResponseViewModel
        {
            UsuarioId = user.Id,
            Email = user.Email,
            Token = token,
            Funcao = user.Funcao.TipoFuncao
        };
    }
    public string GenerateJwtToken(int userId, string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = _configuration["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        );

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256
        );

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(120),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string ComputeSha256Hash(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(
            Encoding.UTF8.GetBytes(password)
        );

        var builder = new StringBuilder();
        foreach (var b in bytes)
            builder.Append(b.ToString("x2"));

        return builder.ToString();
    }
}