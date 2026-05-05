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
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(
                connectionString,
                ServerVersion.Parse("8.0.36-mysql"),
                mysqlOptions => mysqlOptions.MigrationsAssembly("EnemPrep.Infrastructure"));

            // SEGURANCA: nunca logar valores de parâmetros (senhas, emails, tokens)
            // mesmo que o nível de log esteja muito verboso
            options.EnableSensitiveDataLogging(false);

            // Erros detalhados apenas em desenvolvimento
            var isDevelopment = configuration["ASPNETCORE_ENVIRONMENT"] == "Development";
            if (isDevelopment)
            {
                options.EnableDetailedErrors(true);
            }
        });

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IMateriaRepository, MateriaRepository>();
        services.AddScoped<IAssuntoRepository, AssuntoRepository>();
        services.AddScoped<IQuestaoRepository, QuestaoRepository>();
        services.AddScoped<ITentativaQuestaoRepository, TentativaQuestaoRepository>();
        services.AddScoped<IVideoAulaRepository, VideoAulaRepository>();
        services.AddScoped<IPlanoEstudoRepository, PlanoEstudoRepository>();
        services.AddScoped<IStreakUsuarioRepository, StreakUsuarioRepository>();
        services.AddScoped<IDesafioDiarioRepository, DesafioDiarioRepository>();
        services.AddScoped<IConquistaRepository, ConquistaRepository>();
        services.AddScoped<IUsuarioConquistaRepository, UsuarioConquistaRepository>();
        services.AddScoped<ISimuladoRepository, SimuladoRepository>();
        services.AddScoped<ITentativaSimuladoRepository, TentativaSimuladoRepository>();
        services.AddScoped<ILivroRepository, LivroRepository>();

        // Storage: Supabase em produção, local em desenvolvimento
        var supabaseUrl = configuration["Supabase:Url"];
        var isSupabaseConfigured = !string.IsNullOrEmpty(supabaseUrl)
                                   && !supabaseUrl.Contains("VIA_ENV_VAR");

        if (isSupabaseConfigured)
            services.AddHttpClient<EnemPrep.Application.Interfaces.IFileStorageService, EnemPrep.Infrastructure.Services.SupabaseStorageService>();
        else
            services.AddScoped<EnemPrep.Application.Interfaces.IFileStorageService, EnemPrep.Infrastructure.Services.LocalFileStorageService>();

        services.AddScoped<EnemPrep.Application.Interfaces.IPdfProcessorService, EnemPrep.Infrastructure.Services.PdfProcessorService>();
        return services;

    }
}
