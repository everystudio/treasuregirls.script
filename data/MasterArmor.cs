using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterArmorParam : CsvDataParam
{
	public int armor_id { get; set; }
	public string position { get; set; }

	public int level { get; set; }
	public int def { get; set; }
	public int hp { get; set; }
	public int luck { get; set; }
	public int gold { get; set; }
	public int next_id { get; set; }

	public string sprite_name { get;set;}  
	public string armor_name { get; set; }
}

public class MasterArmor : CsvData<MasterArmorParam>
{
}
