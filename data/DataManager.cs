﻿using System;
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


	public DataChara dataChara = new DataChara();
	public DataArmor dataArmor = new DataArmor();

	public bool UseGem( int _iGem)
	{
		// Gemの消費処理

		return true;
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
				dataArmor.list.Add(add);
			}
		}




		Initialized = true;
		Debug.Log("init_network end");
		yield return null;
	}
}


