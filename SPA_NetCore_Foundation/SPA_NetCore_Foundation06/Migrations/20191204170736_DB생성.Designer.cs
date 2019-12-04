﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModelDB;

namespace SPA_NetCore_Foundation06.Migrations
{
    [DbContext(typeof(SpaNetCoreFoundationContext))]
    [Migration("20191204170736_DB생성")]
    partial class DB생성
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SPA_NetCore_Foundation.ModelDB.User", b =>
                {
                    b.Property<long>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password");

                    b.Property<string>("SignEmail");

                    b.HasKey("idUser");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            idUser = 1L,
                            Password = "1111",
                            SignEmail = "test01@test.com"
                        },
                        new
                        {
                            idUser = 2L,
                            Password = "1111",
                            SignEmail = "test02@test.com"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
