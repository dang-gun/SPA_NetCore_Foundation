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
    [DbContext(typeof(SpaNetCoreFoundationContext))]
    [Migration("20191204175145_UserSignIn수정2")]
    partial class UserSignIn수정2
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
                            SignEmail = "test01@email.net"
                        },
                        new
                        {
                            idUser = 2L,
                            Password = "1111",
                            SignEmail = "test02@email.net"
                        });
                });

            modelBuilder.Entity("SPA_NetCore_Foundation.ModelDB.UserSignIn", b =>
                {
                    b.Property<long>("idUserSignIn")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RefreshToken");

                    b.Property<long?>("idUserForeignKey");

                    b.HasKey("idUserSignIn");

                    b.HasIndex("idUserForeignKey");

                    b.ToTable("UserSignIn");
                });

            modelBuilder.Entity("SPA_NetCore_Foundation.ModelDB.UserSignIn", b =>
                {
                    b.HasOne("SPA_NetCore_Foundation.ModelDB.User", "idUser")
                        .WithMany()
                        .HasForeignKey("idUserForeignKey");
                });
#pragma warning restore 612, 618
        }
    }
}
