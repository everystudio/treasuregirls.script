using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : DataManagerBase<DataManager>
{
	[HideInInspector]
	public bool Initialized
	{
		get;
		private set;
	}

	public TextAssetHolder m_textAssetHolder;
	public bool m_bLoadNetworkData;

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

	#region Camp
	public TextMeshProUGUI m_txtKey;
	public TextMeshProUGUI m_txtGoldKey;
	public TextMeshProUGUI m_txtCoin;
	public TextMeshProUGUI m_txtGem;
	#endregion

	public int debug_gem;
	public int debug_coin;
	public int debug_key;
	public int debug_gold_key;
	public bool UseGem(int _iGem)
	{
		// Gemの消費処理
		if( GetGem() < _iGem)
		{
			return false;
		}
		AddGem(-1 * _iGem);
		return true;
	}
	public int GetGem()
	{
		return user_data.ReadInt(Defines.KeyGem);
	}
	public void AddGem(int _iAdd)
	{
		user_data.AddInt(Defines.KeyGem, _iAdd);
		return;
	}
	public void AddCoin(int _iAdd)
	{
		user_data.AddInt(Defines.KeyCoin, _iAdd);
		return;
	}
	public void AddKey(int _iAdd)
	{
		user_data.AddInt(Defines.KeyKey, _iAdd);
		return;
	}
	public void AddGoldKey(int _iAdd)
	{
		user_data.AddInt(Defines.KeyGoldKey, _iAdd);
		return;
	}

	public bool UseCoin(int _iCoin)
	{
		// Gemの消費処理

		if(GetCoin() < _iCoin )
		{
			return false;
		}
		AddCoin(-1 * _iCoin);
		return true;
	}

	public int GetCoin()
	{
		return user_data.ReadInt(Defines.KeyCoin);
	}

	public bool UseKey(int _iUse)
	{
		if (GetKey() < _iUse)
		{
			return false;
		}
		AddKey(-1 * _iUse);
		return true;
	}
	public int GetKey()
	{
		return user_data.ReadInt(Defines.KeyKey);
	}

	public bool UseGoldKey(int _iUse)
	{
		if(GetGoldKey() < _iUse)
		{
			return false;
		}
		AddGoldKey(-1 * _iUse);
		return true;
	}
	public int GetGoldKey()
	{
		return user_data.ReadInt(Defines.KeyGoldKey);
	}

	public override void Initialize()
	{
		Initialized = false;

		#region Camp
		if(m_txtKey != null)
		{
			m_txtKey.text = "0";
			m_txtGoldKey.text = "0";
			m_txtCoin.text = "0";
			m_txtGem.text = "0";
		}
		#endregion

		base.Initialize();
		StartCoroutine(init_network());
	}

	IEnumerator init_network()
	{
		Debug.Log("init_network start");
		masterChara.SetSaveFilename(Defines.FILENAME_MASTERCHARA);

		masterChara.Load(m_textAssetHolder.Get("master_chara"));
		masterWeapon.Load(m_textAssetHolder.Get("master_weapon"));
		masterArmor.Load(m_textAssetHolder.Get("master_armor"));
		masterSkill.Load(m_textAssetHolder.Get("master_skill"));
		masterPotion.Load(m_textAssetHolder.Get("master_potion"));
		masterTreasure.Load(m_textAssetHolder.Get("master_treasure"));
		masterEnemy.Load(m_textAssetHolder.Get("master_enemy"));
		masterItem.Load(m_textAssetHolder.Get("master_item"));
		masterStage.Load(m_textAssetHolder.Get("master_stage"));
		masterFloor.Load(m_textAssetHolder.Get("master_floor"));


#if UNITY_EDITOR
		if (m_bLoadNetworkData)
		{
			#region 通信初期化
			yield return StartCoroutine(masterChara.SpreadSheet(Defines.SS_MASTER, "chara", () => { }));
			yield return StartCoroutine(masterWeapon.SpreadSheet(Defines.SS_MASTER, "weapon", () => { }));
			yield return StartCoroutine(masterArmor.SpreadSheet(Defines.SS_MASTER, "armor", () => { }));
			yield return StartCoroutine(masterSkill.SpreadSheet(Defines.SS_MASTER, "skill", () => { }));
			yield return StartCoroutine(masterPotion.SpreadSheet(Defines.SS_MASTER, "potion", () => { }));
			yield return StartCoroutine(masterTreasure.SpreadSheet(Defines.SS_MASTER, "treasure", () =>
			{
				/*
				foreach( MasterTreasureParam master in masterTreasure.list)
				{
					Debug.Log(string.Format("id:{0} name:{1} sprite:{2}", master.treasure_id , master.name , master.sprite_name));
				}
				*/
			}));
			yield return StartCoroutine(masterEnemy.SpreadSheet(Defines.SS_MASTER, "enemy", () => { }));
			yield return StartCoroutine(masterItem.SpreadSheet(Defines.SS_MASTER, "item", () => { }));
			yield return StartCoroutine(masterStage.SpreadSheet(Defines.SS_MASTER, "stage", () => { }));
			yield return StartCoroutine(masterFloor.SpreadSheet(Defines.SS_MASTER, "floor", () => { }));
			#endregion
		}
#endif
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
			dataSkill.list.Add(new DataSkillParam(6, 2));
			dataSkill.list.Add(new DataSkillParam(11, 3));
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


		if (m_txtKey != null)
		{
			user_data.AddListenerInt(Defines.KeyKey, (iValue) =>
			{
				m_txtKey.text = iValue.ToString();
			});
			user_data.AddListenerInt(Defines.KeyGoldKey, (iValue) =>
			{
				m_txtGoldKey.text = iValue.ToString();
			});
			user_data.AddListenerInt(Defines.KeyCoin, (iValue) =>
			{
				m_txtCoin.text = iValue.ToString();
			});
			user_data.AddListenerInt(Defines.KeyGem, (iValue) =>
			{
				m_txtGem.text = iValue.ToString();
			});

#if UNITY_EDITOR
			AddGem(debug_gem);
			AddCoin(debug_coin);
			AddKey(debug_key);
			AddGoldKey(debug_gold_key);
#endif
			//m_txtKey.text = user_data.ReadInt(Defines.KeyKey).ToString(); ;
			//m_txtGoldKey.text = user_data.Read(Defines.KeyGoldKey);
			//m_txtCoin.text = user_data.Read(Defines.KeyCoin);
			//m_txtGem.text = user_data.Read(Defines.KeyGem);


		}




		Initialized = true;
		Debug.Log("init_network end");
		yield return null;
	}
}


