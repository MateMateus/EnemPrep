using EnemPrep.Domain.Entities;
using EnemPrep.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EnemPrep.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(EnemPrepDbContext context)
    {
        var perfilAluno = await context.PerfisUsuario.FirstOrDefaultAsync(p => p.Tipo == TipoPerfil.Aluno);
        var perfilAdmin = await context.PerfisUsuario.FirstOrDefaultAsync(p => p.Tipo == TipoPerfil.Administrador);

        if (perfilAluno == null || perfilAdmin == null)
        {
            perfilAluno = new PerfilUsuario(TipoPerfil.Aluno, "Aluno");
            perfilAdmin = new PerfilUsuario(TipoPerfil.Administrador, "Administrador");
            await context.PerfisUsuario.AddRangeAsync(perfilAluno, perfilAdmin);
            await context.SaveChangesAsync();
        }

        var adminEmail = "admin@enemprep.com.br";
        if (!await context.Usuarios.AnyAsync(u => u.Email == adminEmail))
        {
            var adminSenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            var adminUser = new Usuario("Administrador do Sistema", adminEmail, adminSenhaHash, perfilAdmin.Id);
            await context.Usuarios.AddAsync(adminUser);
            await context.SaveChangesAsync();
        }

        if (await context.Materias.AnyAsync())
            return;

        var matematica = new Materia("Matemática e suas Tecnologias", "Área de Matemática do ENEM");
        var linguagens = new Materia("Linguagens, Códigos e suas Tecnologias", "Área de Linguagens do ENEM");
        var natureza = new Materia("Ciências da Natureza e suas Tecnologias", "Área de Ciências da Natureza do ENEM");
        var humanas = new Materia("Ciências Humanas e suas Tecnologias", "Área de Ciências Humanas do ENEM");

        await context.Materias.AddRangeAsync(matematica, linguagens, natureza, humanas);

        var assuntos = new List<Assunto>
        {
            new("Álgebra", "Equações, funções e expressões algébricas", matematica.Id),
            new("Geometria", "Geometria plana e espacial", matematica.Id),
            new("Estatística e Probabilidade", "Análise de dados, média, moda e probabilidade", matematica.Id),

            new("Interpretação de Texto", "Compreensão e interpretação textual", linguagens.Id),
            new("Gramática", "Norma culta, concordância, regência", linguagens.Id),
            new("Literatura", "Escolas literárias, obras e autores", linguagens.Id),

            new("Física", "Mecânica, termodinâmica, óptica e ondas", natureza.Id),
            new("Química", "Química orgânica, inorgânica e físico-química", natureza.Id),
            new("Biologia", "Ecologia, genética, citologia e fisiologia", natureza.Id),

            new("História", "História do Brasil e história geral", humanas.Id),
            new("Geografia", "Geografia física, humana e geopolítica", humanas.Id),
            new("Filosofia e Sociologia", "Pensadores, correntes e sociedade", humanas.Id)
        };


        await context.Assuntos.AddRangeAsync(assuntos);
        await context.SaveChangesAsync();

        if (!await context.Questoes.AnyAsync())
        {
            var algebra = assuntos.First(a => a.Nome == "Álgebra");
            var historia = assuntos.First(a => a.Nome == "História");

            var q1 = new Questao("Resolva a equação 2x = 10", EnemPrep.Domain.Enums.NivelDificuldade.Facil, algebra.Id, "Basta dividir 10 por 2.");
            var q2 = new Questao("Em que ano o Brasil foi descoberto?", EnemPrep.Domain.Enums.NivelDificuldade.Facil, historia.Id, "Pedro Álvares Cabral chegou em 1500.");

            await context.Questoes.AddRangeAsync(q1, q2);
            await context.SaveChangesAsync();

            await context.Alternativas.AddRangeAsync(
                new Alternativa("10", false, q1.Id),
                new Alternativa("5", true, q1.Id),
                new Alternativa("2", false, q1.Id),
                new Alternativa("20", false, q1.Id),
                new Alternativa("0", false, q1.Id),
                
                new Alternativa("1492", false, q2.Id),
                new Alternativa("1500", true, q2.Id),
                new Alternativa("1822", false, q2.Id),
                new Alternativa("1889", false, q2.Id),
                new Alternativa("1930", false, q2.Id)
            );
            await context.SaveChangesAsync();

            var simulado = new Simulado("Simulado Diagnóstico Rápido", 2026, TimeSpan.FromMinutes(30));
            await context.Simulados.AddAsync(simulado);
            await context.SaveChangesAsync();

            await context.SimuladosQuestoes.AddRangeAsync(
                new SimuladoQuestao(simulado.Id, q1.Id, 1),
                new SimuladoQuestao(simulado.Id, q2.Id, 2)
            );
            
            var desafio = new DesafioDiario("Acerte 2 questões fáceis", DateTime.UtcNow.Date, q1.Id, 50);
            await context.DesafiosDiarios.AddAsync(desafio);
            await context.SaveChangesAsync();
        }

        if (!await context.Conquistas.AnyAsync())
        {
            var conquistas = new List<Conquista>
            {
                new("Primeiro Passo", "Resolva seu primeiro simulado no app.", "fa-solid fa-medal", 100),
                new("Estudante Ouro", "Mantenha uma ofensiva de 7 dias consecutivos.", "fa-solid fa-fire", 500)
            };
            await context.Conquistas.AddRangeAsync(conquistas);
            await context.SaveChangesAsync();
        }
    }
}
