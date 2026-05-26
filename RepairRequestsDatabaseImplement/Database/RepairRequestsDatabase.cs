using Microsoft.EntityFrameworkCore;
using RepairRequestsBusinessLogic.Services;
using RepairRequestsDatabaseImplement.Models;
using RepairRequestsDataModels.Enums;

namespace RepairRequestsDatabaseImplement.Database
{
    public class RepairRequestsDatabase : DbContext
    {
        public RepairRequestsDatabase(DbContextOptions<RepairRequestsDatabase> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<DeviceType> DeviceTypes { get; set; }

        public virtual DbSet<RepairRequest> RepairRequests { get; set; }

        public virtual DbSet<Service> Services { get; set; }

        public virtual DbSet<RepairRequestService> RepairRequestServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Login = "admin",
                    PasswordHash = PasswordService.getHash("admin"),
                    Name = "Администратор",
                    Role = UserRole.Администратор
                },
                new User
                {
                    Id = 2,
                    Login = "user1",
                    PasswordHash = PasswordService.getHash("user1"),
                    Name = "Иван Петров",
                    Role = UserRole.Пользователь
                },
                new User
                {
                    Id = 3,
                    Login = "user2",
                    PasswordHash = PasswordService.getHash("user2"),
                    Name = "Анна Смирнова",
                    Role = UserRole.Пользователь
                }
            );

            modelBuilder.Entity<DeviceType>().HasData(
                new DeviceType
                {
                    Id = 1,
                    Name = "Ноутбук",
                    Description = "Портативный компьютер"
                },
                new DeviceType
                {
                    Id = 2,
                    Name = "Смартфон",
                    Description = "Мобильный телефон"
                },
                new DeviceType
                {
                    Id = 3,
                    Name = "Планшет",
                    Description = "Планшетный компьютер"
                },
                new DeviceType
                {
                    Id = 4,
                    Name = "Принтер",
                    Description = "Устройство печати"
                },
                new DeviceType
                {
                    Id = 5,
                    Name = "Монитор",
                    Description = "Устройство отображения информации"
                }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service
                {
                    Id = 1,
                    Name = "Диагностика",
                    Description = "Первичная диагностика неисправности",
                    Price = 500
                },
                new Service
                {
                    Id = 2,
                    Name = "Замена экрана",
                    Description = "Замена поврежденного экрана устройства",
                    Price = 3500
                },
                new Service
                {
                    Id = 3,
                    Name = "Чистка системы охлаждения",
                    Description = "Разборка, чистка и замена термопасты",
                    Price = 1500
                },
                new Service
                {
                    Id = 4,
                    Name = "Установка операционной системы",
                    Description = "Установка и первичная настройка ОС",
                    Price = 2000
                },
                new Service
                {
                    Id = 5,
                    Name = "Замена аккумулятора",
                    Description = "Замена изношенного аккумулятора",
                    Price = 2500
                },
                new Service
                {
                    Id = 6,
                    Name = "Ремонт разъема питания",
                    Description = "Восстановление или замена разъема питания",
                    Price = 1800
                },
                new Service
                {
                    Id = 7,
                    Name = "Настройка программ",
                    Description = "Установка и настройка пользовательских программ",
                    Price = 1200
                },
                new Service
                {
                    Id = 8,
                    Name = "Восстановление данных",
                    Description = "Попытка восстановления пользовательских файлов",
                    Price = 4000
                }
            );

            modelBuilder.Entity<RepairRequest>().HasData(
                new RepairRequest
                {
                    Id = 1,
                    Title = "Ноутбук сильно шумит",
                    Description = "Ноутбук перегревается и выключается при нагрузке",
                    CreatedDate = new DateTime(2026, 5, 1, 10, 0, 0, DateTimeKind.Utc),
                    Status = RequestStatus.Новая,
                    UserId = 2,
                    DeviceTypeId = 1
                },
                new RepairRequest
                {
                    Id = 2,
                    Title = "Разбит экран смартфона",
                    Description = "После падения появились трещины на экране",
                    CreatedDate = new DateTime(2026, 5, 2, 12, 30, 0, DateTimeKind.Utc),
                    Status = RequestStatus.ВРаботе,
                    UserId = 2,
                    DeviceTypeId = 2
                },
                new RepairRequest
                {
                    Id = 3,
                    Title = "Не включается планшет",
                    Description = "Планшет не реагирует на кнопку питания",
                    CreatedDate = new DateTime(2026, 5, 3, 9, 15, 0, DateTimeKind.Utc),
                    Status = RequestStatus.Новая,
                    UserId = 3,
                    DeviceTypeId = 3
                },
                new RepairRequest
                {
                    Id = 4,
                    Title = "Принтер не печатает",
                    Description = "Принтер подключен, но задания не выводятся на печать",
                    CreatedDate = new DateTime(2026, 5, 4, 14, 40, 0, DateTimeKind.Utc),
                    Status = RequestStatus.Завершена,
                    UserId = 3,
                    DeviceTypeId = 4
                },
                new RepairRequest
                {
                    Id = 5,
                    Title = "Монитор мигает",
                    Description = "Экран периодически гаснет на несколько секунд",
                    CreatedDate = new DateTime(2026, 5, 5, 16, 10, 0, DateTimeKind.Utc),
                    Status = RequestStatus.Отменена,
                    UserId = 2,
                    DeviceTypeId = 5
                }
            );

            modelBuilder.Entity<RepairRequestService>().HasData(
                new RepairRequestService
                {
                    Id = 1,
                    RepairRequestId = 1,
                    ServiceId = 1
                },
                new RepairRequestService
                {
                    Id = 2,
                    RepairRequestId = 1,
                    ServiceId = 3
                },
                new RepairRequestService
                {
                    Id = 3,
                    RepairRequestId = 2,
                    ServiceId = 1
                },
                new RepairRequestService
                {
                    Id = 4,
                    RepairRequestId = 2,
                    ServiceId = 2
                },
                new RepairRequestService
                {
                    Id = 5,
                    RepairRequestId = 3,
                    ServiceId = 1
                },
                new RepairRequestService
                {
                    Id = 6,
                    RepairRequestId = 3,
                    ServiceId = 5
                },
                new RepairRequestService
                {
                    Id = 7,
                    RepairRequestId = 4,
                    ServiceId = 1
                },
                new RepairRequestService
                {
                    Id = 8,
                    RepairRequestId = 4,
                    ServiceId = 7
                },
                new RepairRequestService
                {
                    Id = 9,
                    RepairRequestId = 5,
                    ServiceId = 1
                }
            );
        }
    }
}