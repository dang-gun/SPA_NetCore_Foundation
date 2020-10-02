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
        public DbSet<FileInfo> FileInfo { get; set; }
        #endregion

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
