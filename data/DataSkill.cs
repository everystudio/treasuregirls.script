using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataSkillEvent : UnityEvent<DataSkillParam>{
}

public class DataSkillParam : CsvDataParam
{
	public int skill_id { get; set; }
	public int position { get; set; }

	public DataSkillParam() { }
	public DataSkillParam(int _iSkillId , int _iPosition)
	{
		// ポジションは重複禁止なので使用場所は注意
		skill_id = _iSkillId;
		position = _iPosition;
	}

}

public class DataSkill : CsvData<DataSkillParam>
{
}
