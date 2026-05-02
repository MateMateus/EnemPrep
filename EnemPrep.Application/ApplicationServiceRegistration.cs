using Microsoft.Extensions.DependencyInjection;
using EnemPrep.Application.Interfaces;
using EnemPrep.Application.UseCases;

namespace EnemPrep.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPerfilService, PerfilService>();
        services.AddScoped<IMateriaService, MateriaService>();
        services.AddScoped<IAssuntoService, AssuntoService>();
        services.AddScoped<IQuestaoService, QuestaoService>();
        services.AddScoped<IVideoAulaService, VideoAulaService>();
        services.AddScoped<IPlanoEstudoService, PlanoEstudoService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IGamificacaoService, GamificacaoService>();
        services.AddScoped<ISimuladoService, SimuladoService>();
        services.AddScoped<ILivroService, LivroService>();

        return services;
    }
}
