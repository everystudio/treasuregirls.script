using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureMainAction
{
	[ActionCategory("TreasureMainAction")]
	[HutongGames.PlayMaker.Tooltip("TreasureMainAction")]
	public abstract class TreasureMainActionBase : FsmStateAction
	{
		protected TreasureMain treasureMain;
		public override void OnEnter()
		{
			base.OnEnter();
			treasureMain = Owner.GetComponent<TreasureMain>();
			// 毎回閉じるか
			treasureMain.ButtonClose();
		}

		public void SetupEquip()
		{

			treasureMain.m_prefIconInventry.SetActive(false);
			treasureMain.equip_treasure_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconInventry>(treasureMain.m_rootSettingTreasure);
			for (int i = 0; i < 3; i++)
			{
				IconInventry icon = PrefabManager.Instance.MakeScript<IconInventry>(treasureMain.m_prefIconInventry, treasureMain.m_rootSettingTreasure);

				DataTreasureParam equip_data = DataManager.Instance.dataTreasure.list.Find(p => p.equip == i + 1);
				if (equip_data != null)
				{
					MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == equip_data.treasure_id);
					icon.Initialize(equip_data, master);
				}
				else
				{
					equip_data = new DataTreasureParam();
					equip_data.equip = i + 1;
					equip_data.treasure_id = 0;
					icon.Initialize(equip_data, null);
				}

				treasureMain.equip_treasure_list.Add(icon);
				icon.OnClickTreasure.AddListener(OnSelectEquipBase);
			}
			treasureMain.SelectEquip(0);
		}

		private void OnSelectEquipBase(DataTreasureParam arg0)
		{
			OnSelectEquip(arg0);
		}

		// 必要があれば継承先でここをつかう
		public virtual void OnSelectEquip(DataTreasureParam arg0)
		{
		}
	}

	[ActionCategory("TreasureMainAction")]
	[HutongGames.PlayMaker.Tooltip("TreasureMainAction")]
	public class idle : TreasureMainActionBase
	{
		public FsmInt treasure_serial;
		public override void OnEnter()
		{
			base.OnEnter();

			treasure_serial.Value = 0;
			treasureMain.m_txtListTitle.text = "所持おたから一覧";

			treasureMain.m_goBuyWindow.SetActive(false);

			treasureMain.m_treasureInfo.Setup(new DataTreasureParam(0, 0), null);
			treasureMain.m_btnBuy.gameObject.SetActive(true);
			treasureMain.m_btnGradeup.gameObject.SetActive(true);

			treasureMain.m_btnGradeup.onClick.AddListener(() =>
			{
				DataTreasureParam data = DataManager.Instance.dataTreasure.list.Find(p => p.serial == treasure_serial.Value);
				MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == data.treasure_id);

				DataManager.Instance.UseCoin(MasterTreasure.GetGradeupPrice(data, master));
				data.level += 1;
				treasureMain.m_treasureInfo.Setup(data, master);

				foreach(IconInventry icon in treasureMain.treasure_list)
				{
					if( icon.m_dataTreasure.serial == data.serial)
					{
						icon.Initialize(data, master);
						icon.SelectTreasure(data.serial);
					}
				}
				foreach( IconInventry icon in treasureMain.equip_treasure_list)
				{
					if (icon.m_dataTreasure.serial == data.serial)
					{
						icon.Initialize(data, master);
						icon.SelectTreasure(data.serial);
					}
				}
			});
			treasureMain.m_btnBuy.onClick.AddListener(() =>
			{
				Fsm.Event("buy");
			});



			treasureMain.m_btnEdit.gameObject.SetActive(true);
			treasureMain.m_btnAlbum.gameObject.SetActive(true);

			treasureMain.m_txtListTitle.text = "所持おたから一覧";
			treasureMain.m_btnEdit.onClick.AddListener(() =>
			{
				Fsm.Event("edit");
			});
			treasureMain.m_btnAlbum.onClick.AddListener(() =>
			{
				Fsm.Event("album");
			});

			SetupEquip();

			treasureMain.treasure_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconInventry>(treasureMain.m_goListContents);
			foreach ( DataTreasureParam treasure in DataManager.Instance.dataTreasure.list)
			{
				IconInventry icon = PrefabManager.Instance.MakeScript<IconInventry>(treasureMain.m_prefIconInventry, treasureMain.m_goListContents);
				MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == treasure.treasure_id);

				icon.Initialize(treasure, master);
				treasureMain.treasure_list.Add(icon);

				icon.OnClickTreasure.AddListener(OnSelectListTreasure);
			}
		}

		public override void OnSelectEquip(DataTreasureParam arg0)
		{
			MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == arg0.treasure_id);

			treasureMain.m_treasureInfo.Setup(arg0, master);

			treasureMain.SelectEquip(arg0.equip);
			treasureMain.SelectListData(0);

			treasure_serial.Value = arg0.serial;
		}


		private void OnSelectListTreasure(DataTreasureParam arg0)
		{
			treasureMain.SelectEquip(0);
			treasureMain.SelectListData(arg0.serial);

			MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == arg0.treasure_id);
			treasureMain.m_treasureInfo.Setup(arg0, master);

			treasure_serial.Value = arg0.serial;
		}
	}

	[ActionCategory("TreasureMainAction")]
	[HutongGames.PlayMaker.Tooltip("TreasureMainAction")]
	public class edit : TreasureMainActionBase
	{
		public int equip_position;
		public int treasure_serial;
		public override void OnEnter()
		{
			base.OnEnter();
			treasureMain.m_txtListTitle.text = "おたから編集";

			equip_position = -1;
			treasure_serial = -1;

			SetupEquip();

			treasureMain.m_btnBuy.gameObject.SetActive(false);
			treasureMain.m_btnGradeup.gameObject.SetActive(false);
			treasureMain.m_btnGradeup.onClick.AddListener(() =>
			{
				DataTreasureParam data = DataManager.Instance.dataTreasure.list.Find(p => p.serial == treasure_serial);
				MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == data.treasure_id);

				DataManager.Instance.UseCoin(MasterTreasure.GetGradeupPrice(data, master));
				data.level += 1;
				treasureMain.m_treasureInfo.Setup(data, master);

				foreach (IconInventry icon in treasureMain.treasure_list)
				{
					if (icon.m_dataTreasure.serial == data.serial)
					{
						icon.Initialize(data, master);
						icon.SelectTreasure(data.serial);
					}
				}
				foreach (IconInventry icon in treasureMain.equip_treasure_list)
				{
					if (icon.m_dataTreasure.serial == data.serial)
					{
						icon.Initialize(data, master);
						icon.SelectTreasure(data.serial);
					}
				}

			});




			treasureMain.m_btnSet.gameObject.SetActive(true);
			treasureMain.m_btnBack.gameObject.SetActive(true);

			treasureMain.m_btnSet.interactable = false;

			treasureMain.m_btnSet.onClick.AddListener(() =>
			{
				DataTreasureParam temp_data = DataManager.Instance.dataTreasure.list.Find(p => p.serial == treasure_serial);

				DataTreasureParam preset_data = DataManager.Instance.dataTreasure.list.Find(p => p.equip == equip_position);

				if(preset_data != null && temp_data != null)
				{
					if( preset_data.serial == temp_data.serial)
					{
						// なにもしない
					}
					else if(temp_data.equip != 0)
					{
						int temp_position = temp_data.equip;
						temp_data.equip = preset_data.equip;
						preset_data.equip = temp_position;
					}
					else
					{
						temp_data.equip = equip_position;
					}
				}
				else if( temp_data != null)
				{
					temp_data.equip = equip_position;
				}
				else if(preset_data != null && temp_data == null)
				{
					// 外す
					preset_data.equip = 0;
				}
				else
				{
					// 入らないんじゃない？
				}
				Fsm.Event("equip");
			});
			treasureMain.m_btnBack.onClick.AddListener(() =>
			{
				Fsm.Event("back");
			});
			treasureMain.treasure_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconInventry>(treasureMain.m_goListContents);
			IconInventry remove_icon = PrefabManager.Instance.MakeScript<IconInventry>(treasureMain.m_prefIconInventry, treasureMain.m_goListContents);
			remove_icon.Initialize(new DataTreasureParam(0, 0), null);
			remove_icon.m_txtNotInventry.text = "はずす";
			remove_icon.OnClickTreasure.AddListener(OnSelectListTreasure);
			treasureMain.treasure_list.Add(remove_icon);
			foreach (DataTreasureParam treasure in DataManager.Instance.dataTreasure.list)
			{
				IconInventry icon = PrefabManager.Instance.MakeScript<IconInventry>(treasureMain.m_prefIconInventry, treasureMain.m_goListContents);
				MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == treasure.treasure_id);

				icon.Initialize(treasure, master);
				treasureMain.treasure_list.Add(icon);

				icon.OnClickTreasure.AddListener(OnSelectListTreasure);
			}
		}

		private bool CheckExchangeButton()
		{
			DataTreasureParam equip_data = DataManager.Instance.dataTreasure.list.Find(p => p.equip == equip_position);
			DataTreasureParam list_data = DataManager.Instance.dataTreasure.list.Find(p => p.serial == treasure_serial);

			if( list_data != null)
			{
				return true;
			}
			else if( list_data != null || equip_data != null)
			{
				return true;
			}
			else
			{
				return false;
			}


		}
		public override void OnSelectEquip(DataTreasureParam arg0)
		{
			equip_position = arg0.equip;
			treasureMain.SelectEquip(arg0.equip);


			MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == arg0.treasure_id);
			treasureMain.m_treasureInfo.Setup(arg0, master);

			treasureMain.m_btnSet.interactable = CheckExchangeButton();
		}

		private void OnSelectListTreasure(DataTreasureParam arg0)
		{
			treasure_serial = arg0.serial;
			treasureMain.SelectListData(arg0.serial);
			treasureMain.m_btnSet.interactable = CheckExchangeButton();

			MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == arg0.treasure_id);
			treasureMain.m_treasureInfo.Setup(arg0, master);

		}
	}

	[ActionCategory("TreasureMainAction")]
	[HutongGames.PlayMaker.Tooltip("TreasureMainAction")]
	public class album : TreasureMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			treasureMain.m_txtListTitle.text = "おたから図鑑";

			treasureMain.m_btnBack.gameObject.SetActive(true);
			treasureMain.m_btnBuy.gameObject.SetActive(false);
			treasureMain.m_btnGradeup.gameObject.SetActive(false);

			SetupEquip();

			treasureMain.m_btnBack.onClick.AddListener(() =>
			{
				Fsm.Event("back");
			});

			treasureMain.treasure_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconInventry>(treasureMain.m_goListContents);
			foreach (DataTreasureParam treasure in DataManager.Instance.dataTreasureAlbum.list)
			{
				IconInventry icon = PrefabManager.Instance.MakeScript<IconInventry>(treasureMain.m_prefIconInventry, treasureMain.m_goListContents);
				MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == treasure.treasure_id);

				icon.Initialize(treasure, master);
				treasureMain.treasure_list.Add(icon);

				icon.OnClickTreasure.AddListener(OnSelectListTreasure);
			}

		}

		private void OnSelectListTreasure(DataTreasureParam arg0)
		{
			treasureMain.SelectListData(arg0.serial);
			MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == arg0.treasure_id);
			treasureMain.m_treasureInfo.Setup(arg0, master);
		}
	}


	[ActionCategory("TreasureMainAction")]
	[HutongGames.PlayMaker.Tooltip("TreasureMainAction")]
	public class buy_check : TreasureMainActionBase
	{
		// 今更だけど買うと売るを逆にしてない？
		public FsmInt treasure_serial;

		public override void OnEnter()
		{
			base.OnEnter();
			treasureMain.m_goBuyWindow.SetActive(true);

			DataTreasureParam data = DataManager.Instance.dataTreasure.list.Find(p => p.serial == treasure_serial.Value);
			MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == data.treasure_id);

			treasureMain.icon_buy.Initialize(data, master);
			treasureMain.m_txtPrice.text = MasterTreasure.GetSellPrice(data, master).ToString();

			treasureMain.m_btnBuyYes.onClick.AddListener(() =>
			{
				DataTreasureParam remove_data = DataManager.Instance.dataTreasure.list.Find(p => p.serial == treasure_serial.Value);
				MasterTreasureParam remove_master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == remove_data.treasure_id);

				int add_gold = MasterTreasure.GetSellPrice(remove_data, remove_master);
				DataManager.Instance.AddCoin(add_gold);
				DataManager.Instance.dataTreasure.list.Remove(remove_data);

				Fsm.Event("buy");
			});
			treasureMain.m_btnBuyCancel.onClick.AddListener(() =>
			{
				Fsm.Event("cancel");
			});



		}

		public override void OnExit()
		{
			base.OnExit();
			treasureMain.m_btnBuyYes.onClick.RemoveAllListeners();
			treasureMain.m_btnBuyCancel.onClick.RemoveAllListeners();
		}

	}



}
