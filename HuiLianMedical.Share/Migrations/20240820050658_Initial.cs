using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HuiLianMedical.Share.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AidModel",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(32)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    Image = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AidModel", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(32)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    Description = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(32)", nullable: false),
                    Password = table.Column<string>(type: "varchar(32)", nullable: false),
                    Email = table.Column<string>(type: "varchar(64)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(14)", nullable: false),
                    Identity = table.Column<string>(type: "varchar(32)", nullable: false),
                    Avatar = table.Column<string>(type: "varchar(32)", nullable: true),
                    Points = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commodities",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(32)", nullable: false),
                    Name = table.Column<string>(type: "varchar(32)", nullable: false),
                    Description = table.Column<string>(type: "varchar(32)", nullable: false),
                    Image = table.Column<string>(type: "varchar(32)", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<int>(type: "varchar(32)", nullable: false),
                    CreateTime = table.Column<string>(type: "varchar(32)", nullable: false),
                    UserModelId = table.Column<string>(type: "varchar(32)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commodities", x => x.Key);
                    table.ForeignKey(
                        name: "FK_Commodities_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryModelCommodityModel",
                columns: table => new
                {
                    CategoryKey = table.Column<string>(type: "varchar(32)", nullable: false),
                    CommoditiesKey = table.Column<string>(type: "varchar(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryModelCommodityModel", x => new { x.CategoryKey, x.CommoditiesKey });
                    table.ForeignKey(
                        name: "FK_CategoryModelCommodityModel_Categories_CategoryKey",
                        column: x => x.CategoryKey,
                        principalTable: "Categories",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryModelCommodityModel_Commodities_CommoditiesKey",
                        column: x => x.CommoditiesKey,
                        principalTable: "Commodities",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryModelCommodityModel_CommoditiesKey",
                table: "CategoryModelCommodityModel",
                column: "CommoditiesKey");

            migrationBuilder.CreateIndex(
                name: "IX_Commodities_UserModelId",
                table: "Commodities",
                column: "UserModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AidModel");

            migrationBuilder.DropTable(
                name: "CategoryModelCommodityModel");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Commodities");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
