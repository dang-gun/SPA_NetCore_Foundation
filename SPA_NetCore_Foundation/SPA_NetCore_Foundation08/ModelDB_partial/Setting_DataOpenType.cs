using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// Setting_Data의 공개 타입
    /// </summary>
    public enum Setting_DataOpenType
    {
        /// <summary>
        /// 선택 없음.
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 공개
        /// </summary>
        Public = 11,

        /// <summary>
        /// 비공개
        /// </summary>
        Private = 21,
    }

    
}
