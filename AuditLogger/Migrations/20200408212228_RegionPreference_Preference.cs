using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditLogger.Migrations
{
    public partial class RegionPreference_Preference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegionPreferences_Preferences_PreferencesId",
                table: "RegionPreferences");

            migrationBuilder.AlterColumn<int>(
                name: "PreferencesId",
                table: "RegionPreferences",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RegionPreferences_Preferences_PreferencesId",
                table: "RegionPreferences",
                column: "PreferencesId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegionPreferences_Preferences_PreferencesId",
                table: "RegionPreferences");

            migrationBuilder.AlterColumn<int>(
                name: "PreferencesId",
                table: "RegionPreferences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_RegionPreferences_Preferences_PreferencesId",
                table: "RegionPreferences",
                column: "PreferencesId",
                principalTable: "Preferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
