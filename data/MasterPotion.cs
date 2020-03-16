using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPotionParam : CsvDataParam
{
	public int potion_id;
	public int level;
	public string sprite_name;
	public int heal;
	public float cool_time;

	public int next_potion_id;
	public int gradeup;
}

public class MasterPotion : CsvData<MasterPotionParam>
{
}
