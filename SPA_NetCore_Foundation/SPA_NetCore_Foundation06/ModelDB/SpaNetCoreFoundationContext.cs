using System;
using Microsoft.EntityFrameworkCore;


namespace SPA_NetCore_Foundation.ModelDB
{
    public class SpaNetCoreFoundationContext : DbContext
    {
        public SpaNetCoreFoundationContext(DbContextOptions options)
            : base(options)
        {
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
