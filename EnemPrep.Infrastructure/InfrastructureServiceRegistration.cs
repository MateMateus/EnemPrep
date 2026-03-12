using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnemPrep.Domain.Interfaces;
using EnemPrep.Infrastructure.Persistence;
using EnemPrep.Infrastructure.Repositories;

namespace EnemPrep.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EnemPrepDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => sqlOptions.MigrationsAssembly(typeof(EnemPrepDbContext).Assembly.FullName)));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IMateriaRepository, MateriaRepository>();
        services.AddScoped<IAssuntoRepository, AssuntoRepository>();
        services.AddScoped<IQuestaoRepository, QuestaoRepository>();
        services.AddScoped<ITentativaQuestaoRepository, TentativaQuestaoRepository>();
        services.AddScoped<IVideoAulaRepository, VideoAulaRepository>();
        services.AddScoped<IMaterialEstudoRepository, MaterialEstudoRepository>();
        services.AddScoped<IPlanoEstudoRepository, PlanoEstudoRepository>();
        services.AddScoped<IStreakUsuarioRepository, StreakUsuarioRepository>();
        services.AddScoped<IDesafioDiarioRepository, DesafioDiarioRepository>();
        services.AddScoped<IConquistaRepository, ConquistaRepository>();
        services.AddScoped<IUsuarioConquistaRepository, UsuarioConquistaRepository>();
        services.AddScoped<ISimuladoRepository, SimuladoRepository>();
        services.AddScoped<ITentativaSimuladoRepository, TentativaSimuladoRepository>();
        return services;
    }
}
