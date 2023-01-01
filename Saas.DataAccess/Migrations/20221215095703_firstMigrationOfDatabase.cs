using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class firstMigrationOfDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Company");

            migrationBuilder.EnsureSchema(
                name: "Roles");

            migrationBuilder.EnsureSchema(
                name: "Problem");

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Company",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    TaxOffice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ID);
                },
                comment: "FirmaBilgileri");

            migrationBuilder.CreateTable(
                name: "CompanyOperationClaim",
                schema: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOperationClaim", x => x.ID);
                },
                comment: "Yetkiler");

            migrationBuilder.CreateTable(
                name: "Log",
                schema: "Problem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Audit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.ID);
                },
                comment: "Log Kayıtları");

            migrationBuilder.CreateTable(
                name: "CompanyBranch",
                schema: "Company",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBranch", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyBranch_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                },
                comment: "Firma şubeleri");

            migrationBuilder.CreateTable(
                name: "CompanyUser",
                schema: "Company",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassWordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PassWordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsStudent = table.Column<bool>(type: "bit", nullable: false, comment: "IsStudent? Yes-No"),
                    SysAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Company Admin"),
                    BranchAdmin = table.Column<bool>(type: "bit", nullable: false, comment: "Branch Admin"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyUser_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                },
                comment: "Firma Kullanicilari");

            migrationBuilder.CreateTable(
                name: "CompanyOperationUserClaim",
                schema: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyOperationClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOperationUserClaim", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyOperationUserClaim_CompanyOperationClaim_CompanyOperationClaimId",
                        column: x => x.CompanyOperationClaimId,
                        principalSchema: "Roles",
                        principalTable: "CompanyOperationClaim",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CompanyOperationUserClaim_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalSchema: "Company",
                        principalTable: "CompanyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                },
                comment: "Kullanici Yetkileri");

            migrationBuilder.CreateTable(
                name: "CompanyUserBranches",
                schema: "Company",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionThree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedByGui = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserBranches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyUserBranches_CompanyBranch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Company",
                        principalTable: "CompanyBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CompanyUserBranches_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalSchema: "Company",
                        principalTable: "CompanyUser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                },
                comment: "Kullanicinin Bağli oldugu Şubeler");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBranch_CompanyId",
                schema: "Company",
                table: "CompanyBranch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOperationUserClaim_CompanyOperationClaimId",
                schema: "Roles",
                table: "CompanyOperationUserClaim",
                column: "CompanyOperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOperationUserClaim_CompanyUserId",
                schema: "Roles",
                table: "CompanyOperationUserClaim",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_CompanyId",
                schema: "Company",
                table: "CompanyUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserBranches_BranchId",
                schema: "Company",
                table: "CompanyUserBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserBranches_CompanyUserId",
                schema: "Company",
                table: "CompanyUserBranches",
                column: "CompanyUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyOperationUserClaim",
                schema: "Roles");

            migrationBuilder.DropTable(
                name: "CompanyUserBranches",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Log",
                schema: "Problem");

            migrationBuilder.DropTable(
                name: "CompanyOperationClaim",
                schema: "Roles");

            migrationBuilder.DropTable(
                name: "CompanyBranch",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "CompanyUser",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Company");
        }
    }
}
