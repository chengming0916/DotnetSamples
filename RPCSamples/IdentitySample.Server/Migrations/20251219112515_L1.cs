using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentitySample.Server.Migrations
{
    /// <inheritdoc />
    public partial class L1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "SYS_USER",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "SYS_ROLE",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b400b20-3052-4fe0-ad0a-e32a9a87ed7b", "efbb839a-28ab-4762-9f8c-c5aa9d4f1a4a", "管理员", null });

            migrationBuilder.InsertData(
                table: "SYS_USER",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bbb5510c-59ef-4988-9ca7-4d9d40a1deb9", 0, "28bac77e-5135-4bd7-8afd-523c767fe576", "管理员", null, false, false, null, "ADMIN", null, "AQAAAAEAACcQAAAAEChYqJukWs6yIODbphrtn5xOgqscDwFEHIG7VBS5/l4H6ejXlh09QiBtWNvbZCAXyw==", null, false, "WDPMF543UJABCB5COV335ULZQPTCFM7M", false, "admin" });

            migrationBuilder.InsertData(
                table: "SYS_USER_ROLE",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4b400b20-3052-4fe0-ad0a-e32a9a87ed7b", "bbb5510c-59ef-4988-9ca7-4d9d40a1deb9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SYS_USER_ROLE",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4b400b20-3052-4fe0-ad0a-e32a9a87ed7b", "bbb5510c-59ef-4988-9ca7-4d9d40a1deb9" });

            migrationBuilder.DeleteData(
                table: "SYS_ROLE",
                keyColumn: "Id",
                keyValue: "4b400b20-3052-4fe0-ad0a-e32a9a87ed7b");

            migrationBuilder.DeleteData(
                table: "SYS_USER",
                keyColumn: "Id",
                keyValue: "bbb5510c-59ef-4988-9ca7-4d9d40a1deb9");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "SYS_USER",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);
        }
    }
}
