using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class CheckinLogMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checkin_logs",
                columns: table => new
                {
                    create_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    update_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false),
                    checkin_log_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_id = table.Column<string>(nullable: true),
                    time = table.Column<DateTime>(nullable: false),
                    ip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkin_logs", x => x.checkin_log_id);
                    table.ForeignKey(
                        name: "FK_checkin_logs_users_create_user_id",
                        column: x => x.create_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkin_logs_users_update_user_id",
                        column: x => x.update_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkin_logs_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_checkin_logs_create_user_id",
                table: "checkin_logs",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_checkin_logs_update_user_id",
                table: "checkin_logs",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_checkin_logs_user_id",
                table: "checkin_logs",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkin_logs");
        }
    }
}
