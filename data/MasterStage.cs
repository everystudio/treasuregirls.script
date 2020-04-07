using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterStageParam : CsvDataParam
{
	public int stage_id { get; set; }
	public string name { get; set; }
	public string thumb_name { get; set; }
	public string bg_name { get; set; }
}

public class MasterStage : CsvData<MasterStageParam>
{
}



