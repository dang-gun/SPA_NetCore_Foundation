using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ListToWwwFile
{
    /// <summary>
    /// 자바스크립트에서 사용할 형태로 만들기위한 공통 모델
    /// </summary>
    public class ListToJavascriptModel
    {
        /// <summary>
        /// 주석
        /// </summary>
        public string Summary { get; set; }


        /// <summary>
        /// 이름
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 값
        /// </summary>
        public string Value { get; set; }
    }
}
