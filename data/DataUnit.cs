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
	public int ability_id { get; set; }
	public int ability_rate { get; set; }

	public int def { get; set; }
	public int mind { get; set; }
	public int luck { get; set; }
	public int move { get; set; }
	public int heal { get; set; }
	public int coin { get; set; }

	public UnityEventIntInt OnChangeHp = new UnityEventIntInt();

	public void BuildPlayer()
	{
		int total_attack = 0;
		int total_speed = 0;

		int total_hp = 0;
		int total_def = 0;
		int total_mind = 0;
		int total_luck = 0;
		int total_move = 100;
		int total_heal = 100;
		int total_coin = 100;

		DataWeaponParam data_equip_weapon = DataManager.Instance.dataWeapon.list.Find(p => 0 < p.equip);
		MasterWeaponParam master_equip_weapon = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == data_equip_weapon.weapon_id);

		// レベルの校正
		MasterWeaponParam master_equip_weapon_level = master_equip_weapon.GetParam(data_equip_weapon.level);

		total_attack = master_equip_weapon_level.attack;
		total_speed = master_equip_weapon_level.speed;

		for (int i = 0; i < MasterArmor.ArmorPositionArr.Length; i++)
		{
			DataArmorParam data = DataManager.Instance.dataArmor.list.Find(p => p.position == MasterArmor.ArmorPositionArr[i]);
			MasterArmorParam master = DataManager.Instance.masterArmor.list.Find(p => p.armor_id == data.armor_id);
			total_hp += master.hp;
			total_def += master.def;
			total_mind += master.mind;
			total_luck += master.luck;
		}

		List<DataTreasureParam> data_treasure_list = DataManager.Instance.dataTreasure.list.FindAll(p => 0 < p.equip);

		TreasureAssist assist = TreasureAssist.GetTreasureAssist(data_treasure_list);

		total_hp = Mathf.CeilToInt((float)total_hp * (float)((float)assist.hp / 100.0f));
		total_attack = Mathf.CeilToInt((float)total_attack * (float)((float)assist.attack / 100.0f));
		total_def = Mathf.CeilToInt((float)total_def * (float)((float)assist.def / 100.0f));
		total_mind = Mathf.CeilToInt((float)total_mind * (float)((float)assist.mind / 100.0f));
		total_move = Mathf.CeilToInt((float)total_move * (float)((float)assist.move / 100.0f));
		total_heal = Mathf.CeilToInt((float)total_heal * (float)((float)assist.heal / 100.0f));
		total_luck = Mathf.CeilToInt((float)total_luck * (float)((float)assist.luck / 100.0f));
		total_coin = Mathf.CeilToInt((float)total_coin * (float)((float)assist.coin / 100.0f));

		hp = total_hp;
		hp_max = total_hp;

		attack = total_attack;
		speed = total_speed;
		def = total_def;
		mind = total_mind;
		luck = total_luck;

		move = total_move;
		heal = total_heal;
		coin = total_coin;

		//Debug.Log(string.Format("HP:{0} atk:{1} def:{2}", hp_max, attack, def));

	}

	public void BuildEnemy(MasterEnemyParam _enemy , int _iLevel)
	{
		hp = GetParamLevel(_enemy.hp, _iLevel);
		hp_max = hp;
		attack = GetParamLevel(_enemy.attack, _iLevel);
		speed = GetParamLevel(_enemy.speed, _iLevel);
		def = GetParamLevel(_enemy.def, _iLevel);
		mind = GetParamLevel(_enemy.mind, _iLevel);
		luck = GetParamLevel(_enemy.luck, _iLevel);
	}

	// 10レベルで２倍
	public int GetParamLevel( int _iParam , int _iLevel)
	{
		float rate = _iLevel * 0.1f;
		return _iParam + (int)(_iParam * rate);
	}

	public int CalcDamage(DataUnitParam _target , MasterSkillParam _skill , MasterWeaponParam _weapon)
	{
		int use_attack = attack;
		if( _weapon != null)
		{
			//Debug.Log(_weapon.weapon_type);
			if(_weapon.weapon_type == "hammer")
			{
				use_attack /= 2;
			}
			else if( _weapon.weapon_type == "dagger")
			{
				use_attack = use_attack + use_attack/2;
			}
			else
			{
			}
		}
		//Debug.Log(string.Format("{0}:{1}", attack, use_attack));

		float base_damage = (use_attack * 3 - _target.def) * 0.717f;
		//Debug.Log(base_damage);

		if (base_damage < 1.0f)
		{
			base_damage = 1.0f;
		}

		float skill_rate = 1.0f;
		if( _skill != null)
		{
			skill_rate = _skill.param * 0.01f;
		}

		float[] rand_rate_table = { 0.98f, 0.99f, 1.00f, 1.01f, 1.02f };
		//int[] rand_add_table = { 0,1,2,3,4 };
		int[] rand_add_table = { 0, 1,1,2,2 };

		int index_rand_rate = UtilRand.GetRand(rand_rate_table.Length);
		int index_rand_add = UtilRand.GetRand(rand_add_table.Length);

		float fRet = (base_damage * skill_rate * rand_rate_table[index_rand_rate]) + rand_add_table[index_rand_add];

		if( fRet < 1.0f)
		{
			fRet = 1.0f;
		}
		return (int)fRet;
	}

	public void Damage( int _iDamage)
	{
		hp = Mathf.Max(0, hp - _iDamage);
		//Debug.Log(hp);
		OnChangeHp.Invoke(hp, hp_max);
	}

	public void Heal( int _iHeal)
	{
		hp = Mathf.Min(hp + _iHeal, hp_max);
		OnChangeHp.Invoke(hp, hp_max);
	}

}

public class DataUnit : CsvData<DataUnitParam>
{
}
