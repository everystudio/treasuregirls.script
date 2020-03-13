using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterCharaParam : CsvDataParam
{
	public int chara_id { get; set; }
	public string name { get; set; }
	public int price { get; set; }// gem

	public string outline { get; set; }

	public string GetIconName()
	{
		return string.Format("charaicon_{0:000}", chara_id);
	}
}

public class MasterChara : CsvData<MasterCharaParam>
{
}
