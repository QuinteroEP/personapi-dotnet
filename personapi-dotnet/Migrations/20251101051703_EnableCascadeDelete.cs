using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace personapi_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class EnableCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "arq_per_db");

            migrationBuilder.CreateTable(
                name: "persona",
                schema: "arq_per_db",
                columns: table => new
                {
                    cc = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    apellido = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    genero = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    edad = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persona_cc", x => x.cc);
                });

            migrationBuilder.CreateTable(
                name: "profesion",
                schema: "arq_per_db",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    nom = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: false),
                    des = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profesion_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "telefono",
                schema: "arq_per_db",
                columns: table => new
                {
                    num = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    oper = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: false),
                    duenio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_telefono_num", x => x.num);
                    table.ForeignKey(
                        name: "telefono$telefono_persona_fk",
                        column: x => x.duenio,
                        principalSchema: "arq_per_db",
                        principalTable: "persona",
                        principalColumn: "cc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "estudios",
                schema: "arq_per_db",
                columns: table => new
                {
                    id_prof = table.Column<int>(type: "int", nullable: false),
                    cc_per = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "(NULL)"),
                    univer = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValueSql: "(NULL)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estudios_id_prof", x => new { x.id_prof, x.cc_per });
                    table.ForeignKey(
                        name: "estudios$estudio_persona_fk",
                        column: x => x.cc_per,
                        principalSchema: "arq_per_db",
                        principalTable: "persona",
                        principalColumn: "cc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "estudios$estudio_profesion_fk",
                        column: x => x.id_prof,
                        principalSchema: "arq_per_db",
                        principalTable: "profesion",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "estudio_persona_fk",
                schema: "arq_per_db",
                table: "estudios",
                column: "cc_per");

            migrationBuilder.CreateIndex(
                name: "telefono_persona_fk",
                schema: "arq_per_db",
                table: "telefono",
                column: "duenio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estudios",
                schema: "arq_per_db");

            migrationBuilder.DropTable(
                name: "telefono",
                schema: "arq_per_db");

            migrationBuilder.DropTable(
                name: "profesion",
                schema: "arq_per_db");

            migrationBuilder.DropTable(
                name: "persona",
                schema: "arq_per_db");
        }
    }
}
