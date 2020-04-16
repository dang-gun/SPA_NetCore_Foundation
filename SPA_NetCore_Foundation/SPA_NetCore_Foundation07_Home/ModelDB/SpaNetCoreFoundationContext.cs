using System;
using Microsoft.EntityFrameworkCore;
using SPA_NetCore_Foundation.Global;

namespace ModelDB
{
    public class SpaNetCoreFoundationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
            switch (GlobalStatic.DBType)
            {
                case "sqlite":
                    options.UseSqlite(GlobalStatic.DBString);
                    break;
                case "mysql":
                    //options.UseSqlite(GlobalStatic.DBString);
                    break;

                case "mssql":
                default:
                    options.UseSqlServer(GlobalStatic.DBString);
                    break;
            }
        }

        public DbSet<Setting_Data> Setting_Data { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserSignIn> UserSignIn { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //서버 기본 세팅 정보************************************
            modelBuilder.Entity<Setting_Data>().HasData(
                new Setting_Data
                { 
                    idSetting_Data = 1
                    , Number = 1
                    , Name = "Title"
                    , ValueData = "ASP.NET Core SPA Foundation"
                    , Description = "프로그램 전체에 표시될 이름"
                }
                , new Setting_Data
                {
                    idSetting_Data = 2
                    , Number = 2
                    , Name = "ConnectionAllow"
                    , ValueData = "true"
                    , Description = "접속 허용 여부"
                }
                , new Setting_Data
                {
                    idSetting_Data = 3
                    , Number = 3
                    , Name = "TestType"
                    , ValueData = "0"
                    , Description = "테스트용 타입. TestType 사용"
                });

            //유저************************************
            modelBuilder.Entity<User>().HasData(
            new User
            { 
                idUser = 1
                , SignEmail = "test01@email.net"
                , Password = "1111"
            }
            , new User
            { 
                idUser = 2
                , SignEmail = "test02@email.net"
                , Password = "1111"
            }
            , new User
            { 
                idUser = 3
                , SignEmail = "testuser@email.net"
                , Password = "1111"
            }
            , new User
            { 
                idUser = 4
                , SignEmail = "user@email.net"
                , Password = "1111"
            });

            //유저 정보************************************
            modelBuilder.Entity<UserInfo>().HasData(
            new UserInfo
            {
                idUserInfo = 1
                , idUser = 1
                , ViewName = "root"
                , ManagerPermission = ManagerPermissionType.All
            }
            , new UserInfo
            {
                idUserInfo = 2
                , idUser = 2
                , ViewName = "admin"
                , ManagerPermission = ManagerPermissionType.Admin
            }
            , new UserInfo
            {
                idUserInfo = 3
                , idUser = 3
                , ViewName = "test User"
                , ManagerPermission = ManagerPermissionType.TestUser
            }
            , new UserInfo
            {
                idUserInfo = 4
                , idUser = 4
                , ViewName = "User"
                , ManagerPermission = ManagerPermissionType.None
            });
        }
    }
}
