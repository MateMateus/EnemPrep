using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnemPrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGamificacaoESimulados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "DesafiosDiarios",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Conquistas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Icone",
                table: "Conquistas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Conquistas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.CreateTable(
                name: "Simulados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AnoReferencia = table.Column<int>(type: "int", nullable: true),
                    DuracaoMaxima = table.Column<TimeSpan>(type: "time", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simulados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimuladosQuestoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SimuladoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimuladosQuestoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimuladosQuestoes_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SimuladosQuestoes_Simulados_SimuladoId",
                        column: x => x.SimuladoId,
                        principalTable: "Simulados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TentativasSimulado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SimuladoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotaTotalBruta = table.Column<int>(type: "int", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TentativasSimulado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TentativasSimulado_Simulados_SimuladoId",
                        column: x => x.SimuladoId,
                        principalTable: "Simulados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TentativasSimulado_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostasSimulado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TentativaSimuladoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlternativaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Correta = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasSimulado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostasSimulado_Alternativas_AlternativaId",
                        column: x => x.AlternativaId,
                        principalTable: "Alternativas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespostasSimulado_Questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RespostasSimulado_TentativasSimulado_TentativaSimuladoId",
                        column: x => x.TentativaSimuladoId,
                        principalTable: "TentativasSimulado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespostasSimulado_AlternativaId",
                table: "RespostasSimulado",
                column: "AlternativaId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasSimulado_QuestaoId",
                table: "RespostasSimulado",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasSimulado_TentativaSimuladoId",
                table: "RespostasSimulado",
                column: "TentativaSimuladoId");

            migrationBuilder.CreateIndex(
                name: "IX_SimuladosQuestoes_QuestaoId",
                table: "SimuladosQuestoes",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SimuladosQuestoes_SimuladoId",
                table: "SimuladosQuestoes",
                column: "SimuladoId");

            migrationBuilder.CreateIndex(
                name: "IX_TentativasSimulado_SimuladoId",
                table: "TentativasSimulado",
                column: "SimuladoId");

            migrationBuilder.CreateIndex(
                name: "IX_TentativasSimulado_UsuarioId",
                table: "TentativasSimulado",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespostasSimulado");

            migrationBuilder.DropTable(
                name: "SimuladosQuestoes");

            migrationBuilder.DropTable(
                name: "TentativasSimulado");

            migrationBuilder.DropTable(
                name: "Simulados");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "DesafiosDiarios",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Conquistas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Icone",
                table: "Conquistas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Conquistas",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }
    }
}
