using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Model.ApiModel
{
    /// <summary>
    /// api요청을 처리할때 요청결과처리를 공통화 하는 클래스.
    /// ApiResultFailModel를 공통으로 리턴하기 위해 베이스를 가지고 있다.
    /// 결과 출력용으로 데이터는 
    /// </summary>
    public class ApiResultReadyModel: ApiResultBaseModel
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
        /// API의 처음부분에서 선언한다.
        /// </summary>
        /// <param name="cbThis">컨트롤러 기능을 사용하기위한 인스턴스</param>

        public ApiResultReadyModel(ControllerBase cbThis)
            : base(null, null)
        {
            this.ThisCB = cbThis;

            this.StatusCode = StatusCodes.Status200OK;
        }

        /// <summary>
        /// API끝에서 호출하여 'ObjectResult'를 생성하여 리턴해 준다.
        /// 만들어지는 결과의 ApiResultBaseModel데이터는 this 기준이다.
        /// </summary>
        /// <param name="objResultData">전달할 모델</param>
        /// <returns></returns>
        public ObjectResult ToResult(object objResultData)
        {
            ObjectResult orReturn = null;

            if (null == objResultData)
            {//오브젝트가 없다.
                //없으면 ApiResultBaseModel로 초기화 해준다.
                objResultData = new ApiResultBaseModel();
            }

            if (StatusCode == StatusCodes.Status200OK)
            {//성공
                if(null != base.InfoCode)
                {//베이스에 데이터가 있다.
                 //베이스에 있는 데이터를 사용한다.
                 //결과에 있는 코드와 메시지를 결과용 모델에 저장한다.
                    ((ApiResultBaseModel)objResultData).InfoCode = base.InfoCode;
                    ((ApiResultBaseModel)objResultData).Message = base.Message;
                }
                else
                {//베이스에 데이터가 없으면 들어온 데이터를 그대로 사용한다.
                }

                
                //성공은 전달받은 오브젝트를 준다,
                orReturn = this.ThisCB.StatusCode(this.StatusCode, objResultData);
            }
            else
            {//실패
                //실패는 500 에러를 기본으로 전달해야 한다.
                ApiResultFailModel afm = new ApiResultFailModel(base.InfoCode, base.Message);

                //여기에 들어왔다는건 예측 가능한 오류가 났다는 의미다.
                //예측가능한 오류는 200으로 바꿔준다.
                orReturn = this.ThisCB.StatusCode(StatusCodes.Status200OK, afm);
                //여기서 예측가능한 오류를 200으로 바꾸지 않으려면 이 코드를 사용한다.
                //orReturn = this.ThisCB.StatusCode(this.StatusCode, afm);
            }

            return orReturn;
        }
    }
}
