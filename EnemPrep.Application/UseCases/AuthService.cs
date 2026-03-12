using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Auth;
using EnemPrep.Application.Interfaces;
using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Enums;
using EnemPrep.Domain.Interfaces;

namespace EnemPrep.Application.UseCases;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
            return Result<AuthResponse>.Fail("Email ou senha inválidos.");

        var response = new AuthResponse(
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.PerfilUsuario?.Tipo.ToString() ?? TipoPerfil.Aluno.ToString(),
            Token: Guid.NewGuid().ToString("N"));

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

        var response = new AuthResponse(
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            TipoPerfil.Aluno.ToString(),
            Token: Guid.NewGuid().ToString("N"));

        return Result<AuthResponse>.Ok(response);
    }
}
