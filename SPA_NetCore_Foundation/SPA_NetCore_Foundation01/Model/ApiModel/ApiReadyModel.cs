using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.ApiModel.Model
{
    /// <summary>
    /// api요청을 처리할때 사용할 모델
    /// </summary>
    public class ApiReadyModel
    {
        /// <summary>
        /// 컨트롤러베이스의 기능을 쓰기위한 개체
        /// </summary>
        private ControllerBase ThisCB { get; set; }

        /// <summary>
        /// 스테이터스 코드
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 실패시 전달한 코드
        /// 0 : 성공.
        /// 다른 값은 모두 실패
        /// </summary>
        public string InfoCode { get; set; }
        /// <summary>
        /// 실패시 전달할 메시지.
        /// 개발에 편하도록 작업한다.
        /// </summary>
        public string Message { get; set; }

        public ApiReadyModel(ControllerBase cbThis)
        {
            this.ThisCB = cbThis;


            this.StatusCode = StatusCodes.Status200OK;

            this.InfoCode = "0";
            this.Message = string.Empty;
        }

        public ObjectResult ToResult(object objResultData)
        {
            ObjectResult orReturn = null;

            if (StatusCode == StatusCodes.Status200OK)
            {//성공
                //성공은 전달받은 오브젝트를 준다,
                orReturn = this.ThisCB.StatusCode(this.StatusCode, objResultData);
            }
            else
            {//실패
                ApiFailModel afm = new ApiFailModel(this.InfoCode, this.Message);
                orReturn = this.ThisCB.StatusCode(this.StatusCode, afm);
            }

            return orReturn;
        }

    }
}
