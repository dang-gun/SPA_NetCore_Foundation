using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ListToWwwFile
{
    /// <summary>
    /// 리스트를 자바스크립트에서 사용할 형태로 만든다.
    /// </summary>
    public class ListToJavascript
    {
        /// <summary>
        /// 사용할 줄바꿈 문자
        /// </summary>
#if DEBUG
        private string sNewLine = Environment.NewLine;
#else
        private string sNewLine = string.Empty;;
#endif
        /// <summary>
        /// 
        /// </summary>
        public ListToJavascript()
        { 
        }

        /// <summary>
        /// 리스트를 Json파일 문자열로 바꾼다.
        /// json은 주석이 없으므로 주석은 무시한다.
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        public string ToJsonString(
            List<ListToJavascriptModel> listData)
        {
            StringBuilder sbReturn = new StringBuilder();

            //배열 시작 
            sbReturn.Append("[" + this.sNewLine);

            for(int i = 0; i < listData.Count; ++i )
            {
                ListToJavascriptModel itemLTJ = listData[i];

                if(0 != i)
                {
                    sbReturn.Append("{" + this.sNewLine);
                }

                sbReturn.Append("{" + this.sNewLine);

                //이름
                sbReturn.Append("\"" + itemLTJ.Name + "\": ");

                //마지막 데이터는 콤마를 넣지 말아야 한다.
                //값
                sbReturn.Append("\"" + itemLTJ.Value + "\"");

                sbReturn.Append(this.sNewLine);
                sbReturn.Append("}" + this.sNewLine);
            }//end for i


            //배열 끝
            sbReturn.Append("]" + this.sNewLine);

            return sbReturn.ToString();
        }
    }
}
