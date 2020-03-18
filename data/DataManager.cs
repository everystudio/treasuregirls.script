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

	public bool UseGem(int _iGem)
	{
		// Gemの消費処理

		return true;
	}
	public bool UseGold(int _iGold)
	{
		// Gemの消費処理

		return true;
	}

	public int GetGold()
	{
		return 500;
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
		yield return StartCoroutine(masterTreasure.SpreadSheet(Defines.SS_MASTER, "treasure", () => { }));
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
			dataTreasureAlbum.Add(1001);
		}



		Initialized = true;
		Debug.Log("init_network end");
		yield return null;
	}
}


