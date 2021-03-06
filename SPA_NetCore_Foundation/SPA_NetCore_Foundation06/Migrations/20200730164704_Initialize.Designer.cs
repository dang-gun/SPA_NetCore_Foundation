﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModelDB;

namespace SPA_NetCore_Foundation06.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    [DbContext(typeof(SpaNetCoreFoundationContext))]
    [Migration("20200730164704_Initialize")]
    partial class Initialize
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ModelDB.User", b =>
                {
                    b.Property<long>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignEmail")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idUser");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            idUser = 1L,
                            Password = "1111",
                            SignEmail = "test01@email.net"
                        },
                        new
                        {
                            idUser = 2L,
                            Password = "1111",
                            SignEmail = "test02@email.net"
                        });
                });

            modelBuilder.Entity("ModelDB.UserSignIn", b =>
                {
                    b.Property<long>("idUserSignIn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("RefreshDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SignInDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("idUser")
                        .HasColumnType("bigint");

                    b.HasKey("idUserSignIn");

                    b.ToTable("UserSignIn");
                });
#pragma warning restore 612, 618
        }
    }
}
