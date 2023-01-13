using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CId = table.Column<int>(name: "C_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CName = table.Column<string>(name: "C_Name", type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CExpenseLimit = table.Column<int>(name: "C_Expense_Limit", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CId);
                });

            migrationBuilder.CreateTable(
                name: "TotalExpenseLimit",
                columns: table => new
                {
                    TotalExpenseLimitId = table.Column<int>(name: "Total_ExpenseLimit_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalExpenseLimit = table.Column<int>(name: "Total_ExpenseLimit", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalExpenseLimit", x => x.TotalExpenseLimitId);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    EId = table.Column<int>(name: "E_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ETitle = table.Column<string>(name: "E_Title", type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EDescription = table.Column<string>(name: "E_Description", type: "nvarchar(max)", nullable: false),
                    EAmount = table.Column<int>(name: "E_Amount", type: "int", nullable: false),
                    EDate = table.Column<DateTime>(name: "E_Date", type: "datetime2", nullable: false),
                    Cid = table.Column<int>(name: "C_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.EId);
                    table.ForeignKey(
                        name: "FK_Expenses_Categories_C_id",
                        column: x => x.Cid,
                        principalTable: "Categories",
                        principalColumn: "C_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_C_id",
                table: "Expenses",
                column: "C_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "TotalExpenseLimit");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
