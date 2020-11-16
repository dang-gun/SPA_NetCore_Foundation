﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModelDB;

namespace SPA_NetCore_Foundation08.Migrations
{
    [DbContext(typeof(SpaNetCoreFoundationContext))]
    partial class SpaNetCoreFoundationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoardModel.BoardPostReplyRelationTreeModel", b =>
                {
                    b.Property<long>("idBoardPostReply")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Depth")
                        .HasColumnType("int");

                    b.Property<long>("idBoardPostReply_Re")
                        .HasColumnType("bigint");

                    b.HasKey("idBoardPostReply");

                    b.ToTable("BoardPostReplyRelationTreeModels");
                });

            modelBuilder.Entity("ModelDB.ApiLog", b =>
                {
                    b.Property<long>("idApiLog")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CallDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Contents")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Step01")
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<long>("idUser")
                        .HasColumnType("bigint");

                    b.HasKey("idApiLog");

                    b.ToTable("ApiLog");
                });

            modelBuilder.Entity("ModelDB.Board", b =>
                {
                    b.Property<long>("idBoard")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorityDefault")
                        .HasColumnType("int");

                    b.Property<int>("BoardFaculty")
                        .HasColumnType("int");

                    b.Property<int>("BoardState")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Memo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("ShowCount")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("UrlStandard")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<long>("idBoardGroup")
                        .HasColumnType("bigint");

                    b.HasKey("idBoard");

                    b.ToTable("Board");

                    b.HasData(
                        new
                        {
                            idBoard = 1L,
                            AuthorityDefault = 0,
                            BoardFaculty = 0,
                            BoardState = 1,
                            CreateDate = new DateTime(2020, 11, 17, 5, 20, 50, 962, DateTimeKind.Local).AddTicks(4518),
                            Memo = "테스트용 게시판",
                            ShowCount = (short)0,
                            Title = "Test",
                            idBoardGroup = 0L
                        });
                });

            modelBuilder.Entity("ModelDB.BoardAuthority", b =>
                {
                    b.Property<long>("idBoardAuthority")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthState")
                        .HasColumnType("int");

                    b.Property<int>("Authority")
                        .HasColumnType("int");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Memo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("idBoard")
                        .HasColumnType("bigint");

                    b.Property<long>("idUser")
                        .HasColumnType("bigint");

                    b.HasKey("idBoardAuthority");

                    b.ToTable("BoardAuthority");

                    b.HasData(
                        new
                        {
                            idBoardAuthority = 1L,
                            AuthState = 0,
                            Authority = 2147483647,
                            EditDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            idBoard = 1L,
                            idUser = 1L
                        });
                });

            modelBuilder.Entity("ModelDB.BoardCategory", b =>
                {
                    b.Property<long>("idBoardCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Memo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<long>("idBoard")
                        .HasColumnType("bigint");

                    b.HasKey("idBoardCategory");

                    b.ToTable("BoardCategory");

                    b.HasData(
                        new
                        {
                            idBoardCategory = 1L,
                            Memo = "전체 게시판에 표시되는 분류.",
                            Title = "All",
                            idBoard = 1L
                        });
                });

            modelBuilder.Entity("ModelDB.BoardContent", b =>
                {
                    b.Property<long>("idBoardContent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EditIP")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("FileList")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WriteIP")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<long>("idBoard")
                        .HasColumnType("bigint");

                    b.Property<long>("idBoardPost")
                        .HasColumnType("bigint");

                    b.HasKey("idBoardContent");

                    b.ToTable("BoardContent");

                    b.HasData(
                        new
                        {
                            idBoardContent = 1L,
                            Content = "DB 생성후 테스트용 자동생성 게시물입니다.<br />내용",
                            EditIP = "",
                            WriteIP = "",
                            idBoard = 1L,
                            idBoardPost = 1L
                        });
                });

            modelBuilder.Entity("ModelDB.BoardGroup", b =>
                {
                    b.Property<long>("idBoardGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Memo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("idBoardGroup");

                    b.ToTable("BoardGroup");
                });

            modelBuilder.Entity("ModelDB.BoardPost", b =>
                {
                    b.Property<long>("idBoardPost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EditIP")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("PostState")
                        .HasColumnType("int");

                    b.Property<int>("ReplyCount")
                        .HasColumnType("int");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<long>("ViewCount")
                        .HasColumnType("bigint");

                    b.Property<long>("ViewCountNone")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("WriteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WriteIP")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<long>("idBoard")
                        .HasColumnType("bigint");

                    b.Property<long>("idBoardCategory")
                        .HasColumnType("bigint");

                    b.Property<long>("idUser")
                        .HasColumnType("bigint");

                    b.HasKey("idBoardPost");

                    b.ToTable("BoardPost");

                    b.HasData(
                        new
                        {
                            idBoardPost = 1L,
                            DeleteDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EditDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PostState = 0,
                            ReplyCount = 0,
                            Title = "DB 생성후 테스트용 자동생성 게시물입니다.",
                            ViewCount = 0L,
                            ViewCountNone = 0L,
                            WriteDate = new DateTime(2020, 11, 17, 5, 20, 50, 963, DateTimeKind.Local).AddTicks(6284),
                            idBoard = 1L,
                            idBoardCategory = 0L,
                            idUser = 1L
                        });
                });

            modelBuilder.Entity("ModelDB.BoardPostReply", b =>
                {
                    b.Property<long>("idBoardPostReply")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EditIP")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("ReReplyCount")
                        .HasColumnType("int");

                    b.Property<int>("ReplyState")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WriteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WriteIP")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<long>("idBoard")
                        .HasColumnType("bigint");

                    b.Property<long>("idBoardPost")
                        .HasColumnType("bigint");

                    b.Property<long>("idBoardPostReply_Re")
                        .HasColumnType("bigint");

                    b.Property<long>("idBoardPostReply_ReParent")
                        .HasColumnType("bigint");

                    b.Property<long>("idUser")
                        .HasColumnType("bigint");

                    b.HasKey("idBoardPostReply");

                    b.ToTable("BoardPostReply");

                    b.HasData(
                        new
                        {
                            idBoardPostReply = 1L,
                            Content = "DB 생성후 테스트용 자동생성 게시물의 댓글입니다.",
                            DeleteDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EditDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EditIP = "",
                            ReReplyCount = 0,
                            ReplyState = 0,
                            WriteDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            WriteIP = "",
                            idBoard = 1L,
                            idBoardPost = 0L,
                            idBoardPostReply_Re = 0L,
                            idBoardPostReply_ReParent = 0L,
                            idUser = 0L
                        });
                });

            modelBuilder.Entity("ModelDB.FileData", b =>
                {
                    b.Property<long>("idFileList")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("EditorDivision")
                        .HasColumnType("nvarchar(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Ext")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileDir")
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FileState")
                        .HasColumnType("int");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("idFileList");

                    b.ToTable("FileData");
                });

            modelBuilder.Entity("ModelDB.Setting_Data", b =>
                {
                    b.Property<long>("idSetting_Data")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("ValueData")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idSetting_Data");

                    b.ToTable("Setting_Data");

                    b.HasData(
                        new
                        {
                            idSetting_Data = 1L,
                            Description = "프로그램 전체에 표시될 이름",
                            Name = "Title",
                            Number = 1,
                            ValueData = "ASP.NET Core SPA Foundation 08"
                        });
                });

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
                            SignEmail = "root"
                        },
                        new
                        {
                            idUser = 2L,
                            Password = "1111",
                            SignEmail = "admin"
                        },
                        new
                        {
                            idUser = 3L,
                            Password = "1111",
                            SignEmail = "test01@email.net"
                        },
                        new
                        {
                            idUser = 4L,
                            Password = "1111",
                            SignEmail = "test02@email.net"
                        });
                });

            modelBuilder.Entity("ModelDB.UserApi", b =>
                {
                    b.Property<long>("idUserApi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApiKey")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserApiState")
                        .HasColumnType("int");

                    b.Property<long>("idUser")
                        .HasColumnType("bigint");

                    b.HasKey("idUserApi");

                    b.ToTable("UserApi");
                });

            modelBuilder.Entity("ModelDB.UserInfo", b =>
                {
                    b.Property<long>("idUserInfo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MgtClass")
                        .HasColumnType("int");

                    b.Property<string>("PlatformInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SignInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SignUpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ViewName")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<long>("idUser")
                        .HasColumnType("bigint");

                    b.Property<long>("idUser_Parent")
                        .HasColumnType("bigint");

                    b.HasKey("idUserInfo");

                    b.ToTable("UserInfo");

                    b.HasData(
                        new
                        {
                            idUserInfo = 1L,
                            MgtClass = 1,
                            RefreshDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SignInDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SignUpDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ViewName = "root",
                            idUser = 1L,
                            idUser_Parent = 0L
                        },
                        new
                        {
                            idUserInfo = 2L,
                            MgtClass = 10000,
                            RefreshDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SignInDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SignUpDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ViewName = "admin",
                            idUser = 2L,
                            idUser_Parent = 0L
                        },
                        new
                        {
                            idUserInfo = 3L,
                            MgtClass = 1000000,
                            RefreshDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SignInDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SignUpDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ViewName = "테스트01",
                            idUser = 3L,
                            idUser_Parent = 0L
                        },
                        new
                        {
                            idUserInfo = 4L,
                            MgtClass = 1000000,
                            RefreshDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SignInDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SignUpDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ViewName = "테스트02",
                            idUser = 4L,
                            idUser_Parent = 0L
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
