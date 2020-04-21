using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CAM.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    CertificationNum = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    AircraftId = table.Column<string>(maxLength: 20, nullable: false),
                    Hobbs = table.Column<decimal>(nullable: false),
                    AirTime = table.Column<int>(nullable: false),
                    Tach1 = table.Column<decimal>(nullable: false),
                    Tach2 = table.Column<decimal>(nullable: false),
                    Prop1 = table.Column<decimal>(nullable: false),
                    Prop2 = table.Column<decimal>(nullable: false),
                    AircraftTotal = table.Column<decimal>(nullable: false),
                    Engine1Total = table.Column<decimal>(nullable: false),
                    Engine2Total = table.Column<decimal>(nullable: false),
                    Cycles = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.AircraftId);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AircraftId = table.Column<string>(maxLength: 20, nullable: false),
                    Title = table.Column<string>(maxLength: 40, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateFinalized = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    WorkStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PartCategoryId = table.Column<int>(nullable: false),
                    MfrsPartNumber = table.Column<string>(maxLength: 50, nullable: true),
                    CataloguePartNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 600, nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    ImageThumbPath = table.Column<string>(nullable: true),
                    CurrentStock = table.Column<int>(nullable: false),
                    PriceIn = table.Column<decimal>(nullable: false),
                    PriceOut = table.Column<decimal>(nullable: true),
                    Vendor = table.Column<string>(maxLength: 30, nullable: false),
                    MinimumStock = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_PartCategories_PartCategoryId",
                        column: x => x.PartCategoryId,
                        principalTable: "PartCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aircraft",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 20, nullable: false),
                    Year = table.Column<int>(nullable: true),
                    Model = table.Column<string>(maxLength: 20, nullable: false),
                    SerialNum = table.Column<string>(maxLength: 30, nullable: true),
                    IsTwin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircraft", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aircraft_Times_Id",
                        column: x => x.Id,
                        principalTable: "Times",
                        principalColumn: "AircraftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscrepancyTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkOrderTemplateId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(maxLength: 15, nullable: true),
                    Description = table.Column<string>(maxLength: 75, nullable: false),
                    Resolution = table.Column<string>(maxLength: 600, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscrepancyTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscrepancyTemplates_WorkOrderTemplates_WorkOrderTemplateId",
                        column: x => x.WorkOrderTemplateId,
                        principalTable: "WorkOrderTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discrepancies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AircraftId = table.Column<string>(nullable: true),
                    WorkOrderId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 40, nullable: true),
                    Description = table.Column<string>(maxLength: 75, nullable: false),
                    Resolution = table.Column<string>(maxLength: 600, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateFinalized = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 20, nullable: true),
                    WorkStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discrepancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discrepancies_Aircraft_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discrepancies_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscrepancyTemplateParts",
                columns: table => new
                {
                    DiscrepancyTemplateId = table.Column<int>(nullable: false),
                    PartId = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscrepancyTemplateParts", x => new { x.DiscrepancyTemplateId, x.PartId });
                    table.ForeignKey(
                        name: "FK_DiscrepancyTemplateParts_DiscrepancyTemplates_DiscrepancyTemplateId",
                        column: x => x.DiscrepancyTemplateId,
                        principalTable: "DiscrepancyTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscrepancyTemplateParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderTemplateDiscrepancyTemplates",
                columns: table => new
                {
                    WorkOrderTemplateId = table.Column<int>(nullable: false),
                    DiscrepancyTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderTemplateDiscrepancyTemplates", x => new { x.WorkOrderTemplateId, x.DiscrepancyTemplateId });
                    table.ForeignKey(
                        name: "FK_WorkOrderTemplateDiscrepancyTemplates_DiscrepancyTemplates_DiscrepancyTemplateId",
                        column: x => x.DiscrepancyTemplateId,
                        principalTable: "DiscrepancyTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderTemplateDiscrepancyTemplates_WorkOrderTemplates_WorkOrderTemplateId",
                        column: x => x.WorkOrderTemplateId,
                        principalTable: "WorkOrderTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscrepancyParts",
                columns: table => new
                {
                    DiscrepancyId = table.Column<int>(nullable: false),
                    PartId = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscrepancyParts", x => new { x.DiscrepancyId, x.PartId });
                    table.ForeignKey(
                        name: "FK_DiscrepancyParts_Discrepancies_DiscrepancyId",
                        column: x => x.DiscrepancyId,
                        principalTable: "Discrepancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscrepancyParts_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LaborRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiscrepancyId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    LaborInHours = table.Column<decimal>(nullable: false),
                    DatePerformed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaborRecords_Discrepancies_DiscrepancyId",
                        column: x => x.DiscrepancyId,
                        principalTable: "Discrepancies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaborRecords_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discrepancies_AircraftId",
                table: "Discrepancies",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Discrepancies_WorkOrderId",
                table: "Discrepancies",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscrepancyParts_PartId",
                table: "DiscrepancyParts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscrepancyTemplateParts_PartId",
                table: "DiscrepancyTemplateParts",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscrepancyTemplates_WorkOrderTemplateId",
                table: "DiscrepancyTemplates",
                column: "WorkOrderTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_LaborRecords_DiscrepancyId",
                table: "LaborRecords",
                column: "DiscrepancyId");

            migrationBuilder.CreateIndex(
                name: "IX_LaborRecords_EmployeeId",
                table: "LaborRecords",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartCategoryId",
                table: "Parts",
                column: "PartCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTemplateDiscrepancyTemplates_DiscrepancyTemplateId",
                table: "WorkOrderTemplateDiscrepancyTemplates",
                column: "DiscrepancyTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscrepancyParts");

            migrationBuilder.DropTable(
                name: "DiscrepancyTemplateParts");

            migrationBuilder.DropTable(
                name: "LaborRecords");

            migrationBuilder.DropTable(
                name: "WorkOrderTemplateDiscrepancyTemplates");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Discrepancies");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "DiscrepancyTemplates");

            migrationBuilder.DropTable(
                name: "PartCategories");

            migrationBuilder.DropTable(
                name: "Aircraft");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "WorkOrderTemplates");

            migrationBuilder.DropTable(
                name: "Times");
        }
    }
}
