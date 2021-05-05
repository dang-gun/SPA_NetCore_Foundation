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
        private string sNewLine = Environment.NewLine;

        /// <summary>
        /// 
        /// </summary>
        public ListToJavascript()
        { 
        }

        /// <summary>
        /// 리스트를 Json파일 문자열로 바꾼다.
        /// </summary>
        /// <param name="listData"></param>
        /// <returns></returns>
        public string ToJsonString(List<ListToJavascriptModel> listData)
        {
            StringBuilder sbReturn = new StringBuilder();

            //배열 시작 
            sbReturn.Append("[" + this.sNewLine);

            foreach(ListToJavascriptModel itemLTJ in listData)
            {
                sbReturn.Append("{" + this.sNewLine);

                if (string.Empty != itemLTJ.Summary)
                {//주석이 있다.
                    sbReturn.Append("//" + itemLTJ.Summary + this.sNewLine);
                }

                //이름
                sbReturn.Append("\"" + itemLTJ.Name + "\": ");

                //값
                sbReturn.Append("\"" + itemLTJ.Value + "\",");


                sbReturn.Append("}," + this.sNewLine);
            }//end foreach itemLTJ

            //배열 끝
            sbReturn.Append("]" + this.sNewLine);

            return sbReturn.ToString();
        }
    }
}
