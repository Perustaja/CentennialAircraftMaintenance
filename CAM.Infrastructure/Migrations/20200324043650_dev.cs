using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CAM.Infrastructure.Migrations
{
    public partial class dev : Migration
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
                name: "SquawkStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    IsOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquawkStatuses", x => x.Id);
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
                name: "WorkStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    PartCategoryId = table.Column<int>(nullable: false),
                    CataloguePartNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 600, nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    ImageThumbPath = table.Column<string>(nullable: true),
                    CurrentStock = table.Column<int>(nullable: false),
                    QtySoldToDate = table.Column<int>(nullable: false),
                    PriceIn = table.Column<decimal>(nullable: false),
                    PriceOut = table.Column<decimal>(nullable: true),
                    Vendor = table.Column<string>(maxLength: 30, nullable: false),
                    IsDiscontinued = table.Column<bool>(nullable: false),
                    MinimumStock = table.Column<int>(nullable: true)
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
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkStatusId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 15, nullable: false),
                    AircraftId = table.Column<string>(maxLength: 20, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateFinalized = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkStatuses_WorkStatusId",
                        column: x => x.WorkStatusId,
                        principalTable: "WorkStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Squawks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AircraftId = table.Column<string>(maxLength: 20, nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Pilot = table.Column<string>(maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    Resolution = table.Column<string>(maxLength: 1000, nullable: true),
                    DateResolved = table.Column<DateTime>(nullable: true),
                    ResolvedBy = table.Column<string>(maxLength: 30, nullable: true),
                    IsGroundable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squawks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Squawks_Aircraft_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Squawks_SquawkStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "SquawkStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discrepancies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AircraftId = table.Column<string>(nullable: true),
                    WorkStatusId = table.Column<int>(nullable: false),
                    WorkOrderId = table.Column<int>(nullable: true),
                    SquawkId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(maxLength: 15, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Resolution = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateFinalized = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 60, nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Discrepancies_WorkStatuses_WorkStatusId",
                        column: x => x.WorkStatusId,
                        principalTable: "WorkStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscrepancyParts",
                columns: table => new
                {
                    DiscrepancyId = table.Column<int>(nullable: false),
                    PartId = table.Column<string>(nullable: false),
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
                    LaborInHours = table.Column<decimal>(nullable: false)
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
                name: "IX_Discrepancies_WorkStatusId",
                table: "Discrepancies",
                column: "WorkStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscrepancyParts_PartId",
                table: "DiscrepancyParts",
                column: "PartId");

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
                name: "IX_Squawks_AircraftId",
                table: "Squawks",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Squawks_StatusId",
                table: "Squawks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkStatusId",
                table: "WorkOrders",
                column: "WorkStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscrepancyParts");

            migrationBuilder.DropTable(
                name: "LaborRecords");

            migrationBuilder.DropTable(
                name: "Squawks");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Discrepancies");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "SquawkStatuses");

            migrationBuilder.DropTable(
                name: "PartCategories");

            migrationBuilder.DropTable(
                name: "Aircraft");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "WorkStatuses");
        }
    }
}
