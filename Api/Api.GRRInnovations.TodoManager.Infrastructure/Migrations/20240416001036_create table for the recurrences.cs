using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.GRRInnovations.TodoManager.Infrastructure.Migrations
{
    public partial class createtablefortherecurrences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskRecurrenceUid",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TasksRecurrences",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: true),
                    RecurrenceType = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TaskUid = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksRecurrences", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_TasksRecurrences_Tasks_TaskUid",
                        column: x => x.TaskUid,
                        principalTable: "Tasks",
                        principalColumn: "Uid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TasksRecurrences_TaskUid",
                table: "TasksRecurrences",
                column: "TaskUid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasksRecurrences");

            migrationBuilder.DropColumn(
                name: "TaskRecurrenceUid",
                table: "Tasks");
        }
    }
}
