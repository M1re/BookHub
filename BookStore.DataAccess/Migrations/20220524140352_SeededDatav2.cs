using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.DataAccess.Migrations
{
    public partial class SeededDatav2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e270b1f3-5780-40f1-817f-0852cca1b394",
                column: "ConcurrencyStamp",
                value: "9ecba644-fabf-4622-a5e3-ca71a782e4db");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edd63545-eb9f-46f0-bb37-e2e000cb4193",
                column: "ConcurrencyStamp",
                value: "90bf2f93-54f8-41e7-9e41-2e24a21c6bd0");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "818c63bc-e500-4815-97fc-ad7fa5a5ab35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc1e8ea6-faf0-4bb2-bfc9-f065c9701177", "AQAAAAEAACcQAAAAEN7itVEUhti19Xcxe2WFS/WX2N4iimRmXwZNl4JsZeg02z2JcbKYk8hxF+Rq5fMBew==", "083bb808-edf2-487c-a7b4-91fb0df12341" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd4b49ef-60ce-4091-a314-29a571c1bc99",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9ac857f-66a3-4566-baac-f1eb29025e93", "AQAAAAEAACcQAAAAENja6ISV5d38mbAl/n9pT0RG6aXPP1jP4kZAkmrGnigdVxJIg4qMiJd8rfTty/h44A==", "5733c26c-6d88-47ef-93a7-067be9e48cfb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e270b1f3-5780-40f1-817f-0852cca1b394",
                column: "ConcurrencyStamp",
                value: "d60e1f62-deae-4bc8-858b-1128a8267d4e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edd63545-eb9f-46f0-bb37-e2e000cb4193",
                column: "ConcurrencyStamp",
                value: "1a7f1166-0ba8-4037-a202-70232a4ee59b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "818c63bc-e500-4815-97fc-ad7fa5a5ab35",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bdd789c7-377e-4573-b546-83d7ab6b5742", "AQAAAAEAACcQAAAAEFLm9ADMHEEA7iFu1Fov3PvAS12jtSA2NuM5ne/lqxejFpQK0UHG+ciFkvJsbrJ4XQ==", "5bfbc20d-825a-4170-bcb1-97779509b27a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fd4b49ef-60ce-4091-a314-29a571c1bc99",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "573a3c9c-95e8-4a59-b75a-60af423de2fb", "AQAAAAEAACcQAAAAEP6TMnbBa/NRn/es+1AZLvJTeyg/RNLdn/bfTMRFbd6TdXVXKCHxVyKcweGyZIyjhQ==", "927fefdd-8ec4-48d2-aaf2-fc32f58fe49c" });
        }
    }
}
