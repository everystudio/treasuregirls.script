using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataScrollParam :CsvDataParam
{
	public int scroll_id { get; set; }
	public int num { get; set; }
	public DataScrollParam() { }
	public DataScrollParam(int _id , int _num)
	{
		scroll_id = _id;
		num = _num;
	}
}

public class DataScroll : CsvData<DataScrollParam>
{
}
