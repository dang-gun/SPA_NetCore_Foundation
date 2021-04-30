using System;
using BoardModel;
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

        #region Board
        /// <summary>
        /// 게시판 정보
        /// </summary>
        public DbSet<Board> Board { get; set; }
        /// <summary>
        /// 게시물
        /// </summary>
        public DbSet<BoardPost> BoardPost { get; set; }
        /// <summary>
        /// 게시판 개별권한
        /// </summary>
        public DbSet<BoardAuthority> BoardAuthority { get; set; }
        /// <summary>
        /// 게시판 카테고리 정보
        /// </summary>
        public DbSet<BoardCategory> BoardCategory { get; set; }
        /// <summary>
        /// 게시물 내용
        /// </summary>
        public DbSet<BoardContent> BoardContent { get; set; }
        /// <summary>
        /// 게시판 그룹
        /// </summary>
        public DbSet<BoardGroup> BoardGroup { get; set; }
        /// <summary>
        /// 게시물 댓글
        /// </summary>
        public DbSet<BoardPostReply> BoardPostReply { get; set; }
        #endregion

        #region File
        /// <summary>
        /// 파일 정보
        /// </summary>
        public DbSet<FileData> FileData { get; set; }
        #endregion

        #region 유저 관련
        /// <summary>
        /// 유저 사인인 정보
        /// </summary>
        public DbSet<User> User { get; set; }
        /// <summary>
        /// 유저 API 정보
        /// </summary>
        public DbSet<UserApi> UserApi { get; set; }
        /// <summary>
        /// 유저 정보중 자주쓰는 정보
        /// </summary>
        public DbSet<UserInfo> UserInfo { get; set; }
        /// <summary>
        /// 사용자 사인인 리스트
        /// </summary>
        public DbSet<UserSignIn> UserSignIn { get; set; }

        /// <summary>
        /// 사인 관련 로그
        /// </summary>
        public DbSet<UserSignLog> UserSignLog { get; set; }
        #endregion


        /// <summary>
        /// api 호출 로그
        /// </summary>
        public DbSet<ApiLog> ApiLog { get; set; }

        /// <summary>
        /// 서버 세팅 정보
        /// </summary>
        public DbSet<Setting_Data> Setting_Data { get; set; }



        /// <summary>
        /// 게시판 댓글 관계 검색 모델
        /// </summary>
        public DbSet<BoardPostReplyRelationTreeModel> BoardPostReplyRelationTreeModels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //서버 기본 세팅 정보************************************
            modelBuilder.Entity<Setting_Data>().HasData(
                new Setting_Data
                {
                    idSetting_Data = 1,
                    Number = 1,
                    Name = "Title",
                    OpenType = Setting_DataOpenType.Public,
                    ValueData = "ASP.NET Core SPA Foundation 08",
                    Description = "프로그램 전체에 표시될 이름",
                }
                , new Setting_Data
                {
                    idSetting_Data = 2,
                    Number = 2,
                    Name = "SignLog",
                    OpenType = Setting_DataOpenType.Private,
                    ValueData = "0",
                    Description = "사인 관련 로그를 어떻게 남기는 레벨.(높을수록 많은 정보를 남긴다.== DB부하가 심해짐)",
                });

            #region 유저 정보
            //유저************************************
            modelBuilder.Entity<User>().HasData(
                new User
                { 
                    idUser = 1,
                    SignEmail = "root",
                    Password = "1111"
                }
                , new User
                { 
                    idUser = 2,
                    SignEmail = "admin",
                    Password = "1111"
                }
                , new User
                {
                    idUser = 3,
                    SignEmail = "test01@email.net",
                    Password = "1111"
                }
                , new User
                {
                    idUser = 4,
                    SignEmail = "test02@email.net",
                    Password = "1111"
                });

            //유저 정보************************************
            modelBuilder.Entity<UserInfo>().HasData(
                new UserInfo
                { 
                    idUserInfo = 1,
                    idUser = 1,
                    ViewName = "root",
                    MgtClass = ManagementClassType.Root,
                }
                , new UserInfo
                {
                    idUserInfo = 2,
                    idUser = 2,
                    ViewName = "admin",
                    MgtClass = ManagementClassType.Admin,
                }
                , new UserInfo
                {
                    idUserInfo = 3,
                    idUser = 3,
                    ViewName = "테스트01",
                    MgtClass = ManagementClassType.User,
                }
                , new UserInfo
                {
                    idUserInfo = 4,
                    idUser = 4,
                    ViewName = "테스트02",
                    MgtClass = ManagementClassType.User,
                });
            #endregion

            #region 게시판 기본 정보
            //카테고리 정보************************************
            modelBuilder.Entity<BoardCategory>().HasData(
            new BoardCategory
            {
                idBoardCategory = 1,
                idBoard = 1,
                Title = "All",
                Memo = "전체 게시판에 표시되는 분류."
            });
            //보드 정보************************************
            modelBuilder.Entity<Board>().HasData(
            new Board
            {
                idBoard = 1,
                Title = "Test",
                BoardState = BoardStateType.Use,
                CreateDate = DateTime.Now,
                AuthorityDefault
                    = BoardAuthorityType.None
                        | BoardAuthorityType.ReadList
                        | BoardAuthorityType.Read
                        | BoardAuthorityType.Write
                        | BoardAuthorityType.WriteReply
                        | BoardAuthorityType.Delete
                        | BoardAuthorityType.Edit,
                BoardFaculty = BoardFacultyType.ShowCount_Server,
                ShowCount = 10,
                Memo = "테스트용 게시판"
            });
            //기본 게시물 입력*********************************
            modelBuilder.Entity<BoardPost>().HasData(
            new BoardPost
            {
                idBoardPost = 1,
                idBoard = 1,
                idBoardCategory = 0,
                Title = "DB 생성후 테스트용 자동생성 게시물입니다.",
                idUser = 1,
                ViewCount = 0,
                WriteDate = DateTime.Now
            });
            //기본 게시물 입력*********************************
            modelBuilder.Entity<BoardContent>().HasData(
            new BoardContent
            {
                idBoardContent = 1,
                idBoard = 1,
                idBoardPost = 1,
                Content = "DB 생성후 테스트용 자동생성 게시물입니다.<br />내용",
                WriteIP = "",
                EditIP = ""
            });
            //기본 게시물의 댓글 입력*********************************
            modelBuilder.Entity<BoardPostReply>().HasData(
            new BoardPostReply
            {
                idBoardPostReply = 1,
                idBoard = 1,
                Content = "DB 생성후 테스트용 자동생성 게시물의 댓글입니다.",
                WriteIP = "",
                EditIP = ""
            });
            //테스트 게시판에 루트유저 모든 권한 부여*********************************
            modelBuilder.Entity<BoardAuthority>().HasData(
            new BoardAuthority
            {
                idBoardAuthority = 1,
                idBoard = 1,
                idUser = 1,
                Authority = BoardAuthorityType.All
            });
            #endregion
        }
    }
}
