using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnemPrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conquistas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Icone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PontosZ = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conquistas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfisUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NomeApresentacao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfisUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assuntos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assuntos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assuntos_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PerfilUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_PerfisUsuario_PerfilUsuarioId",
                        column: x => x.PerfilUsuarioId,
                        principalTable: "PerfisUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MateriaisEstudo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UrlArquivo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AssuntoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaisEstudo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriaisEstudo_Assuntos_AssuntoId",
                        column: x => x.AssuntoId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Enunciado = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Dificuldade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Explicacao = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    AssuntoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questoes_Assuntos_AssuntoId",
                        column: x => x.AssuntoId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoAulas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UrlVideo = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DuracaoSegundos = table.Column<int>(type: "int", nullable: false),
                    AssuntoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoAulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoAulas_Assuntos_AssuntoId",
                        column: x => x.AssuntoId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanosEstudo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosEstudo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanosEstudo_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreaksUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiasConsecutivos = table.Column<int>(type: "int", nullable: false),
                    MaiorStreak = table.Column<int>(type: "int", nullable: false),
                    UltimaAtividade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreaksUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreaksUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioConquistas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConquistaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataObtencao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioConquistas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioConquistas_Conquistas_ConquistaId",
                        column: x => x.ConquistaId,
                        principalTable: "Conquistas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioConquistas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alternativas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Correta = table.Column<bool>(type: "bit", nullable: false),
                    QuestaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alternativas_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesafiosDiarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataDesafio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuestaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XPRecompensa = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesafiosDiarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesafiosDiarios_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanosEstudoItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoEstudoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssuntoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosEstudoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanosEstudoItens_Assuntos_AssuntoId",
                        column: x => x.AssuntoId,
                        principalTable: "Assuntos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanosEstudoItens_PlanosEstudo_PlanoEstudoId",
                        column: x => x.PlanoEstudoId,
                        principalTable: "PlanosEstudo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TentativasQuestao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternativaSelecionadaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Acertou = table.Column<bool>(type: "bit", nullable: false),
                    TempoGastoSegundos = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TentativasQuestao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TentativasQuestao_Alternativas_AlternativaSelecionadaId",
                        column: x => x.AlternativaSelecionadaId,
                        principalTable: "Alternativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TentativasQuestao_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TentativasQuestao_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativas_QuestaoId",
                table: "Alternativas",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Assuntos_MateriaId",
                table: "Assuntos",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_DesafiosDiarios_QuestaoId",
                table: "DesafiosDiarios",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaisEstudo_AssuntoId",
                table: "MateriaisEstudo",
                column: "AssuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosEstudo_UsuarioId",
                table: "PlanosEstudo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosEstudoItens_AssuntoId",
                table: "PlanosEstudoItens",
                column: "AssuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanosEstudoItens_PlanoEstudoId",
                table: "PlanosEstudoItens",
                column: "PlanoEstudoId");

            migrationBuilder.CreateIndex(
                name: "IX_Questoes_AssuntoId",
                table: "Questoes",
                column: "AssuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_StreaksUsuario_UsuarioId",
                table: "StreaksUsuario",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TentativasQuestao_AlternativaSelecionadaId",
                table: "TentativasQuestao",
                column: "AlternativaSelecionadaId");

            migrationBuilder.CreateIndex(
                name: "IX_TentativasQuestao_QuestaoId",
                table: "TentativasQuestao",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_TentativasQuestao_UsuarioId",
                table: "TentativasQuestao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioConquistas_ConquistaId",
                table: "UsuarioConquistas",
                column: "ConquistaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioConquistas_UsuarioId_ConquistaId",
                table: "UsuarioConquistas",
                columns: new[] { "UsuarioId", "ConquistaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PerfilUsuarioId",
                table: "Usuarios",
                column: "PerfilUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoAulas_AssuntoId",
                table: "VideoAulas",
                column: "AssuntoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesafiosDiarios");

            migrationBuilder.DropTable(
                name: "MateriaisEstudo");

            migrationBuilder.DropTable(
                name: "PlanosEstudoItens");

            migrationBuilder.DropTable(
                name: "StreaksUsuario");

            migrationBuilder.DropTable(
                name: "TentativasQuestao");

            migrationBuilder.DropTable(
                name: "UsuarioConquistas");

            migrationBuilder.DropTable(
                name: "VideoAulas");

            migrationBuilder.DropTable(
                name: "PlanosEstudo");

            migrationBuilder.DropTable(
                name: "Alternativas");

            migrationBuilder.DropTable(
                name: "Conquistas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Questoes");

            migrationBuilder.DropTable(
                name: "PerfisUsuario");

            migrationBuilder.DropTable(
                name: "Assuntos");

            migrationBuilder.DropTable(
                name: "Materias");
        }
    }
}
