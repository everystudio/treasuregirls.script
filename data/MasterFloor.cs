using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterFloorParam : CsvDataParam
{
	public int floor_id { get; set; }
	public int stage_id { get; set; }
	public int floor_no { get; set; }

	public int enemy_id { get; set; }
	public int enemy_level { get; set; }
	public int boss_enemy_id { get; set; }
	public int boss_level { get; set; }

	public int drop_item_id { get; set; }
	public int rare_item_id { get; set; }

}

public class MasterFloor : CsvData<MasterFloorParam>
{
}
