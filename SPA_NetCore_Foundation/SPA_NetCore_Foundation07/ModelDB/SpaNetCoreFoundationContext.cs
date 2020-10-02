using System;
using Microsoft.EntityFrameworkCore;
using SPA_NetCore_Foundation.Global;

namespace ModelDB
{
    /// <summary>
    /// 
    /// </summary>
    public class SpaNetCoreFoundationContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
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

        /// <summary>
        /// 유저 사인인 정보
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 유저 정보중 자주쓰는 정보
        /// </summary>
        public DbSet<UserInfo> UserInfo { get; set; }
        /// <summary>
        /// 사용자 사인인 리스트
        /// </summary>
        public DbSet<UserSignIn> UserSignIn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            { 
                idUser = 1
                , SignEmail = "test01@email.net"
                , Password = "1111"
            }
            , new User
            { 
                idUser = 2
                ,
                SignEmail = "test02@email.net"
                , Password = "1111"
            });
        }
    }
}
