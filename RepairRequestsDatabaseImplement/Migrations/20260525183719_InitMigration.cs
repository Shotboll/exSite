using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RepairRequestsDatabaseImplement.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DeviceTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairRequests_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepairRequestServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RepairRequestId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairRequestServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepairRequestServices_RepairRequests_RepairRequestId",
                        column: x => x.RepairRequestId,
                        principalTable: "RepairRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepairRequestServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeviceTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Портативный компьютер", "Ноутбук" },
                    { 2, "Мобильный телефон", "Смартфон" },
                    { 3, "Планшетный компьютер", "Планшет" },
                    { 4, "Устройство печати", "Принтер" },
                    { 5, "Устройство отображения информации", "Монитор" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Первичная диагностика неисправности", "Диагностика", 500m },
                    { 2, "Замена поврежденного экрана устройства", "Замена экрана", 3500m },
                    { 3, "Разборка, чистка и замена термопасты", "Чистка системы охлаждения", 1500m },
                    { 4, "Установка и первичная настройка ОС", "Установка операционной системы", 2000m },
                    { 5, "Замена изношенного аккумулятора", "Замена аккумулятора", 2500m },
                    { 6, "Восстановление или замена разъема питания", "Ремонт разъема питания", 1800m },
                    { 7, "Установка и настройка пользовательских программ", "Настройка программ", 1200m },
                    { 8, "Попытка восстановления пользовательских файлов", "Восстановление данных", 4000m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "admin", "Администратор", "admin", 1 },
                    { 2, "user1", "Иван Петров", "user1", 0 },
                    { 3, "user2", "Анна Смирнова", "user2", 0 }
                });

            migrationBuilder.InsertData(
                table: "RepairRequests",
                columns: new[] { "Id", "CreatedDate", "Description", "DeviceTypeId", "Status", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Ноутбук перегревается и выключается при нагрузке", 1, 0, "Ноутбук сильно шумит", 2 },
                    { 2, new DateTime(2026, 5, 2, 12, 30, 0, 0, DateTimeKind.Utc), "После падения появились трещины на экране", 2, 1, "Разбит экран смартфона", 2 },
                    { 3, new DateTime(2026, 5, 3, 9, 15, 0, 0, DateTimeKind.Utc), "Планшет не реагирует на кнопку питания", 3, 0, "Не включается планшет", 3 },
                    { 4, new DateTime(2026, 5, 4, 14, 40, 0, 0, DateTimeKind.Utc), "Принтер подключен, но задания не выводятся на печать", 4, 2, "Принтер не печатает", 3 },
                    { 5, new DateTime(2026, 5, 5, 16, 10, 0, 0, DateTimeKind.Utc), "Экран периодически гаснет на несколько секунд", 5, 3, "Монитор мигает", 2 }
                });

            migrationBuilder.InsertData(
                table: "RepairRequestServices",
                columns: new[] { "Id", "RepairRequestId", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 3 },
                    { 3, 2, 1 },
                    { 4, 2, 2 },
                    { 5, 3, 1 },
                    { 6, 3, 5 },
                    { 7, 4, 1 },
                    { 8, 4, 7 },
                    { 9, 5, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_DeviceTypeId",
                table: "RepairRequests",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_UserId",
                table: "RepairRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestServices_RepairRequestId",
                table: "RepairRequestServices",
                column: "RepairRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequestServices_ServiceId",
                table: "RepairRequestServices",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepairRequestServices");

            migrationBuilder.DropTable(
                name: "RepairRequests");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
