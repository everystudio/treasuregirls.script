using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUnitParam :CsvDataParam
{
    // プロジェクトごとに使い分ける必要あり
    public int unit_id;

	public int hp { get; set; }
	public int hp_max { get; set; }

	public int attack { get; set; }
	public int speed { get; set; }

	public int def { get; set; }
	public int mind { get; set; }
	public int luck { get; set; }

	public int CalcDamage(DataUnitParam _target , MasterSkillParam _skill )
	{
		float base_damage = (attack * 3 - _target.def) * 0.717f;

		float skill_rate = 1.0f;
		if( _skill != null)
		{
			skill_rate = _skill.param * 0.01f;
		}

		float[] rand_rate_table = { 0.98f, 0.99f, 1.00f, 1.01f, 1.02f };
		int[] rand_add_table = { 0,1,2,3,4 };

		int index_rand_rate = UtilRand.GetRand(rand_rate_table.Length);
		int index_rand_add = UtilRand.GetRand(rand_add_table.Length);

		float fRet = (base_damage * skill_rate * rand_rate_table[index_rand_rate]) + rand_add_table[index_rand_add];

		return (int)fRet;
	}


}

public class DataUnit : CsvData<DataUnitParam>
{
}
