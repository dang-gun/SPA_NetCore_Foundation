using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

using Newtonsoft.Json;

using ModelDB;
using SPA_NetCore_Foundation.Global;
using FileListModel;
using BoardModel;

namespace Faculty.File
{
    /// <summary>
    /// 파일 변환 관련
    /// </summary>
    public class FileProcess
    {
        /// <summary>
        /// base64 바이너리 정보를 바이트배열로 변환한다.
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public byte[] Base64ToByte(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            //using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            //{
            //    Image image = Image.FromStream(ms, true);
            //    return image;
            //}
            return imageBytes;
        }

        /// <summary>
        /// 파일을 저장하고 DB에 정보를 저장한다.
        /// </summary>
        /// <param name="sDir"></param>
        /// <param name="listFileInfo"></param>
        /// <param name="fiThumbnail"></param>
        /// <param name="byteThumbnail"></param>
        /// <returns></returns>
        public FileData[] FileInDb(string sDir
            , FileInfoModel[] listFileInfo
            , out FileInfoModel fiThumbnail
            , out byte[] byteThumbnail)
        {
            List<FileData> listFileNew = new List<FileData>();

            fiThumbnail = null;
            byteThumbnail = null;


            DateTime dtNow = DateTime.Now;

            //파일 저장 위치
            string sFilePathShort
                = string.Format(@"\wwwroot\Upload\{0:D4}\{1:D2}\{2:D2}\"
                    , dtNow.Year
                    , dtNow.Month
                    , dtNow.Day);
            //파일 외부 주소
            string sFileUrl
                    = string.Format(@"/Upload/{0:D4}/{1:D2}/{2:D2}/"
                        , dtNow.Year
                        , dtNow.Month
                        , dtNow.Day);

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //파일 처리******

                //파일 리스트 문자열
                StringBuilder sbFileList = new StringBuilder();

                foreach (FileInfoModel itemFI in listFileInfo)
                {
                    if((0 >= itemFI.idFile)
                        && (false == itemFI.Delete)
                        && (false == itemFI.Edit))
                    {//추가다
                     //파일 추가*******************
                        FileData newFL = new FileData();
                        newFL.FileName = itemFI.Name;
                        newFL.Ext = itemFI.Extension;
                        newFL.Description = itemFI.Description;
                        newFL.EditorDivision = itemFI.EditorDivision;
                        newFL.FileState = FileStateType.Normal;

                        //기초정보 생성
                        db1.FileData.Add(newFL);
                        db1.SaveChanges();

                        //생성된 인덱스 추가
                        sbFileList.Append("," + newFL.idFileList);

                        //저장위치 + 파일인덱스 + 확장자
                        string sFilePathShortFileName = sFilePathShort + newFL.idFileList + itemFI.Extension;
                        //url + 파일인덱스 + 확장자
                        string sFilePath = sDir + sFilePathShortFileName;
                        //파일 url
                        string sFileUrlName
                            = sFileUrl
                                + string.Format(@"{0}{1}"
                                    , newFL.idFileList
                                    , itemFI.Extension);

                        //디랙토리 생성
                        System.IO.Directory.CreateDirectory(sDir + sFilePathShort);
                        //파일 생성
                        using (FileStream stream = System.IO.File.Create(sFilePath))
                        {
                            //베이스64 데이터를 바이트 어레이로 변환
                            string[] sCutBase64 = itemFI.Binary.Split(",");
                            byte[] byteFile;
                            if (2 <= sCutBase64.Length)
                            {
                                byteFile = Convert.FromBase64String(sCutBase64[1]);
                            }
                            else
                            {
                                byteFile = Convert.FromBase64String(sCutBase64[0]);
                            }
                            
                            //파일 시스템에 저장
                            stream.Write(byteFile);

                            //썸네일 임시저장
                            if (true == itemFI.Thumbnail)
                            {
                                fiThumbnail = itemFI;
                                byteThumbnail = byteFile;
                            }

                            //파일용량 저장
                            newFL.Size = byteFile.Length;
                        }//end using stream

                        newFL.FileDir = sFilePathShortFileName;
                        newFL.FileUrl = sFileUrlName;


                        db1.SaveChanges();


                        //완성된 파일 정보 백업
                        listFileNew.Add(newFL);
                    }
                    else if (true == itemFI.Delete)
                    {//삭제다.

                        if(0>= itemFI.idFile)
                        {//파일 정보가 없다.
                            //삭제가 불가능하다.
                        }
                        else
                        {
                            //파일정보를 찾는다.
                            FileData findFl
                                = db1.FileData
                                    .Where(m => m.idFileList == itemFI.idFile)
                                    .FirstOrDefault();

                            //삭제 타입으로 변경
                            findFl.FileState = FileStateType.DeleteReservation;
                        }
                        
                    }
                    
                }
            }//end using db1

            return listFileNew.ToArray();
        }//end FileInDb



        /// <summary>
        /// wwwroot 내보내는 json파일 처리
        /// </summary>
        /// <param name="sDir"></param>
        /// <param name="objList"></param>
        public void WWW_Json(string sDir, object objList)
        {
            //리스트를 Json으로 변환
            string sJsonList = JsonConvert.SerializeObject(objList);

            string sFileDir = GlobalStatic.Dir_LocalRoot + @"\wwwroot\" + sDir;

            using (StreamWriter stream = new StreamWriter(sFileDir, false, Encoding.UTF8))
            {
                //파일 저장
                stream.Write(sJsonList);
                //stream.BaseStream.Write(byteJsonList);
            }//end using stream


        }//end WWW_Json


        /// <summary>
        /// 게시판 정보 json으로 저장 - 유저에게 보낼 게시판 정보
        /// </summary>
        public void WWW_Json_BoardInfo()
        {
            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //json 파일로 저장*******************************
                List<BoardListInfoModel> listBoard
                    = db1.Board
                        .Select(m => new BoardListInfoModel(m))
                        .ToList();
                WWW_Json(
                    @"Faculty\Board\BoardInfo.json"
                    , listBoard);
            }
        }

        /// <summary>
        /// 스크림을 파일로 바꾼다.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="resourceImage"></param>
        /// <returns></returns>
        public Image GetReducedImage(int width, int height, Stream resourceImage)
        {
            try
            {
                Image image = Image.FromStream(resourceImage);
                Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return null;
            }
        }

    }
}
