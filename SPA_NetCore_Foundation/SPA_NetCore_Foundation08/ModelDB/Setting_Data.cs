using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 이 프로그램 전체에서 사용되는 세팅 정보.
    /// 매번 DB에서 호출하면 자원낭비가 심하므로 메모리에 올려서 사용한다.
    /// </summary>
    public class Setting_Data
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idSetting_Data { get; set; }

        /// <summary>
        /// 표시 순서로 사용할 숫자
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 구분용 고유 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 공개 타입
        /// </summary>
        public Setting_DataOpenType OpenType { get; set; }

        /// <summary>
        /// 설정값
        /// </summary>
        public string ValueData { get; set; }

        /// <summary>
        /// 이 설정의 설명
        /// </summary>
        public string Description { get; set; }
    }
}
