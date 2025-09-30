using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Intituicao.Financeira.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CondicaoVeiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicaoVeiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusPagamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVeiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVeiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClienteCpfCnpj = table.Column<long>(type: "bigint", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "numeric(9,2)", nullable: false),
                    TaxaMensal = table.Column<decimal>(type: "numeric(2,2)", nullable: false),
                    PrazoMeses = table.Column<int>(type: "integer", nullable: false),
                    DataVencimentoPrimeiraParcela = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoVeiculoId = table.Column<int>(type: "integer", nullable: false),
                    CondicaoVeiculoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contrato_CondicaoVeiculo_CondicaoVeiculoId",
                        column: x => x.CondicaoVeiculoId,
                        principalTable: "CondicaoVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_TipoVeiculo_TipoVeiculoId",
                        column: x => x.TipoVeiculoId,
                        principalTable: "TipoVeiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SaldoDevedor = table.Column<decimal>(type: "numeric(9,2)", nullable: false),
                    StatusPagamentoId = table.Column<int>(type: "integer", nullable: false),
                    ContratoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamento_Contrato_ContratoId",
                        column: x => x.ContratoId,
                        principalTable: "Contrato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pagamento_StatusPagamento_StatusPagamentoId",
                        column: x => x.StatusPagamentoId,
                        principalTable: "StatusPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CondicaoVeiculo_Id",
                table: "CondicaoVeiculo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_CondicaoVeiculoId",
                table: "Contrato",
                column: "CondicaoVeiculoId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_TipoVeiculoId",
                table: "Contrato",
                column: "TipoVeiculoId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_ContratoId",
                table: "Pagamento",
                column: "ContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_StatusPagamentoId",
                table: "Pagamento",
                column: "StatusPagamentoId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_StatusPagamento_Id",
                table: "StatusPagamento",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TipoVeiculo_Id",
                table: "TipoVeiculo",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "StatusPagamento");

            migrationBuilder.DropTable(
                name: "CondicaoVeiculo");

            migrationBuilder.DropTable(
                name: "TipoVeiculo");
        }
    }
}
