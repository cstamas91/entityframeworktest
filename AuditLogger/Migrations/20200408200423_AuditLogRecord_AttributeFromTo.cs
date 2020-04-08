using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditLogger.Migrations
{
    public partial class AuditLogRecord_AttributeFromTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "AuditLogRecord");

            migrationBuilder.AddColumn<string>(
                name: "Attribute",
                table: "AuditLogRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "AuditLogRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "AuditLogRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attribute",
                table: "AuditLogRecord");

            migrationBuilder.DropColumn(
                name: "From",
                table: "AuditLogRecord");

            migrationBuilder.DropColumn(
                name: "To",
                table: "AuditLogRecord");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "AuditLogRecord",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
