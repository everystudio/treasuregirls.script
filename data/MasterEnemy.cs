using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEnemyParam :CsvDataParam
{
	public int enemy_id { get; set; }
	public string name { get; set; }
	public int hp { get; set; }
	public int attack { get; set; }
	public int def { get; set; }
	public int speed { get; set; }
	public int mind { get; set; }
	public int luck { get; set; }

	public string sprite_name { get; set; }

	public MasterEnemyParam GetLevel(int _iLevel)
	{
		MasterEnemyParam ret = new MasterEnemyParam();

		return ret;
	}


}

public class MasterEnemy : CsvData<MasterEnemyParam>
{
}
