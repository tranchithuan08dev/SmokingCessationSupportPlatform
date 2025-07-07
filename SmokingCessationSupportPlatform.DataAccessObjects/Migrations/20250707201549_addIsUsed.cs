using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmokingCessationSupportPlatform.DataAccessObjects.Migrations
{
    /// <inheritdoc />
    public partial class addIsUsed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "PasswordResetTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "PasswordResetTokens");
        }
    }
}
