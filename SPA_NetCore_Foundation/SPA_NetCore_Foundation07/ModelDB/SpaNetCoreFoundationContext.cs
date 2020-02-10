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

        public DbSet<User> User { get; set; }
        public DbSet<UserSignIn> UserSignIn { get; set; }


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
