using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    create_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    update_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false),
                    permission_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    function_name_id = table.Column<int>(nullable: true),
                    action_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.permission_id);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    create_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    update_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false),
                    role_permission_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    role_id = table.Column<int>(nullable: true),
                    permission_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => x.role_permission_id);
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "permission_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<string>(maxLength: 36, nullable: false),
                    account_number = table.Column<string>(maxLength: 20, nullable: false),
                    password = table.Column<string>(maxLength: 100, nullable: false),
                    user_name = table.Column<string>(maxLength: 10, nullable: false),
                    role_id = table.Column<int>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    gender = table.Column<string>(maxLength: 1, nullable: false),
                    due_date = table.Column<DateTime>(nullable: false),
                    resignation_date = table.Column<DateTime>(nullable: true),
                    create_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "actions",
                columns: table => new
                {
                    create_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    update_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false),
                    action_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    action = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actions", x => x.action_id);
                    table.ForeignKey(
                        name: "FK_actions_users_create_user_id",
                        column: x => x.create_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_actions_users_update_user_id",
                        column: x => x.update_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "function_names",
                columns: table => new
                {
                    create_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    update_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false),
                    function_name_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    function_name = table.Column<string>(maxLength: 50, nullable: true),
                    function_name_chinese = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_function_names", x => x.function_name_id);
                    table.ForeignKey(
                        name: "FK_function_names_users_create_user_id",
                        column: x => x.create_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_function_names_users_update_user_id",
                        column: x => x.update_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    create_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    update_user_id = table.Column<string>(maxLength: 36, nullable: true),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: false),
                    role_id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    role = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                    table.ForeignKey(
                        name: "FK_roles_users_create_user_id",
                        column: x => x.create_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_roles_users_update_user_id",
                        column: x => x.update_user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actions_create_user_id",
                table: "actions",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_actions_update_user_id",
                table: "actions",
                column: "update_user_id");

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

            migrationBuilder.CreateIndex(
                name: "IX_function_names_create_user_id",
                table: "function_names",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_function_names_update_user_id",
                table: "function_names",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_action_id",
                table: "permissions",
                column: "action_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_create_user_id",
                table: "permissions",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_function_name_id",
                table: "permissions",
                column: "function_name_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_update_user_id",
                table: "permissions",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_create_user_id",
                table: "role_permissions",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_role_id",
                table: "role_permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_update_user_id",
                table: "role_permissions",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_create_user_id",
                table: "roles",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_update_user_id",
                table: "roles",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_account_number",
                table: "users",
                column: "account_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_users_create_user_id",
                table: "permissions",
                column: "create_user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_users_update_user_id",
                table: "permissions",
                column: "update_user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_actions_action_id",
                table: "permissions",
                column: "action_id",
                principalTable: "actions",
                principalColumn: "action_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_permissions_function_names_function_name_id",
                table: "permissions",
                column: "function_name_id",
                principalTable: "function_names",
                principalColumn: "function_name_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_users_create_user_id",
                table: "role_permissions",
                column: "create_user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_users_update_user_id",
                table: "role_permissions",
                column: "update_user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_role_permissions_roles_role_id",
                table: "role_permissions",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                table: "users",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roles_users_create_user_id",
                table: "roles");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_users_update_user_id",
                table: "roles");

            migrationBuilder.DropTable(
                name: "checkin_logs");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "actions");

            migrationBuilder.DropTable(
                name: "function_names");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
