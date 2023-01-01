using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Saas.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addednewClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CompanyRoles");

            migrationBuilder.RenameTable(
                name: "CompanyOperationUserClaim",
                schema: "Roles",
                newName: "CompanyOperationUserClaim",
                newSchema: "CompanyRoles");

            migrationBuilder.RenameTable(
                name: "CompanyOperationClaim",
                schema: "Roles",
                newName: "CompanyOperationClaim",
                newSchema: "CompanyRoles");

            migrationBuilder.CreateTable(
                name: "CompanyOffer",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_CompanyOffer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompanyOffer_CompanyBranch_BranchId",
                        column: x => x.BranchId,
                        principalSchema: "Company",
                        principalTable: "CompanyBranch",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CompanyOffer_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnits",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Calculate = table.Column<double>(type: "float", nullable: true),
                    CalculateCost = table.Column<double>(type: "float", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_ProductUnits", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductUnits_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OfferRow",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyProductUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    ApproveGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceApproveType = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_OfferRow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OfferRow_CompanyOffer_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "CompanyOffer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OfferRow_ProductUnits_CompanyProductUnitId",
                        column: x => x.CompanyProductUnitId,
                        principalTable: "ProductUnits",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OfferRow_Products_CompanyProductId",
                        column: x => x.CompanyProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOffer_BranchId",
                table: "CompanyOffer",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyOffer_CompanyId",
                table: "CompanyOffer",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRow_CompanyProductId",
                table: "OfferRow",
                column: "CompanyProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRow_CompanyProductUnitId",
                table: "OfferRow",
                column: "CompanyProductUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferRow_HeaderId",
                table: "OfferRow",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_CompanyId",
                table: "ProductUnits",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferRow");

            migrationBuilder.DropTable(
                name: "CompanyOffer");

            migrationBuilder.DropTable(
                name: "ProductUnits");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.EnsureSchema(
                name: "Roles");

            migrationBuilder.RenameTable(
                name: "CompanyOperationUserClaim",
                schema: "CompanyRoles",
                newName: "CompanyOperationUserClaim",
                newSchema: "Roles");

            migrationBuilder.RenameTable(
                name: "CompanyOperationClaim",
                schema: "CompanyRoles",
                newName: "CompanyOperationClaim",
                newSchema: "Roles");
        }
    }
}
