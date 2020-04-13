using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterArmorParam : CsvDataParam
{
	public int armor_id { get; set; }
	public string position { get; set; }

	public int level { get; set; }
	public int hp { get; set; }
	public int def { get; set; }
	public int mind { get; set; }
	public int luck { get; set; }
	public int coin { get; set; }
	public int next_id { get; set; }

	public string sprite_name { get;set;}  
	public string armor_name { get; set; }
}

public class MasterArmor : CsvData<MasterArmorParam>
{
	public static readonly string[] ArmorPositionArr = new string[8]{
		"helmet",
		"body",
		"gloves",
		"shield",
		"bracelet",
		"necklace",
		"pants",
		"cloaks",
		};
}
