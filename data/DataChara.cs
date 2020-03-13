using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCharaParam : CsvDataParam
{
	public int chara_id { get; set; }
	public string status { get; set; }

	public bool IsStatus( DataChara.STATUS _eStatus )
	{
		// 大文字だっけ？
		return status.ToUpper() == _eStatus.ToString().ToUpper();
	}


}

public class DataChara : CsvData<DataCharaParam>
{
	public enum STATUS
	{
		LOCKED	= 0,
		IDLE	,
		USING	,

	};
}
