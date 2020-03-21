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

	public MasterChara masterChara = new MasterChara();
	public MasterArmor masterArmor = new MasterArmor();
	public MasterSkill masterSkill = new MasterSkill();
	public MasterPotion masterPotion = new MasterPotion();
	public MasterTreasure masterTreasure = new MasterTreasure();

	public DataChara dataChara = new DataChara();
	public DataArmor dataArmor = new DataArmor();
	public DataSkill dataSkill = new DataSkill();
	public DataPotion dataPotion = new DataPotion();
	public DataTreasure dataTreasure = new DataTreasure();
	public DataTreasure dataTreasureAlbum = new DataTreasure();
	public DataScroll dataScroll = new DataScroll();

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
		#endregion

		dataChara.SetSaveFilename(Defines.FILENAME_DATACHARA);
		if( dataChara.LoadMulti() == false)
		{
			// 初期データ的ななにか保存はしない
			DataCharaParam slime = new DataCharaParam();
			slime.chara_id = 1;
			slime.status = DataChara.STATUS.USING.ToString();
			dataChara.list.Add(slime);
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
			dataScroll.list.Add(new DataScrollParam(1, 20));
			dataScroll.list.Add(new DataScrollParam(2, 20));
			dataScroll.list.Add(new DataScrollParam(3, 20));
			dataScroll.list.Add(new DataScrollParam(4, 20));
			dataScroll.list.Add(new DataScrollParam(5, 20));
		}



		Initialized = true;
		Debug.Log("init_network end");
		yield return null;
	}
}


