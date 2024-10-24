using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ucinci_RangListe_RangListaId",
                table: "Ucinci");

            migrationBuilder.DropIndex(
                name: "IX_Ucinci_RangListaId",
                table: "Ucinci");

            migrationBuilder.DropColumn(
                name: "RangListaId",
                table: "Ucinci");

            migrationBuilder.DropColumn(
                name: "UcinakId",
                table: "RangListe");

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Trke",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RangListaId",
                table: "Ucinci",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Naziv",
                table: "Trke",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<string>(
                name: "UcinakId",
                table: "RangListe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ucinci_RangListaId",
                table: "Ucinci",
                column: "RangListaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ucinci_RangListe_RangListaId",
                table: "Ucinci",
                column: "RangListaId",
                principalTable: "RangListe",
                principalColumn: "Id");
        }
    }
}
