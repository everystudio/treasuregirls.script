using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFloorParam :CsvDataParam
{
	public int floor_id { get; set; }
	public int status { get; set; }
}

public class DataFloor : CsvData<DataFloorParam>
{
}
