using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.DataAccess.Migrations
{
    public partial class SeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e270b1f3-5780-40f1-817f-0852cca1b394", "d60e1f62-deae-4bc8-858b-1128a8267d4e", "Admin", "ADMIN" },
                    { "edd63545-eb9f-46f0-bb37-e2e000cb4193", "1a7f1166-0ba8-4037-a202-70232a4ee59b", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "818c63bc-e500-4815-97fc-ad7fa5a5ab35", 0, "bdd789c7-377e-4573-b546-83d7ab6b5742", "admin@bookstore.com", false, false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEFLm9ADMHEEA7iFu1Fov3PvAS12jtSA2NuM5ne/lqxejFpQK0UHG+ciFkvJsbrJ4XQ==", null, false, "5bfbc20d-825a-4170-bcb1-97779509b27a", false, "admin@bookstore.com" },
                    { "fd4b49ef-60ce-4091-a314-29a571c1bc99", 0, "573a3c9c-95e8-4a59-b75a-60af423de2fb", "user@bookstore.com", false, false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEP6TMnbBa/NRn/es+1AZLvJTeyg/RNLdn/bfTMRFbd6TdXVXKCHxVyKcweGyZIyjhQ==", null, false, "927fefdd-8ec4-48d2-aaf2-fc32f58fe49c", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e270b1f3-5780-40f1-817f-0852cca1b394", "818c63bc-e500-4815-97fc-ad7fa5a5ab35" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "edd63545-eb9f-46f0-bb37-e2e000cb4193", "fd4b49ef-60ce-4091-a314-29a571c1bc99" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e270b1f3-5780-40f1-817f-0852cca1b394", "818c63bc-e500-4815-97fc-ad7fa5a5ab35" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "edd63545-eb9f-46f0-bb37-e2e000cb4193", "fd4b49ef-60ce-4091-a314-29a571c1bc99" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e270b1f3-5780-40f1-817f-0852cca1b394");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edd63545-eb9f-46f0-bb37-e2e000cb4193");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "818c63bc-e500-4815-97fc-ad7fa5a5ab35");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd4b49ef-60ce-4091-a314-29a571c1bc99");
        }
    }
}
