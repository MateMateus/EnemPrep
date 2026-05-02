using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Auth;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Enums;
using EnemPrep.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EnemPrep.Application.UseCases;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            return Result<AuthResponse>.Fail("Email ou senha inválidos.");

        var role = usuario.PerfilUsuario?.Tipo.ToString() ?? TipoPerfil.Aluno.ToString();
        var token = GenerateJwtToken(usuario, role);

        var response = new AuthResponse(
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            role,
            Token: token);

        return Result<AuthResponse>.Ok(response);
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        if (request.Senha != request.ConfirmacaoSenha)
            return Result<AuthResponse>.Fail("Senha e confirmação não conferem.");

        if (await _usuarioRepository.ExistsAsync(request.Email, cancellationToken))
            return Result<AuthResponse>.Fail("Email já cadastrado.");

        var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);

        var perfilAluno = await _usuarioRepository.GetPerfilByTipoAsync(TipoPerfil.Aluno, cancellationToken);
        if (perfilAluno is null)
            return Result<AuthResponse>.Fail("Perfil padrão não configurado no sistema.");

        var usuario = new Usuario(request.Nome, request.Email, senhaHash, perfilAluno.Id);

        await _usuarioRepository.AddAsync(usuario, cancellationToken);

        var token = GenerateJwtToken(usuario, TipoPerfil.Aluno.ToString());

        var response = new AuthResponse(
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            TipoPerfil.Aluno.ToString(),
            Token: token);

        return Result<AuthResponse>.Ok(response);
    }

    private string GenerateJwtToken(Usuario usuario, string role)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not found.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryInMinutes"] ?? "1440")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
