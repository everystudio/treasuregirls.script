using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStageParam : CsvDataParam
{
	public int stage_id { get; set; }
	public int status { get; set; }
}

public class DataStage : CsvData<DataStageParam>
{
}
