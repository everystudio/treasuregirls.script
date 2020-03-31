using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSkillParam : CsvDataParam
{
	public int skill_id { get; set; }
	public string name { get; set; }
	public string sprite_name { get; set; }
	public string prefab_name { get; set; }

	public float cool_time { get; set; }

	public string outline { get; set; }
	public string type { get; set; }
	public int param { get; set; }

	public int gold { get; set; }
	public int gem { get; set; }

}

public class MasterSkill : CsvData<MasterSkillParam>
{
}
