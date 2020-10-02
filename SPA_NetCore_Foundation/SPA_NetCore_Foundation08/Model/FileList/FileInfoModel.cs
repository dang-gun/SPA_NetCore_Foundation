using ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileList.Model
{
    /// <summary>
    /// 파일 정보 모델
    /// </summary>
    public class FileInfoModel
    {
        /// <summary>
        /// 파일 이름
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 파일 확장자
        /// </summary>
        public string Extension { get; set; }
        /// <summary>
        /// 파일 크기
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 파일의 타입 정보
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 파일에 대한 설명
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 에디터에서 사용될 파일 구분값
        /// </summary>
        public string EditorDivision { get; set; }

        /// <summary>
        /// 바이너리 정보를 사용할지 여부.
        /// 이것이 true이면 동적으로 바이너리 정보를 읽어 미리보기이미지로 출력하게 된다.
        /// 이미 처리된 데이터인경우 이것이 false가 되어야 한다.
        /// </summary>
        public bool BinaryIs { get; set; }
        /// <summary>
        /// 바이너리 정보를 사용할때 바이너리 데이터가 준비가 끝났는지 여부
        /// </summary>
        public bool BinaryReadyIs { get; set; }
        /// <summary>
        /// 로컬파일인 경우 파일의 바이너리 정보.
        /// base64로 들어온다.
        /// </summary>
        public string Binary { get; set; }


        /// <summary>
        /// 파일이 업로드되어 있을때 고유 번호
        /// </summary>
        public long idFile { get; set; }
        /// <summary>
        /// 파일이 업로드 되어 있는 상태에 가지고 있는
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 썸네일 여부
        /// </summary>
        public bool Thumbnail { get; set; }

        /// <summary>
        ///  수정여부
        /// </summary>
        public bool Edit { get; set; }
        /// <summary>
        /// 삭제여부
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FileInfoModel()
        { 
        }

        /// <summary>
        /// 파일 리스트 DB를 받아 변환한다.
        /// </summary>
        /// <param name="flData"></param>
        public FileInfoModel(FileInfo flData)
        {
            this.idFile = flData.idFileList;
            this.Name = flData.FileName;
            this.Extension = flData.Ext;
            this.Size = flData.Size;
            this.Url = flData.FileUrl;
            this.Description = flData.Description;
            this.EditorDivision = flData.EditorDivision;


            this.BinaryIs = false;
            this.Thumbnail = false;
            this.Edit = false;
            this.Delete = false;
        }
    }
}

