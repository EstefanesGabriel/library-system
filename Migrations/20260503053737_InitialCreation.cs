using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaBiblioteca.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institution",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    TaxId = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    ContactEmail = table.Column<string>(type: "NVARCHAR(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institution", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Title = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Isbn = table.Column<string>(type: "NVARCHAR(13)", maxLength: 13, nullable: false),
                    Genre = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    CoverImageUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    PublicationYear = table.Column<int>(type: "INT", nullable: false),
                    StockQuantity = table.Column<int>(type: "INT", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    InstitutionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FullName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    NationalId = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Role = table.Column<int>(type: "INT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsActive = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    InsitutionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Institution_InsitutionId",
                        column: x => x.InsitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    BookId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DueDate = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false),
                    ReturnedAt = table.Column<DateTime>(type: "SMALLDATETIME", nullable: true),
                    LoanStatus = table.Column<int>(type: "INT", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loan_Book",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loan_Institution",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loan_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LoanId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FK_Multa_Loan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "SMALLDATETIME", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsPaid = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    PaidAt = table.Column<DateTime>(type: "SMALLDATETIME", nullable: true),
                    InstitutionId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fine_Institution_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Institution",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fine_Loan_FK_Multa_Loan",
                        column: x => x.FK_Multa_Loan,
                        principalTable: "Loan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Author",
                table: "Book",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Book_InstitutionId",
                table: "Book",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Isbn",
                table: "Book",
                column: "Isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Title",
                table: "Book",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Fine_FK_Multa_Loan",
                table: "Fine",
                column: "FK_Multa_Loan");

            migrationBuilder.CreateIndex(
                name: "IX_Fine_InstitutionId",
                table: "Fine",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Fine_IsPaid",
                table: "Fine",
                column: "IsPaid");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_BookId",
                table: "Loan",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_InstitutionId",
                table: "Loan",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanDate",
                table: "Loan",
                column: "LoanDate");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanStatus",
                table: "Loan",
                column: "LoanStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_UserId",
                table: "Loan",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_InsitutionId",
                table: "User",
                column: "InsitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_NationalId",
                table: "User",
                column: "NationalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fine");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Institution");
        }
    }
}
