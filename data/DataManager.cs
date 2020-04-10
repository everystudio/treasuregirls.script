using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : DataManagerBase<DataManager>
{
	[HideInInspector]
	public bool Initialized
	{
		get;
		private set;
	}

	public TextAssetHolder m_textAssetHolder;

	public CsvKvs game_data = new CsvKvs();

	public MasterChara masterChara = new MasterChara();
	public MasterWeapon masterWeapon = new MasterWeapon();
	public MasterArmor masterArmor = new MasterArmor();
	public MasterSkill masterSkill = new MasterSkill();
	public MasterPotion masterPotion = new MasterPotion();
	public MasterTreasure masterTreasure = new MasterTreasure();
	public MasterEnemy masterEnemy = new MasterEnemy();
	public MasterItem masterItem = new MasterItem();
	public MasterStage masterStage = new MasterStage();
	public MasterFloor masterFloor = new MasterFloor();

	public DataChara dataChara = new DataChara();
	public DataWeapon dataWeapon = new DataWeapon();
	public DataWeapon dataWeaponAlbum = new DataWeapon();
	public DataArmor dataArmor = new DataArmor();
	public DataSkill dataSkill = new DataSkill();
	public DataPotion dataPotion = new DataPotion();
	public DataTreasure dataTreasure = new DataTreasure();
	public DataTreasure dataTreasureAlbum = new DataTreasure();
	public DataScroll dataScroll = new DataScroll();
	public DataStage dataStage = new DataStage();
	public DataFloor dataFloor = new DataFloor();

	public DataUnit dataUnit = new DataUnit();
	public DataItem dataItem = new DataItem();
	public DataItem dataGetItem = new DataItem();

	public int debug_gem;
	public int debug_gold;
	public int debug_key;
	public int debug_gold_key;
	public bool UseGem(int _iGem)
	{
		// Gemの消費処理
		if( debug_gem < _iGem)
		{
			return false;
		}
		debug_gem -= _iGem;
		return true;
	}
	public int GetGem()
	{
		return debug_gem;
	}

	public bool UseGold(int _iGold)
	{
		// Gemの消費処理

		if(debug_gold < _iGold )
		{
			return false;
		}

		debug_gold -= _iGold;

		return true;
	}
	public void AddGold(int _iAdd)
	{
		return;
	}

	public int GetGold()
	{
		return debug_gold;
	}

	public bool UseKey(int _iUse)
	{
		if( debug_key < _iUse)
		{
			return false;
		}
		debug_key -= _iUse;
		return true;
	}
	public int GetKey()
	{
		return debug_key;
	}

	public bool UseGoldKey(int _iUse)
	{
		if( debug_gold_key < _iUse)
		{
			return false;
		}
		debug_gold_key -= _iUse;
		return true;
	}
	public int GetGoldKey()
	{
		return debug_gold_key;
	}

	public override void Initialize()
	{
		Initialized = false;
		base.Initialize();
		StartCoroutine(init_network());
	}

	IEnumerator init_network()
	{
		Debug.Log("init_network start");
		masterChara.SetSaveFilename(Defines.FILENAME_MASTERCHARA);

		#region 通信初期化
		yield return StartCoroutine(masterChara.SpreadSheet(Defines.SS_MASTER, "chara", () => { }));
		yield return StartCoroutine(masterWeapon.SpreadSheet(Defines.SS_MASTER, "weapon", () => { }));
		yield return StartCoroutine(masterArmor.SpreadSheet(Defines.SS_MASTER, "armor", () => { }));
		yield return StartCoroutine(masterSkill.SpreadSheet(Defines.SS_MASTER, "skill", () => { }));
		yield return StartCoroutine(masterPotion.SpreadSheet(Defines.SS_MASTER, "potion", () => { }));
		yield return StartCoroutine(masterTreasure.SpreadSheet(Defines.SS_MASTER, "treasure", () => {
			/*
			foreach( MasterTreasureParam master in masterTreasure.list)
			{
				Debug.Log(string.Format("id:{0} name:{1} sprite:{2}", master.treasure_id , master.name , master.sprite_name));
			}
			*/
		}));
		yield return StartCoroutine(masterEnemy.SpreadSheet(Defines.SS_MASTER, "enemy", () => { }));
		yield return StartCoroutine(masterItem.SpreadSheet(Defines.SS_MASTER, "item", () =>{ }));
		yield return StartCoroutine(masterStage.SpreadSheet(Defines.SS_MASTER, "stage", () =>{ }));
		yield return StartCoroutine(masterFloor.SpreadSheet(Defines.SS_MASTER, "floor", () =>{ }));

		#endregion

		game_data.SetSaveFilename(Defines.FILENAME_GAMEDATA);
		if (game_data.LoadMulti() == false)
		{
			// なんかする
			// なにもしなくてok
		}
		dataChara.SetSaveFilename(Defines.FILENAME_DATACHARA);
		if( dataChara.LoadMulti() == false)
		{
			// 初期データ的ななにか保存はしない
			DataCharaParam slime = new DataCharaParam();
			slime.chara_id = 1;
			slime.status = DataChara.STATUS.USING.ToString();
			dataChara.list.Add(slime);
		}
		dataWeapon.SetSaveFilename(Defines.FILENAME_DATAWEAPON);
		if( dataWeapon.LoadMulti()== false)
		{
			DataWeaponParam add_weapon = new DataWeaponParam();
			add_weapon.weapon_id = 1;
			add_weapon.level = 3;
			add_weapon.equip = 1;   // 武器はtfで良いのでは？

			DataWeaponParam add_weapon2 = new DataWeaponParam();
			add_weapon2.weapon_id = 2;
			add_weapon2.level = 1;
			add_weapon2.equip = 0;   // 武器はtfで良いのでは？

			dataWeapon.Add(add_weapon);
			dataWeapon.Add(add_weapon2);
		}
		dataWeaponAlbum.SetSaveFilename(Defines.FILENAME_DATAWEAPONALBUM);
		if (dataWeaponAlbum.LoadMulti() == false)
		{
			dataWeaponAlbum.AddAlbum(1);
			dataWeaponAlbum.AddAlbum(2);
			dataWeaponAlbum.AddAlbum(3);
			dataWeaponAlbum.AddAlbum(4);
		}


		dataArmor.SetSaveFilename(Defines.FILENAME_DATAARMOR);
		if( dataArmor.LoadMulti() == false)
		{
			for( int i = 0; i < MasterArmor.ArmorPositionArr.Length; i++)
			{
				DataArmorParam add = new DataArmorParam();
				add.position = MasterArmor.ArmorPositionArr[i];
				add.level = 1;
				MasterArmorParam mas = masterArmor.list.Find(p => p.position == add.position && p.level == add.level);
				add.armor_id = mas.armor_id;
				dataArmor.list.Add(add);
			}
		}
		dataSkill.SetSaveFilename(Defines.FILENAME_DATASKILL);
		if( dataSkill.LoadMulti() == false)
		{
			dataSkill.list.Add(new DataSkillParam(1, 1));
		}
		dataPotion.SetSaveFilename(Defines.FILENAME_DATAPOTION);
		if (dataPotion.LoadMulti() == false)
		{
			DataPotionParam add = new DataPotionParam();
			add.potion_id = 1;
			add.num = 9;
			add.is_use = true;
			dataPotion.list.Add(add);
		}

		dataTreasure.SetSaveFilename(Defines.FILENAME_DATATREASURE);
		if(dataTreasure.LoadMulti() == false)
		{
			dataTreasure.Add(1001);
		}
		dataTreasureAlbum.SetSaveFilename(Defines.FILENAME_DATATREASUREALBUM);
		if (dataTreasureAlbum.LoadMulti() == false)
		{
			dataTreasureAlbum.Add(1002);
			dataTreasureAlbum.Add(1003);
			dataTreasureAlbum.Add(1006);
			dataTreasureAlbum.Add(1010);
			dataTreasureAlbum.Add(1026);
		}
		dataScroll.SetSaveFilename(Defines.FILENAME_DATASCROLL);
		if( dataScroll.LoadMulti()== false)
		{
			dataScroll.list.Add(new DataScrollParam(1, 120));
			dataScroll.list.Add(new DataScrollParam(2, 120));
			dataScroll.list.Add(new DataScrollParam(3, 120));
			dataScroll.list.Add(new DataScrollParam(4, 120));
			dataScroll.list.Add(new DataScrollParam(5, 120));
		}
		dataUnit.SetSaveFilename(Defines.FILENAME_DATAUNIT);
		if( dataUnit.LoadMulti()== false)
		{

		}
		dataItem.SetSaveFilename(Defines.FILENAME_DATAITEM);
		if (dataItem.LoadMulti() == false)
		{

		}
		dataGetItem.SetSaveFilename(Defines.FILENAME_DATAITEM_GET);
		if (dataGetItem.LoadMulti() == false)
		{

		}
		dataStage.SetSaveFilename(Defines.FILENAME_DATASTAGE);
		if (dataStage.LoadMulti() == false)
		{
			DataStageParam data = new DataStageParam();
			data.stage_id = 1;
			data.status = 1;
			dataStage.list.Add(data);
			DataStageParam data2 = new DataStageParam();
			data2.stage_id = 2;
			data2.status = 1;
			dataStage.list.Add(data2);
		}
		dataFloor.SetSaveFilename(Defines.FILENAME_DATAFLOOR);
		if (dataFloor.LoadMulti() == false)
		{
			DataFloorParam data = new DataFloorParam();
			data.floor_id = 1;
			data.status = 2;
			dataFloor.list.Add(data);
			DataFloorParam data2 = new DataFloorParam();
			data2.floor_id = 2;
			data2.status = 1;
			dataFloor.list.Add(data2);
		}

		Initialized = true;
		Debug.Log("init_network end");
		yield return null;
	}
}


