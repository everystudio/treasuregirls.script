using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFloorParam :CsvDataParam
{
	public int floor_id { get; set; }
	public int stage_id { get; set; }
	public int status { get; set; }

	public int count { get; set; }
}

public class DataFloor : CsvData<DataFloorParam>
{
}
