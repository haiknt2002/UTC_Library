using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdminEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdminPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookCategory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookAuthor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookCopies = table.Column<int>(type: "int", nullable: true),
                    BookPub = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookPubName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookISBN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Copyright = table.Column<int>(type: "int", nullable: true),
                    DateAdded = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserGender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserDep = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    UserAdmNo = table.Column<int>(type: "int", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserPass = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TranId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    BookTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookISBN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TranId);
                    table.ForeignKey(
                        name: "FK_Transactions_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId");
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BookId",
                table: "Transactions",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
