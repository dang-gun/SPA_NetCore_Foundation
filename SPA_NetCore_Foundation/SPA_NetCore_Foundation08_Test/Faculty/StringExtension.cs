using ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty
{
    /// <summary>
    /// 문자열 확장 메소드
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 문자열 지정한 크기만큼 잘라준다.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string WithMaxLength(this string value, int maxLength)
        {
            return value?.Substring(0, Math.Min(value.Length, maxLength));
        }

    }
}
