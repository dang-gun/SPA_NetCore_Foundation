using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EnumToClass
{
    /// <summary>
    /// 프로젝트의 XML 출력파일을 미리 읽어들이는 클래스
    /// </summary>
    public class EnumXml
    {
        /// <summary>
        /// 읽어들이 xml 내용
        /// </summary>
        public XDocument ProjectXml { get; private set; }

        /// <summary>
        /// 맴버 요소 미리 검색
        /// </summary>
        public XElement[] Members { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sXmlName">불러올 프로젝트 XML 파일 이름</param>
        public EnumXml(string sXmlName)
        {
            this.ProjectXml = XDocument.Load(sXmlName);
            this.Members
                = this.ProjectXml
                    .Elements("doc")
                    .Elements("members")
                    .Elements("member")
                    .ToArray();
        }

        /// <summary>
        /// 맴버이름으로 주석 내용을 받아온다.
        /// </summary>
        /// <param name="sMemberName"></param>
        /// <returns></returns>
        public string SummaryGet(string sMemberName)
        {
            string sReturn = string.Empty;

            XElement findXE 
                = this.Members
                        .Where(m => m.Attribute("name").Value == sMemberName)
                        .FirstOrDefault();

            if (null != findXE)
            {
                sReturn = findXE.Element("summary").Value.Trim();
            }

            return sReturn;
        }

    }
}
