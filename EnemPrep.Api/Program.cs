using Microsoft.EntityFrameworkCore;
using EnemPrep.Application;
using EnemPrep.Infrastructure;
using EnemPrep.Infrastructure.Persistence;
using EnemPrep.Infrastructure.Persistence.Seed;
using EnemPrep.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<EnemPrepDbContext>();
    await context.Database.MigrateAsync();
    await DatabaseSeeder.SeedAsync(context);
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
