using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GrpcServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Epc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character(8)", fixedLength: true, maxLength: 8, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Park",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    AsuNumber = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Park_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Path",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    AsuNumber = table.Column<string>(type: "character(2)", fixedLength: true, maxLength: 2, nullable: false),
                    IdPark = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Path_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Path_Park",
                        column: x => x.IdPark,
                        principalTable: "Park",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EpcEvent",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdPath = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    NumberInOrder = table.Column<int>(type: "integer", nullable: false),
                    IdEpc = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpcEvent", x => x.Time);
                    table.ForeignKey(
                        name: "FK_EpcEvent_Epc_IdEpc",
                        column: x => x.IdEpc,
                        principalTable: "Epc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EpcEvent_Path_IdPath",
                        column: x => x.IdPath,
                        principalTable: "Path",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventAdd",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdPath = table.Column<int>(type: "integer", nullable: false),
                    Direction = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAdd", x => x.Time);
                    table.ForeignKey(
                        name: "FK_EventAdd_EpcEvent_Time",
                        column: x => x.Time,
                        principalTable: "EpcEvent",
                        principalColumn: "Time",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventSub",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdPath = table.Column<int>(type: "integer", nullable: false),
                    Direction = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSub", x => x.Time);
                    table.ForeignKey(
                        name: "FK_EventSub_EpcEvent_Time",
                        column: x => x.Time,
                        principalTable: "EpcEvent",
                        principalColumn: "Time",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventArrival",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdPath = table.Column<int>(type: "integer", nullable: false),
                    TrainNumber = table.Column<string>(type: "character(4)", fixedLength: true, maxLength: 4, nullable: false),
                    TrainIndex = table.Column<string>(type: "character(17)", fixedLength: true, maxLength: 17, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventArrival", x => x.Time);
                    table.ForeignKey(
                        name: "FK_EventArrival_EventAdd_Time",
                        column: x => x.Time,
                        principalTable: "EventAdd",
                        principalColumn: "Time",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventDeparture",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IdPath = table.Column<int>(type: "integer", nullable: false),
                    TrainNumber = table.Column<string>(type: "character(4)", fixedLength: true, maxLength: 4, nullable: false),
                    TrainIndex = table.Column<string>(type: "character(17)", fixedLength: true, maxLength: 17, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDeparture", x => x.Time);
                    table.ForeignKey(
                        name: "FK_EventDeparture_EventSub_Time",
                        column: x => x.Time,
                        principalTable: "EventSub",
                        principalColumn: "Time",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EpcEvent_IdEpc",
                table: "EpcEvent",
                column: "IdEpc",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EpcEvent_IdPath",
                table: "EpcEvent",
                column: "IdPath");

            migrationBuilder.CreateIndex(
                name: "IX_Path_IdPark",
                table: "Path",
                column: "IdPark");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventArrival");

            migrationBuilder.DropTable(
                name: "EventDeparture");

            migrationBuilder.DropTable(
                name: "EventAdd");

            migrationBuilder.DropTable(
                name: "EventSub");

            migrationBuilder.DropTable(
                name: "EpcEvent");

            migrationBuilder.DropTable(
                name: "Epc");

            migrationBuilder.DropTable(
                name: "Path");

            migrationBuilder.DropTable(
                name: "Park");
        }
    }
}
