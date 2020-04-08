using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopMainAction
{
	[ActionCategory("ShopMainAction")]
	[HutongGames.PlayMaker.Tooltip("ShopMainAction")]
	public abstract class ShopMainActionBase : FsmStateAction
	{
		protected ShopMain shop;
		public override void OnEnter()
		{
			base.OnEnter();
			shop = Owner.GetComponent<ShopMain>();


			shop.m_goBannerGemFree.SetActive(false);
			shop.m_goBannerGoldPack1.SetActive(false);
			shop.m_goBannerGoldPack2.SetActive(false);
			shop.m_goBannerGoldPack3.SetActive(false);
			shop.m_goBannerWeaponFree.SetActive(false);
			shop.m_goBannerWeapon1.SetActive(false);
			shop.m_goBannerWeapon2.SetActive(false);
			shop.m_goBannerTreasureFree.SetActive(false);
			shop.m_goBannerTreasure1.SetActive(false);
			shop.m_goBannerTreasure2.SetActive(false);

			shop.m_goScrollInfoRoot.SetActive(false);
			shop.m_goScrollCreateRoot.SetActive(false);
			shop.m_goScrollUseRoot.SetActive(false);

			shop.m_btnMainGem.onClick.RemoveAllListeners();
			shop.m_btnMainGold.onClick.RemoveAllListeners();
			shop.m_btnMainWeapon.onClick.RemoveAllListeners();
			shop.m_btnMainTreasure.onClick.RemoveAllListeners();
			shop.m_btnMainScroll.onClick.RemoveAllListeners();


			shop.m_btnMainGem.onClick.AddListener(() => { Fsm.Event("gem"); });
			shop.m_btnMainGold.onClick.AddListener(() => { Fsm.Event("gold"); });
			shop.m_btnMainWeapon.onClick.AddListener(() => { Fsm.Event("weapon"); });
			shop.m_btnMainTreasure.onClick.AddListener(() => { Fsm.Event("treasure"); });
			shop.m_btnMainScroll.onClick.AddListener(() => { Fsm.Event("scroll"); });

		}

		public void ScrollInfoUpdate()
		{
			shop.m_txtScrollNumBlue.text = DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 1).num.ToString();
			shop.m_txtScrollNumYellow.text = DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 2).num.ToString();
			shop.m_txtScrollNumGreen.text = DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 3).num.ToString();
			shop.m_txtScrollNumPurple.text = DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 4).num.ToString();
			shop.m_txtScrollNumRed.text = DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 5).num.ToString();
		}

	}

	[ActionCategory("ShopMainAction")]
	[HutongGames.PlayMaker.Tooltip("ShopMainAction")]
	public class gem : ShopMainActionBase
	{

		private DateTime check_datetime;
		public override void OnEnter()
		{
			base.OnEnter();
			shop.m_goBannerGemFree.SetActive(true);

			shop.m_goGemFreeButtonRoot.SetActive(false);
			shop.m_goGemFreeTimeRoot.SetActive(false);

			if (!DataManager.Instance.user_data.HasKey(Defines.KEY_LAST_REWARD_TIME_GEM_FREE))
			{
				check_datetime = new DateTime(2020, 1, 1);
			}
			else
			{
				check_datetime = System.DateTime.Parse(DataManager.Instance.user_data.Read(Defines.KEY_LAST_REWARD_TIME_GEM_FREE));
			}

			shop.m_btnGemFree.onClick.AddListener(() =>
			{
				DataManager.Instance.user_data.Write(
					Defines.KEY_LAST_REWARD_TIME_GEM_FREE,
					NTPTimer.Instance.now.ToString("yyyy/MM/dd HH:mm:ss"));
				check_datetime = System.DateTime.Parse(DataManager.Instance.user_data.Read(Defines.KEY_LAST_REWARD_TIME_GEM_FREE));

			});
		}

		public override void OnUpdate()
		{
			base.OnUpdate();


			TimeSpan ts = NTPTimer.Instance.now - check_datetime;

			int last_second = (60 * 5) - (int)(ts.TotalSeconds);

			if (0 < last_second)
			{
				shop.m_goGemFreeButtonRoot.SetActive(false);
				shop.m_goGemFreeTimeRoot.SetActive(true);
				shop.m_txtGemFreeTime.text = string.Format("{0}:{1:00}", last_second / 60, last_second % 60);
				//btnMedal.m_txtLimitTime.text = string.Format("あと{0}:{1:00}", last_second / 60, last_second % 60);
			}
			else
			{
				shop.m_goGemFreeButtonRoot.SetActive(true);
				shop.m_goGemFreeTimeRoot.SetActive(false);
			}

		}
		public override void OnExit()
		{
			base.OnExit();

			shop.m_btnGemFree.onClick.RemoveAllListeners();

		}
	}
	[ActionCategory("ShopMainAction")]
	[HutongGames.PlayMaker.Tooltip("ShopMainAction")]
	public class gold : ShopMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			shop.m_goBannerGoldPack1.SetActive(true);
			shop.m_goBannerGoldPack2.SetActive(true);
			shop.m_goBannerGoldPack3.SetActive(true);

			button_interactable();

			shop.m_btnGoldPack1.onClick.AddListener(() =>
			{
				DataManager.Instance.UseGem(20);
				DataManager.Instance.AddGold(500);
				button_interactable();
			});
			shop.m_btnGoldPack2.onClick.AddListener(() =>
			{
				DataManager.Instance.UseGem(100);
				DataManager.Instance.AddGold(3000);
				button_interactable();

			});
			shop.m_btnGoldPack3.onClick.AddListener(() =>
			{
				DataManager.Instance.UseGem(500);
				DataManager.Instance.AddGold(20000);
				button_interactable();

			});
		}


		private void button_interactable()
		{
			int current_gem = DataManager.Instance.GetGem();
			shop.m_btnGoldPack1.interactable = 20 <= current_gem;
			shop.m_btnGoldPack2.interactable = 100 <= current_gem;
			shop.m_btnGoldPack3.interactable = 500 <= current_gem;
		}
		public override void OnExit()
		{
			base.OnExit();
			shop.m_btnGoldPack1.onClick.RemoveAllListeners();
			shop.m_btnGoldPack2.onClick.RemoveAllListeners();
			shop.m_btnGoldPack3.onClick.RemoveAllListeners();
		}


	}
	[ActionCategory("ShopMainAction")]
	[HutongGames.PlayMaker.Tooltip("ShopMainAction")]
	public class weapon : ShopMainActionBase
	{
		private DateTime check_datetime;
		public override void OnEnter()
		{
			base.OnEnter();
			shop.m_goBannerWeaponFree.SetActive(true);
			shop.m_goBannerWeapon1.SetActive(true);
			shop.m_goBannerWeapon2.SetActive(true);

			if (!DataManager.Instance.user_data.HasKey(Defines.KEY_LAST_REWARD_TIME_FREE_WEAPON))
			{
				check_datetime = new DateTime(2020, 1, 1);
			}
			else
			{
				check_datetime = System.DateTime.Parse(DataManager.Instance.user_data.Read(Defines.KEY_LAST_REWARD_TIME_FREE_WEAPON));
			}

			button_interactable();

			shop.m_goWeaponFreeButtonRoot.SetActive(false);
			shop.m_goWeaponFreeTimeRoot.SetActive(false);

			shop.m_btnWeaponFree.onClick.AddListener(() =>
			{
				DataManager.Instance.user_data.Write(
					Defines.KEY_LAST_REWARD_TIME_FREE_WEAPON,
					NTPTimer.Instance.now.ToString("yyyy/MM/dd HH:mm:ss"));
				check_datetime = System.DateTime.Parse(DataManager.Instance.user_data.Read(Defines.KEY_LAST_REWARD_TIME_FREE_WEAPON));

			});
			shop.m_btnWeapon1.onClick.AddListener(() =>
			{
				DataManager.Instance.UseGem(50);
				button_interactable();
			});
			shop.m_btnWeapon2.onClick.AddListener(() =>
			{
				DataManager.Instance.UseGem(450);
				button_interactable();
			});
		}

		private void button_interactable()
		{
			int correct_gem = DataManager.Instance.GetGem();
			shop.m_btnWeapon1.interactable = 50 <= correct_gem;
			shop.m_btnWeapon2.interactable = 450 <= correct_gem;
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			TimeSpan ts = NTPTimer.Instance.now - check_datetime;
			int last_second = (60 * 5) - (int)(ts.TotalSeconds);
			if (0 < last_second)
			{
				shop.m_goWeaponFreeButtonRoot.SetActive(false);
				shop.m_goWeaponFreeTimeRoot.SetActive(true);
				shop.m_txtWeaponFreeTime.text = string.Format("{0}:{1:00}", last_second / 60, last_second % 60);
				//btnMedal.m_txtLimitTime.text = string.Format("あと{0}:{1:00}", last_second / 60, last_second % 60);
			}
			else
			{
				shop.m_goWeaponFreeButtonRoot.SetActive(true);
				shop.m_goWeaponFreeTimeRoot.SetActive(false);
			}
		}

		public override void OnExit()
		{
			base.OnExit();
			shop.m_btnWeaponFree.onClick.RemoveAllListeners();
			shop.m_btnWeapon1.onClick.RemoveAllListeners();
			shop.m_btnWeapon2.onClick.RemoveAllListeners();
		}
	}
	[ActionCategory("ShopMainAction")]
	[HutongGames.PlayMaker.Tooltip("ShopMainAction")]
	public class treasure : ShopMainActionBase
	{
		private DateTime check_datetime;
		public override void OnEnter()
		{
			base.OnEnter();

			shop.m_goBannerTreasureFree.SetActive(true);
			shop.m_goBannerTreasure1.SetActive(true);
			shop.m_goBannerTreasure2.SetActive(true);
			if (!DataManager.Instance.user_data.HasKey(Defines.KEY_LAST_REWARD_TIME_FREE_TREASURE))
			{
				check_datetime = new DateTime(2020, 1, 1);
			}
			else
			{
				check_datetime = System.DateTime.Parse(DataManager.Instance.user_data.Read(Defines.KEY_LAST_REWARD_TIME_FREE_TREASURE));
			}
			button_interactable();
			shop.m_goTreasureFreeButtonRoot.SetActive(false);
			shop.m_goTreasureFreeTimeRoot.SetActive(false);

			shop.m_btnTreasureFree.onClick.AddListener(() =>
			{
				DataManager.Instance.user_data.Write(
					Defines.KEY_LAST_REWARD_TIME_FREE_TREASURE,
					NTPTimer.Instance.now.ToString("yyyy/MM/dd HH:mm:ss"));
				check_datetime = System.DateTime.Parse(DataManager.Instance.user_data.Read(Defines.KEY_LAST_REWARD_TIME_FREE_TREASURE));

			});
			shop.m_btnTreasure1.onClick.AddListener(() =>
			{
				DataManager.Instance.UseKey(50);
				button_interactable();
			});
			shop.m_btnTreasure2.onClick.AddListener(() =>
			{
				DataManager.Instance.UseGoldKey(50);
				button_interactable();
			});

		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			TimeSpan ts = NTPTimer.Instance.now - check_datetime;
			int last_second = (60 * 5) - (int)(ts.TotalSeconds);
			if (0 < last_second)
			{
				shop.m_goTreasureFreeButtonRoot.SetActive(false);
				shop.m_goTreasureFreeTimeRoot.SetActive(true);
				shop.m_txtTreasureFreeTime.text = string.Format("{0}:{1:00}", last_second / 60, last_second % 60);
				//btnMedal.m_txtLimitTime.text = string.Format("あと{0}:{1:00}", last_second / 60, last_second % 60);
			}
			else
			{
				shop.m_goTreasureFreeButtonRoot.SetActive(true);
				shop.m_goTreasureFreeTimeRoot.SetActive(false);
			}
		}

		private void button_interactable()
		{
			shop.m_btnTreasure1.interactable = 50 <= DataManager.Instance.GetKey();
			shop.m_btnTreasure2.interactable = 50 <= DataManager.Instance.GetGoldKey();
		}
		public override void OnExit()
		{
			base.OnExit();
			shop.m_btnTreasureFree.onClick.RemoveAllListeners();
			shop.m_btnTreasure1.onClick.RemoveAllListeners();
			shop.m_btnTreasure2.onClick.RemoveAllListeners();

		}
	}
	[ActionCategory("ShopMainAction")]
	[HutongGames.PlayMaker.Tooltip("ShopMainAction")]
	public class scroll_use : ShopMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			shop.m_goScrollUseRoot.SetActive(true);
			shop.m_goScrollInfoRoot.SetActive(true);

			ScrollInfoUpdate();

			shop.m_btnScroolUseBlueOne.onClick.AddListener(() => { Use(1, 1); });
			shop.m_btnScroolUseBlueTen.onClick.AddListener(() => { Use(1, 10); });
			shop.m_btnScroolUseYellowOne.onClick.AddListener(() => { Use(2, 1); });
			shop.m_btnScroolUseYellowTen.onClick.AddListener(() => { Use(2, 10); });
			shop.m_btnScroolUseGreenOne.onClick.AddListener(() => { Use(3, 1); });
			shop.m_btnScroolUseGreanTen.onClick.AddListener(() => { Use(3, 10); });
			shop.m_btnScroolUsePurpleOne.onClick.AddListener(() => { Use(4, 1); });
			shop.m_btnScroolUsePurpleTen.onClick.AddListener(() => { Use(4, 10); });
			shop.m_btnScroolUseRedOne.onClick.AddListener(() => { Use(5, 1); });
			shop.m_btnScroolUseRedTen.onClick.AddListener(() => { Use(5, 10); });

			ButtonInteractable();


			shop.m_btnScrollCreate.gameObject.GetComponent<Animator>().SetBool("select", false);
			shop.m_btnScrollUse.gameObject.GetComponent<Animator>().SetBool("select", true);

			shop.m_btnScrollCreate.onClick.AddListener(() =>
			{
				Fsm.Event("change");
			});
		}

		public void Use( int _iScrollId , int _iCreateNum)
		{
			DataScrollParam data = DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == _iScrollId);
			data.num -= _iCreateNum * 10;


			ScrollInfoUpdate();
			ButtonInteractable();
		}

		public void ButtonInteractable()
		{
			shop.m_btnScroolUseBlueOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 1).num;
			shop.m_btnScroolUseBlueTen.interactable = 100<= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 1).num;
			shop.m_btnScroolUseYellowOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 2).num;
			shop.m_btnScroolUseYellowTen.interactable = 100<= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 2).num;
			shop.m_btnScroolUseGreenOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 3).num;
			shop.m_btnScroolUseGreanTen.interactable = 100<= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 3).num;
			shop.m_btnScroolUsePurpleOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 4).num;
			shop.m_btnScroolUsePurpleTen.interactable = 100<= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 4).num;
			shop.m_btnScroolUseRedOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 5).num;
			shop.m_btnScroolUseRedTen.interactable = 100<= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 5).num;
		}

		public override void OnExit()
		{
			base.OnExit();
			shop.m_btnScrollCreate.onClick.RemoveAllListeners();

			shop.m_btnScroolUseBlueOne.onClick.RemoveAllListeners();
			shop.m_btnScroolUseBlueTen.onClick.RemoveAllListeners();
			shop.m_btnScroolUseYellowOne.onClick.RemoveAllListeners();
			shop.m_btnScroolUseYellowTen.onClick.RemoveAllListeners();
			shop.m_btnScroolUseGreenOne.onClick.RemoveAllListeners();
			shop.m_btnScroolUseGreanTen.onClick.RemoveAllListeners();
			shop.m_btnScroolUsePurpleOne.onClick.RemoveAllListeners();
			shop.m_btnScroolUsePurpleTen.onClick.RemoveAllListeners();
			shop.m_btnScroolUseRedOne.onClick.RemoveAllListeners();
			shop.m_btnScroolUseRedTen.onClick.RemoveAllListeners();
			shop.m_btnScrollCreate.onClick.RemoveAllListeners();

		}

	}
	[ActionCategory("ShopMainAction")]
	[HutongGames.PlayMaker.Tooltip("ShopMainAction")]
	public class scroll_create : ShopMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			shop.m_goScrollCreateRoot.SetActive(true);
			shop.m_goScrollInfoRoot.SetActive(true);

			ScrollInfoUpdate();
			ButtonInteractable();
			shop.m_btnScrollCreate.gameObject.GetComponent<Animator>().SetBool("select", true);
			shop.m_btnScrollUse.gameObject.GetComponent<Animator>().SetBool("select", false);

			shop.m_btnScrollUse.onClick.AddListener(() =>
			{
				Fsm.Event("change");
			});

			shop.m_btnScroolCreateYellowOne.onClick.AddListener(() => { Create(1, 1); });
			shop.m_btnScroolCreateYellowTen.onClick.AddListener(() => { Create(1, 10); });
			shop.m_btnScroolCreateGreenOne.onClick.AddListener(() => { Create(2, 1); });
			shop.m_btnScroolCreateGreanTen.onClick.AddListener(() => { Create(2, 10); });
			shop.m_btnScroolCreatePurpleOne.onClick.AddListener(() => { Create(3, 1); });
			shop.m_btnScroolCreatePurpleTen.onClick.AddListener(() => { Create(3, 10); });
			shop.m_btnScroolCreateRedOne.onClick.AddListener(() => { Create(4, 1); });
			shop.m_btnScroolCreateRedTen.onClick.AddListener(() => { Create(4, 10); });


		}
		public void Create( int _iScrollId , int _iNum)
		{
			// idは消費する方のid
			DataScrollParam data = DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == _iScrollId);
			DataScrollParam next = DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == _iScrollId+1);

			data.num -= _iNum * 10;
			next.num += _iNum;

			ScrollInfoUpdate();
			ButtonInteractable();
		}


		public void ButtonInteractable()
		{
			shop.m_btnScroolCreateYellowOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 1).num;
			shop.m_btnScroolCreateYellowTen.interactable = 100 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 1).num;
			shop.m_btnScroolCreateGreenOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 2).num;
			shop.m_btnScroolCreateGreanTen.interactable = 100 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 2).num;
			shop.m_btnScroolCreatePurpleOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 3).num;
			shop.m_btnScroolCreatePurpleTen.interactable = 100 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 3).num;
			shop.m_btnScroolCreateRedOne.interactable = 10 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 4).num;
			shop.m_btnScroolCreateRedTen.interactable = 100 <= DataManager.Instance.dataScroll.list.Find(p => p.scroll_id == 4).num;
		}
		public override void OnExit()
		{
			base.OnExit();
			shop.m_btnScrollUse.onClick.RemoveAllListeners();

			shop.m_btnScroolCreateYellowOne.onClick.RemoveAllListeners();
			shop.m_btnScroolCreateYellowTen.onClick.RemoveAllListeners();
			shop.m_btnScroolCreateGreenOne.onClick.RemoveAllListeners();
			shop.m_btnScroolCreateGreanTen.onClick.RemoveAllListeners();
			shop.m_btnScroolCreatePurpleOne.onClick.RemoveAllListeners();
			shop.m_btnScroolCreatePurpleTen.onClick.RemoveAllListeners();
			shop.m_btnScroolCreateRedOne.onClick.RemoveAllListeners();
			shop.m_btnScroolCreateRedTen.onClick.RemoveAllListeners();

		}

	}

}