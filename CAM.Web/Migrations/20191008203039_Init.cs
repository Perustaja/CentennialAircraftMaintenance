using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CAM.Web.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aircraft",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 20, nullable: false),
                    ImagePath = table.Column<string>(maxLength: 100, nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Model = table.Column<string>(maxLength: 20, nullable: false),
                    SerialNum = table.Column<string>(maxLength: 30, nullable: true),
                    IsTwin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aircraft", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CertificationNum = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    IsOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    AircraftId = table.Column<string>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Times_Aircraft_AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(nullable: false),
                    PartNumber = table.Column<string>(maxLength: 40, nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    QtyCurrent = table.Column<int>(nullable: false),
                    QtySoldYear = table.Column<int>(nullable: false),
                    PriceIn = table.Column<decimal>(nullable: false),
                    PriceOut = table.Column<decimal>(nullable: false),
                    MinimumStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
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
                        name: "FK_Squawks_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discrepancies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hobbs = table.Column<decimal>(nullable: false),
                    AirTime = table.Column<int>(nullable: false),
                    Tach1 = table.Column<decimal>(nullable: false),
                    Tach2 = table.Column<decimal>(nullable: false),
                    Prop1 = table.Column<decimal>(nullable: false),
                    Prop2 = table.Column<decimal>(nullable: false),
                    AircraftTotal = table.Column<decimal>(nullable: false),
                    Engine1Total = table.Column<decimal>(nullable: false),
                    Engine2Total = table.Column<decimal>(nullable: false),
                    Cycles = table.Column<int>(nullable: false),
                    WorkOrderId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateFinalized = table.Column<DateTime>(nullable: false),
                    IsFinalized = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Resolution = table.Column<string>(maxLength: 1000, nullable: true),
                    AircraftId = table.Column<string>(maxLength: 20, nullable: false),
                    Year = table.Column<int>(nullable: true),
                    Model = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discrepancies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discrepancies_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
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
                name: "IX_Discrepancies_WorkOrderId",
                table: "Discrepancies",
                column: "WorkOrderId");

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
                name: "IX_Parts_CategoryId",
                table: "Parts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Squawks_AircraftId",
                table: "Squawks",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Squawks_StatusId",
                table: "Squawks",
                column: "StatusId");
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
                name: "Times");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Discrepancies");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Aircraft");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "WorkOrders");
        }
    }
}
