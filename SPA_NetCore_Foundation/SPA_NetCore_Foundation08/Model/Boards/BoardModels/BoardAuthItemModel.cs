using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boards.Model
{
    /// <summary>
    /// 게시판 개별 권한 정보 아이템
    /// </summary>
    public class BoardAuthItemModel
    {
        /// <summary>
        /// 게시판 권한 고유 아이디
        /// </summary>
        public long idBoardAuthority { get; set; }

        /// <summary>
        /// 연결된 게시판의 고유번호
        /// </summary>
        public long idBoard { get; set; }

        /// <summary>
        /// 소유 유저
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 가지고 있는 권한.
        /// </summary>
        public BoardAuthorityType Authority { get; set; }
        /// <summary>
        /// 권한 상태
        /// </summary>
        public BoardAuthorityStateType AuthState { get; set; }

        /// <summary>
        /// 사인인 이메일
        /// </summary>
        public string SignEmail { get; set; }
        /// <summary>
        /// 소유 유저 이름
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 설명
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 수정 날짜
        /// </summary>
        public DateTime EditDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BoardAuthItemModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baData"></param>
        public BoardAuthItemModel(BoardAuthority baData)
        {
            this.idBoardAuthority = baData.idBoardAuthority;
            this.idBoard = baData.idBoard;
            this.idUser = baData.idUser;
            this.Authority = baData.Authority;
            this.AuthState = baData.AuthState;

            this.SignEmail = string.Empty;
            this.UserName = string.Empty;

            this.Memo = baData.Memo;
            this.EditDate = baData.EditDate;
            
        }

    }
}
