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

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<EnemPrepDbContext>();
    await context.Database.MigrateAsync();
    await DatabaseSeeder.SeedAsync(context);
}

// HTTPS obrigatório em todos os ambientes
app.UseHttpsRedirection();

app.UseStaticFiles();

// Rate Limiter (antes do roteamento)
app.UseRateLimiter();

// CORS com política restrita
app.UseCors("AllowWeb");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
