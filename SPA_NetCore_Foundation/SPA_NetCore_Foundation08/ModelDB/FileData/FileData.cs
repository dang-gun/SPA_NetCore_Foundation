using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 파일 
    /// </summary>
    public class FileData
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idFileList { get; set; }

        /// <summary>
        /// 원래 파일 이름
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 확장자
        /// </summary>
        public string Ext { get; set; }
        /// <summary>
        /// 파일 크기
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 서버 로컬의 파일 위치
        /// </summary>
        [MaxLength(512)]
        public string FileDir { get; set; }
        /// <summary>
        /// 저정되어 있는 파일의 Url
        /// </summary>
        [MaxLength(512)]
        public string FileUrl { get; set; }

        /// <summary>
        /// 파일에 대한 설명
        /// </summary>
        [MaxLength(1024)]
        public string Description { get; set; }

        /// <summary>
        /// 에디터에서 파일을 구분하기위해 사용한 구분값.
        /// 에디터가 아닌곳에서는 아직 사용하지 않는다.
        /// </summary>
        [MaxLength(1024)]
        public string EditorDivision { get; set; }

        /// <summary>
        /// 파일의 상태정보
        /// </summary>
        public FileStateType FileState { get; set; }
}
}
