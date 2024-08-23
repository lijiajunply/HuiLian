using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuiLianMedical.Share.Migrations
{
    /// <inheritdoc />
    public partial class AidChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AidModel",
                newName: "Num");

            migrationBuilder.AddColumn<string>(
                name: "IdCard",
                table: "AidModel",
                type: "varchar(18)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCard",
                table: "AidModel");

            migrationBuilder.RenameColumn(
                name: "Num",
                table: "AidModel",
                newName: "Name");
        }
    }
}
