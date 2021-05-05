using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumToClass
{
	/// <summary>
	/// 열거형 멤버의 정보를 검색하기 쉽게 저장합니다.
	/// </summary>
	public class EnumMember
	{

		/// <summary>
		/// 지정된 열겨헝 멤버
		/// </summary>
		public Enum Type { get; private set; }
		/// <summary>
		/// 지정된 열겨헝 멤버의 이름
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// 지정된 열겨헝 멤버의 인덱스
		/// </summary>
		public int Index { get; private set; }

		/// <summary>
		/// 사용할 열거형 멤버를 오브젝트(object)형태로 처리합니다.
		/// </summary>
		/// <param name="objData"></param>
		public EnumMember(object objData)
		{
			SetData(objData as Enum);
		}

		/// <summary>
		/// 사용할 열거형 멤버를 지정합니다.
		/// </summary>
		/// <param name="typeData"></param>
		public EnumMember(Enum typeData)
		{
			SetData(typeData);
		}

		/// <summary>
		/// 필요한 데이터를 기록 합니다.
		/// </summary>
		/// <param name="typeData"></param>
		private void SetData(Enum typeData)
		{
			this.Type = typeData;
			this.Index = this.Type.GetHashCode();
			this.Name = this.Type.ToString();
		}
	}
}
