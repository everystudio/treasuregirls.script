using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponMainAction
{
	[ActionCategory("WeaponMainAction")]
	[HutongGames.PlayMaker.Tooltip("WeaponMainAction")]
	public abstract class TreasureMainActionBase : FsmStateAction
	{
		protected WeaponMain main;
		public override void OnEnter()
		{
			base.OnEnter();
			main = Owner.GetComponent<WeaponMain>();
		}
	}

	[ActionCategory("WeaponMainAction")]
	[HutongGames.PlayMaker.Tooltip("WeaponMainAction")]
	public class idle : TreasureMainActionBase
	{
		public FsmInt weapon_serial;
		public override void OnEnter()
		{
			base.OnEnter();
			//weapon_serial.Value = 0;

			if (weapon_serial.Value == 0)
			{
				main.m_weaponInfo.Setup(new DataWeaponParam(0, 0), null);
			}
			else
			{
				DataWeaponParam data = DataManager.Instance.dataWeapon.list.Find(p => p.serial == weapon_serial.Value);
				MasterWeaponParam master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == data.weapon_id);
				main.m_weaponInfo.Setup(data,master);
			}

			main.m_btnBuy.gameObject.SetActive(true);
			main.m_btnBuy.onClick.AddListener(() =>
			{
				Fsm.Event("buy");
			});
			main.m_btnGradeup.gameObject.SetActive(true);
			main.m_btnGradeup.onClick.AddListener(() =>
			{
				DataWeaponParam data = DataManager.Instance.dataWeapon.list.Find(p => p.serial == weapon_serial.Value);
				MasterWeaponParam master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == data.weapon_id);
				DataManager.Instance.UseCoin(MasterWeapon.GetGradeupPrice(data, master));

				data.level += 1;
				main.m_weaponInfo.Setup(data, master);

				foreach(IconInventry icon in main.weapon_list)
				{
					if( icon.m_dataWeapon.serial == data.serial)
					{
						icon.Initialize(data, master);
						icon.SelectTreasure(data.serial);
					}
				}
			});

			main.m_btnEquip.gameObject.SetActive(true);
			main.m_btnEquip.interactable = false;
			main.m_btnEquip.onClick.AddListener(() =>
			{
				DataWeaponParam data_equip_pre = DataManager.Instance.dataWeapon.list.Find(p => 0 < p.equip);
				data_equip_pre.equip = 0;
				DataWeaponParam data_equip = DataManager.Instance.dataWeapon.list.Find(p => p.serial == weapon_serial.Value);
				data_equip.equip = 1;
				MasterWeaponParam master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == data_equip.weapon_id);

				main.m_weaponInfo.Setup(data_equip, master);

				foreach (IconInventry icon in main.weapon_list)
				{
					if (icon.m_dataWeapon.serial == data_equip_pre.serial)
					{
						icon.m_goEquip.SetActive(false);
					}
					if( icon.m_dataWeapon.serial == data_equip.serial)
					{
						icon.m_goEquip.SetActive(true);
					}
				}
				main.m_btnEquip.interactable = false;
			});

			main.m_btnAlbum.gameObject.SetActive(true);
			main.m_btnAlbum.onClick.AddListener(() =>
			{
				Fsm.Event("album");
			});

			main.weapon_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconInventry>(main.m_goListContents);
			foreach (DataWeaponParam data in DataManager.Instance.dataWeapon.list)
			{
				IconInventry icon = PrefabManager.Instance.MakeScript<IconInventry>(
					main.m_prefIconInventry,
					main.m_goListContents);
				MasterWeaponParam master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == data.weapon_id);

				icon.Initialize(data, master);
				main.weapon_list.Add(icon);

				icon.OnClickWeapon.AddListener(OnSelectListWeapon);
			}
			main.SelectListData(weapon_serial.Value);
		}

		private void OnSelectListWeapon(DataWeaponParam arg0)
		{
			weapon_serial.Value = arg0.serial;
			main.SelectListData(arg0.serial);

			MasterWeaponParam master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id== arg0.weapon_id);
			main.m_weaponInfo.Setup(arg0, master);

			DataWeaponParam data_equip = DataManager.Instance.dataWeapon.list.Find(p => 0 < p.equip);
			main.m_btnEquip.interactable = arg0.serial != data_equip.serial;
		}

		public override void OnExit()
		{
			base.OnExit();
			main.m_btnBuy.onClick.RemoveAllListeners();
			main.m_btnGradeup.onClick.RemoveAllListeners();
			main.m_btnEquip.onClick.RemoveAllListeners();
			main.m_btnAlbum.onClick.RemoveAllListeners();

		}
	}

	[ActionCategory("WeaponMainAction")]
	[HutongGames.PlayMaker.Tooltip("WeaponMainAction")]
	public class album : TreasureMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			main.m_btnEquip.gameObject.SetActive(false);
			main.m_btnAlbum.gameObject.SetActive(false);
			main.m_btnBuy.gameObject.SetActive(false);
			main.m_btnGradeup.gameObject.SetActive(false);

			main.m_btnList.gameObject.SetActive(true);
			main.m_btnList.onClick.AddListener(() =>
			{
				Finish();
			});

			main.weapon_list.Clear();
			MonoBehaviourEx.DeleteObjects<IconInventry>(main.m_goListContents);
			foreach (DataWeaponParam data in DataManager.Instance.dataWeaponAlbum.list)
			{
				IconInventry icon = PrefabManager.Instance.MakeScript<IconInventry>(
					main.m_prefIconInventry,
					main.m_goListContents);
				MasterWeaponParam master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == data.weapon_id);

				icon.Initialize(data, master);
				main.weapon_list.Add(icon);

				icon.OnClickWeapon.AddListener(OnSelectListWeapon);
			}
		}

		private void OnSelectListWeapon(DataWeaponParam arg0)
		{
			main.SelectListData_weapon_id(arg0.weapon_id);
			MasterWeaponParam master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == arg0.weapon_id);
			main.m_weaponInfo.Setup(arg0, master);
		}
		public override void OnExit()
		{
			base.OnExit();
			main.m_btnList.gameObject.SetActive(false);
			main.m_btnList.onClick.RemoveAllListeners();
		}
	}


	[ActionCategory("WeaponMainAction")]
	[HutongGames.PlayMaker.Tooltip("WeaponMainAction")]
	public class buycheck : TreasureMainActionBase
	{
		// 今更だけど買うと売るを逆にしてない？
		public FsmInt weapon_serial;

		public override void OnEnter()
		{
			base.OnEnter();

			main.m_goBuyWindow.SetActive(true);

			DataWeaponParam data = DataManager.Instance.dataWeapon.list.Find(p => p.serial == weapon_serial.Value);
			MasterWeaponParam master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == data.weapon_id);

			main.icon_buy.Initialize(data, master);
			main.m_txtPrice.text = MasterWeapon.GetSellPrice(data, master).ToString();

			main.m_btnBuyYes.onClick.AddListener(() =>
			{
				DataWeaponParam remove_data = DataManager.Instance.dataWeapon.list.Find(p => p.serial == weapon_serial.Value);
				MasterWeaponParam remove_master = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == remove_data.weapon_id);

				int add_gold = MasterWeapon.GetSellPrice(remove_data, remove_master);
				DataManager.Instance.AddCoin(add_gold);
				DataManager.Instance.dataWeapon.list.Remove(remove_data);

				Fsm.Event("buy");
			});
			main.m_btnBuyCancel.onClick.AddListener(() =>
			{
				Fsm.Event("cancel");
			});

		}
		public override void OnExit()
		{
			base.OnExit();
			main.m_btnBuyYes.onClick.RemoveAllListeners();
			main.m_btnBuyCancel.onClick.RemoveAllListeners();
			main.m_goBuyWindow.SetActive(false);

		}


	}




}
