using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EnemPrep.Infrastructure.Persistence;
using EnemPrep.Domain.Entities;

Console.WriteLine("Iniciando Test...");

var optionsBuilder = new DbContextOptionsBuilder<EnemPrepDbContext>();
optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EnemPrepDb_Dev;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

using var context = new EnemPrepDbContext(optionsBuilder.Options);

var assunto = await context.Assuntos.FirstOrDefaultAsync();
if (assunto == null) {
    Console.WriteLine("Nenhum assunto encontrado.");
    return;
}
Console.WriteLine($"Usando AssuntoId: {assunto.Id} ({assunto.Nome})");

var videoAula = new VideoAula("Aula Teste Debug", "http://youtube.com/watch?v=debug", 300, assunto.Id);
Console.WriteLine($"Criando VideoAula com Id: {videoAula.Id}");

await context.VideoAulas.AddAsync(videoAula);
try {
    var result = await context.SaveChangesAsync();
    Console.WriteLine($"SaveChangesAsync retornou: {result}");
} catch (Exception ex) {
    Console.WriteLine($"Exception ao salvar: {ex}");
}

// Verifica se salvou
var salva = await context.VideoAulas.FirstOrDefaultAsync(v => v.Id == videoAula.Id);
if (salva != null) {
    Console.WriteLine($"Sucesso! Encontrada no DB: {salva.Titulo}");
} else {
    Console.WriteLine("FALHA: Não encontrada no DB após SaveChangesAsync!");
}
