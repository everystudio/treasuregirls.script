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

	public void BuildPlayer()
	{
		int total_attack = 0;
		int total_speed = 0;

		int total_hp = 0;
		int total_def = 0;
		int total_mind = 0;
		int total_luck = 0;

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

		hp = total_hp;
		hp_max = total_hp;

		attack = total_attack;
		speed = total_speed;
		def = total_def;
		mind = total_mind;
		luck = total_luck;
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

	public void Damage( int _iDamage)
	{
		hp = Mathf.Max(0, hp - _iDamage);
	}

	public void Heal( int _iHeal)
	{
		hp = Mathf.Min(hp + _iHeal, hp_max);
	}

}

public class DataUnit : CsvData<DataUnitParam>
{
}
