using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// これ継承する
public abstract class DataBattleStatusParam : CsvDataParam
{
	public int hp { get; set; }
	public int hp_max { get; set; }
	public int mp { get; set; }
	public int mp_max { get; set; }

	public int attack { get; set; }
	public int defence { get; set; }

	public int attribute1 { get; set; }
	public int attribute2 { get; set; }
	public int attribute3 { get; set; }
	public int attribute4 { get; set; }
	public int attribute5 { get; set; }

	public int strength { get; set; }
	public int agility { get; set; }
	public int technique { get; set; }
	public int intelligence { get; set; }
	public int mind { get; set; }


}

