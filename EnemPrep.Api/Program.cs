using Microsoft.EntityFrameworkCore;
using EnemPrep.Application;
using EnemPrep.Infrastructure;
using EnemPrep.Infrastructure.Persistence;
using EnemPrep.Infrastructure.Persistence.Seed;
using EnemPrep.Api.Extensions;
using EnemPrep.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration); // agora recebe IConfiguration

var app = builder.Build();

// Security Headers em todas as respostas (antes de qualquer outro middleware)
app.UseSecurityHeaders();

app.UseExceptionHandler();

// Executa as migrations do banco de dados em todos os ambientes (Dev e Produção)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EnemPrepDbContext>();
    try
    {
        Console.WriteLine("=== MIGRATION START ===");
        Console.WriteLine($"Connection: {context.Database.GetConnectionString()}");
        
        var pending = await context.Database.GetPendingMigrationsAsync();
        Console.WriteLine($"Pending migrations: {string.Join(", ", pending)}");
        
        var applied = await context.Database.GetAppliedMigrationsAsync();
        Console.WriteLine($"Applied migrations: {string.Join(", ", applied)}");
        
        await context.Database.MigrateAsync();
        Console.WriteLine("=== MIGRATION SUCCESS ===");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"=== MIGRATION ERROR ===");
        Console.WriteLine($"Message: {ex.Message}");
        Console.WriteLine($"Inner: {ex.InnerException?.Message}");
        Console.WriteLine($"StackTrace: {ex.StackTrace}");
        throw; // re-throw para a API não arrancar com DB incompleto
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    // Executa os dados iniciais do seed apenas em desenvolvimento
    using var devScope = app.Services.CreateScope();
    var devContext = devScope.ServiceProvider.GetRequiredService<EnemPrepDbContext>();
    await DatabaseSeeder.SeedAsync(devContext);
}

// Configuração para Proxy Reverso (EasyPanel/Traefik)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | 
                       Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(wwwrootPath),
    RequestPath = ""
});

// Rate Limiter (antes do roteamento)
app.UseRateLimiter();

// CORS com política restrita
app.UseCors("AllowWeb");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
