using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumToClass
{
	/// <summary>
	/// 열거형의 멤버를 분해하여 배열형태로 관리 해주는 클래스.
	/// </summary>
	public class EnumToModel
	{
		/// <summary>
		/// 지정된 열거형
		/// </summary>
		public Enum EnumType { get; private set; }
		/// <summary>
		/// 지정된 열거형의 이름
		/// </summary>
		public string EnumName { get { return this.EnumType.GetType().Name; } }
		/// <summary>
		/// 지정된 열거형의 네임스페이스
		/// </summary>
		public string EnumNamespace { get { return this.EnumType.GetType().Namespace; } }

		/// <summary>
		/// 분해한 열거형 멤버 데이터
		/// </summary>
		public EnumMember[] EnumMember { get; private set; }

		/// <summary>
		/// 지정된 열거형의 멤버 갯수
		/// </summary>
		public int Count
		{
			get
			{
				return this.EnumMember.Length;
			}
		}

		/// <summary>
		/// 읽어들인 xml 내용
		/// </summary>
		public EnumXml EnumXml { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeData"></param>
		public EnumToModel(Enum typeData)
		{
			Reset(typeData, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeData"></param>
		/// <param name="objEnumXml"></param>
		public EnumToModel(Enum typeData, EnumXml objEnumXml)
        {
			Reset(typeData, objEnumXml);
		}

		private void Reset(Enum typeData, EnumXml objEnumXml)
		{
			//원본 저장
			this.EnumType = typeData;
			this.EnumXml = objEnumXml;

			//들어온 열거형을 리스트로 변환한다.
			Array arrayTemp = Enum.GetValues(this.EnumType.GetType());

			//맴버 갯수만큼 공간을 만들고
			this.EnumMember = new EnumMember[arrayTemp.Length];

			//각 맴버를 입력한다.
			for (int i = 0; i < arrayTemp.Length; ++i)
			{
				this.EnumMember[i] = new EnumMember(arrayTemp.GetValue(i));
			}
		}

		/// <summary>
		/// 멤버중 지정한 이름이 있는지 찾습니다.
		/// </summary>
		/// <param name="sName"></param>
		/// <returns></returns>
		public EnumMember FindEnumMember(string sName)
		{
			EnumMember emReturn = null;
			List<EnumMember> listEM = new List<EnumMember>();

			//검색한다.
			listEM = this.EnumMember.Where(member => member.Name == sName).ToList();

			if (0 < listEM.Count)
			{	//검색된 데이터가 있다면
				//맨 첫번째 값을 저장
				emReturn = listEM[0];
			}

			return emReturn;
		}

		/// <summary>
		/// 자바스크립트에서 사용하는 열거형 타입으로 선언하는 코드를 생성한다.
		/// </summary>
		/// <returns></returns>
		public string ToJavaScriptVarString()
        {
			StringBuilder sbReturn = new StringBuilder();

			

			//머리 만들기*********
			//주석 검색어 만들기 - 타입명
			string sT = string.Format("T:{0}.{1}"
											, this.EnumNamespace
											, this.EnumName);
			//주석 검색어 만들기 - 요소 명
			string sF = string.Format("F:{0}.{1}"
											, this.EnumNamespace
											, this.EnumName);

			//머리 주석
			if (null != this.EnumXml)
            {
				string sHeadSummary 
					= this.EnumXml.SummaryGet(sT);

				if(string.Empty != sHeadSummary)
                {//주석 내용이 있다.
					sbReturn.Append(string.Format("/** {0} */" + Environment.NewLine
													, sHeadSummary));
				}
			}

			//머리 이름
			sbReturn.Append(string.Format("var {0} = " + Environment.NewLine
												+ "{{" + Environment.NewLine
											, this.EnumName));

			for (int i = 0; i < this.Count; ++i)
			{
				EnumMember itemEM = this.EnumMember[i];

				//검색어 완성 시키기
				string sF_Name = sF + "." + itemEM.Name;

				//주석
				if (null != this.EnumXml)
				{
					string sSummary
						= this.EnumXml.SummaryGet(sF_Name);

					if (string.Empty != sSummary)
					{//주석 내용이 있다.
						sbReturn.Append(string.Format(@"    /** {0} */" + Environment.NewLine
														, sSummary));
					}
				}

				//요소 추가
				sbReturn.Append(
					string.Format(@"    {0}: {1}," + Environment.NewLine
									, itemEM.Name
									, itemEM.Index));
			}

			//꼬리 만들기
			sbReturn.Append(string.Format("}}", this.EnumName));

			return sbReturn.ToString();
        }
	}
}
