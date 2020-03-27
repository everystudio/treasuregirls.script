using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataItemParam :CsvDataParam
{
	public int item_id { get; set; }
	public int num { get; set; }
}

public class DataItem : CsvData<DataItemParam>
{
	public void Add( int _item_id , int _num )
	{
		DataItemParam data = list.Find(p => p.item_id == _item_id);
		if( data == null)
		{
			data = new DataItemParam();
			data.item_id = _item_id;
			data.num = 0;
			list.Add(data);
		}
		data.num += _num;
	}
}
