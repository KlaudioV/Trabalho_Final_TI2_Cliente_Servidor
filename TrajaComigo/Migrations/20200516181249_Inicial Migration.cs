using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrajaComigo.Migrations
{
    public partial class InicialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(maxLength: 40, nullable: false),
                    Apelido = table.Column<string>(maxLength: 40, nullable: false),
                    Morada = table.Column<string>(maxLength: 128, nullable: true),
                    CodigoPostal = table.Column<string>(maxLength: 64, nullable: true),
                    Concelho = table.Column<string>(maxLength: 20, nullable: true),
                    Telefone = table.Column<string>(maxLength: 9, nullable: false),
                    Telemovel = table.Column<string>(maxLength: 9, nullable: false),
                    NIF = table.Column<string>(maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designacao = table.Column<string>(maxLength: 64, nullable: true),
                    Descricao = table.Column<string>(maxLength: 255, nullable: true),
                    PrecoUni = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Imagem = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(maxLength: 12, nullable: true),
                    IVA = table.Column<decimal>(type: "decimal(8, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Encomendas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cliente = table.Column<int>(nullable: true),
                    DataEncomenda = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TipoPagamento = table.Column<string>(type: "Text", maxLength: 255, nullable: true),
                    MoradaEntrega = table.Column<string>(maxLength: 128, nullable: false),
                    CodigoPostal = table.Column<string>(maxLength: 64, nullable: false),
                    PrecoProduto = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    ValorEntrega = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    PrecoFinal = table.Column<decimal>(type: "decimal(8, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encomendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encomendas_Clientes_Cliente",
                        column: x => x.Cliente,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalhesEncomendas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Encomenda = table.Column<int>(nullable: true),
                    Produto = table.Column<int>(nullable: true),
                    Tamanhos = table.Column<string>(maxLength: 128, nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    PrecoUni = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(8, 2)", nullable: false),
                    IVA = table.Column<decimal>(type: "decimal(8, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalhesEncomendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalhesEncomendas_Encomendas_Encomenda",
                        column: x => x.Encomenda,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalhesEncomendas_Produtos_Produto",
                        column: x => x.Produto,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstadoEncomendas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Encomenda = table.Column<int>(nullable: true),
                    DataPrevisao = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Notas = table.Column<string>(type: "Text", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoEncomendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadoEncomendas_Encomendas_Encomenda",
                        column: x => x.Encomenda,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesEncomendas_Encomenda",
                table: "DetalhesEncomendas",
                column: "Encomenda");

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesEncomendas_Produto",
                table: "DetalhesEncomendas",
                column: "Produto");

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_Cliente",
                table: "Encomendas",
                column: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoEncomendas_Encomenda",
                table: "EstadoEncomendas",
                column: "Encomenda");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalhesEncomendas");

            migrationBuilder.DropTable(
                name: "EstadoEncomendas");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Encomendas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
