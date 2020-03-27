using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterItemParam :CsvDataParam
{
	public int item_id { get; set; }
	public string name { get; set; }
	public string sprite_name { get; set; }
}

public class MasterItem : CsvData<MasterItemParam>
{
}
