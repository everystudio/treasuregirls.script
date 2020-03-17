using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPotionParam : CsvDataParam
{
	public int potion_id { get; set; }
	public int level { get; set; }
	public string name { get; set; }
	public string sprite_name { get; set; }
	public int heal { get; set; }
	public float cool_time { get; set; }

	public int next_potion_id { get; set; }
	public int upgrade_gold { get; set; }
	public int add_gold { get; set; }
}

public class MasterPotion : CsvData<MasterPotionParam>
{
}
