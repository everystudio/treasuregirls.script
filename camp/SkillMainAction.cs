﻿using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillMainAction
{
	[ActionCategory("SkillMainAction")]
	[HutongGames.PlayMaker.Tooltip("SkillMainAction")]
	public abstract class SkillMainActionBase : FsmStateAction
	{
		protected SkillMain skillMain;
		public override void OnEnter()
		{
			base.OnEnter();
			skillMain = Owner.GetComponent<SkillMain>();
		}
	}

	[ActionCategory("SkillMainAction")]
	[HutongGames.PlayMaker.Tooltip("SkillMainAction")]
	public class idle : SkillMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			skillMain.m_charaView.Initialize();

			skillMain.m_prefBannerSkill.SetActive(false);



			skillMain.m_goViewSkill_Setting.SetActive(true);
			skillMain.m_goViewSkill_List.SetActive(false);

			skillMain.skill_banner_list.Clear();
			MonoBehaviourEx.DeleteObjects<BannerSkill>(skillMain.m_goSettingSkillRoot);
			for( int i = 0; i < 3; i++)
			{
				DataSkillParam data = DataManager.Instance.dataSkill.list.Find(p => p.position == i+1);
				if( data == null)
				{
					data = new DataSkillParam(0, i + 1);
				}
				BannerSkill script = PrefabManager.Instance.MakeScript<BannerSkill>(skillMain.m_prefBannerSkill, skillMain.m_goSettingSkillRoot);
				skillMain.skill_banner_list.Add(script);
				MasterSkillParam master = DataManager.Instance.masterSkill.list.Find(p => p.skill_id == data.skill_id);
				script.Initialize(data,master);
			}



			skillMain.AllButtonClose();
			skillMain.m_btnEdit.gameObject.SetActive(true);
			skillMain.m_btnShop.gameObject.SetActive(true);
			skillMain.m_btnList.gameObject.SetActive(true);
			skillMain.m_btnEdit.onClick.AddListener(() =>
			{
				Fsm.Event("edit");
			});
			skillMain.m_btnShop.onClick.AddListener(() =>
			{
				Fsm.Event("shop");
			});
			skillMain.m_btnList.onClick.AddListener(() =>
			{
				Fsm.Event("list");
			});
		}

		public override void OnExit()
		{
			base.OnExit();
			skillMain.m_btnEdit.onClick.RemoveAllListeners();
			skillMain.m_btnShop.onClick.RemoveAllListeners();
		}
	}
	[ActionCategory("SkillMainAction")]
	[HutongGames.PlayMaker.Tooltip("SkillMainAction")]
	public class edit : SkillMainActionBase
	{
		public int icon_position;
		public int banner_skill_id;

		public override void OnEnter()
		{
			base.OnEnter();

			icon_position = 0;
			banner_skill_id = 0;

			foreach (IconSkill skill in skillMain.m_charaView.m_iconSkillList)
			{
				skill.OnClickIcon.AddListener(OnSkillIcon);
			}

			skillMain.m_goViewSkill_Setting.SetActive(false);
			skillMain.m_goViewSkill_List.SetActive(true);

			skillMain.skill_banner_list.Clear();
			MonoBehaviourEx.DeleteObjects<BannerSkill>(skillMain.m_goListContentsRoot);
			foreach (DataSkillParam data in DataManager.Instance.dataSkill.list)
			{
				BannerSkill script = PrefabManager.Instance.MakeScript<BannerSkill>(skillMain.m_prefBannerSkill, skillMain.m_goListContentsRoot);
				script.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 90);
				MasterSkillParam master = DataManager.Instance.masterSkill.list.Find(p => p.skill_id == data.skill_id);
				script.Initialize(data,master);
				script.OnClickBanner.AddListener(OnSkillBanner);
				skillMain.skill_banner_list.Add(script);
			}

			skillMain.AllButtonClose();
			skillMain.m_btnSet.gameObject.SetActive(true);
			skillMain.m_btnSet.interactable = false;

			skillMain.m_btnSet.onClick.AddListener(() =>
			{
				DataSkillParam target_data = DataManager.Instance.dataSkill.list.Find(p => p.skill_id == banner_skill_id);

				int temp_icon_position = skillMain.m_charaView.GetSkillPosition(target_data.skill_id);

				if(temp_icon_position != 0)
				{
					int temp_skill_id = skillMain.m_charaView.GetSkillId(temp_icon_position);
					DataSkillParam exchange_data = DataManager.Instance.dataSkill.list.Find(p => p.skill_id == banner_skill_id);

					if( exchange_data != null)
					{
						exchange_data.position = temp_icon_position;
					}
				}
				target_data.position = icon_position;

				skillMain.m_charaView.Initialize();

				banner_skill_id = 0;
				icon_position = 0;

				skillMain.SkillBannerSelect(0);
				skillMain.m_charaView.SkillSelect(0);
			});

			skillMain.m_btnBack.gameObject.SetActive(true);
			skillMain.m_btnBack.onClick.AddListener(() =>
			{
				Fsm.Event("back");
			});
		}

		private void OnSkillBanner(DataSkillParam arg0)
		{
			banner_skill_id = arg0.skill_id;
			skillMain.SkillBannerSelect(banner_skill_id);
			skillMain.m_btnSet.interactable = (banner_skill_id != 0 && icon_position != 0);
		}

		private void OnSkillIcon(DataSkillParam arg0)
		{
			icon_position = arg0.position;
			skillMain.m_charaView.SkillSelect(icon_position);
			skillMain.m_btnSet.interactable = (banner_skill_id != 0 && icon_position != 0);
		}

		public override void OnExit()
		{
			base.OnExit();
			foreach (IconSkill skill in skillMain.m_charaView.m_iconSkillList)
			{
				skill.OnClickIcon.RemoveAllListeners();
			}
		}

	}

	[ActionCategory("SkillMainAction")]
	[HutongGames.PlayMaker.Tooltip("SkillMainAction")]
	public class shop : SkillMainActionBase
	{
		public FsmInt select_skill_id;

		public override void OnEnter()
		{
			base.OnEnter();
			select_skill_id.Value= 0;
			skillMain.m_txtPrice.text = "0";

			skillMain.m_charaView.Initialize();

			skillMain.m_goViewSkill_Setting.SetActive(false);
			skillMain.m_goViewSkill_List.SetActive(true);

			skillMain.skill_banner_list.Clear();
			MonoBehaviourEx.DeleteObjects<BannerSkill>(skillMain.m_goListContentsRoot);
			foreach (MasterSkillParam master in DataManager.Instance.masterSkill.list.FindAll(p=>0<p.skill_id))
			{
				DataSkillParam check_data = DataManager.Instance.dataSkill.list.Find(p => p.skill_id == master.skill_id );
				if( check_data == null)
				{
					BannerSkill script = PrefabManager.Instance.MakeScript<BannerSkill>(skillMain.m_prefBannerSkill, skillMain.m_goListContentsRoot);
					script.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 90);

					DataSkillParam data = new DataSkillParam(master.skill_id, 0);
					script.Initialize(data, master);
					script.OnClickBanner.AddListener(OnSkillBanner);
					skillMain.skill_banner_list.Add(script);
				}
			}

			skillMain.AllButtonClose();
			skillMain.m_btnBuy.gameObject.SetActive(true);
			skillMain.m_btnBack.gameObject.SetActive(true);
			skillMain.m_btnBuy.interactable = false;
			skillMain.m_btnBuy.onClick.AddListener(() =>
			{
				Fsm.Event("buy");
			});
			skillMain.m_btnBack.onClick.AddListener(() =>
			{
				Fsm.Event("back");
			});
		}

		private void OnSkillBanner(DataSkillParam arg0)
		{
			select_skill_id.Value = arg0.skill_id;
			MasterSkillParam master = DataManager.Instance.masterSkill.list.Find(p => p.skill_id == arg0.skill_id);

			bool bCan = false;

			if( 0 <= master.gold)
			{
				bCan = true;
			}
			skillMain.m_btnBuy.interactable = bCan;

			skillMain.m_txtPrice.text = master.gold.ToString();
			skillMain.SkillBannerSelect(select_skill_id.Value);
		}

		public override void OnExit()
		{
			base.OnExit();
			skillMain.m_btnEdit.onClick.RemoveAllListeners();
			skillMain.m_btnBack.onClick.RemoveAllListeners();
		}
	}

	[ActionCategory("SkillMainAction")]
	[HutongGames.PlayMaker.Tooltip("SkillMainAction")]
	public class buy : SkillMainActionBase
	{
		public FsmInt select_skill_id;
		public override void OnEnter()
		{
			base.OnEnter();
			MasterSkillParam buy_skill = DataManager.Instance.masterSkill.list.Find(p => p.skill_id == select_skill_id.Value);

			if( DataManager.Instance.UseGold(buy_skill.gold))
			{
				DataSkillParam add = new DataSkillParam(buy_skill.skill_id, 0);
				DataManager.Instance.dataSkill.list.Add(add);
			}
			Finish();
		}
	}



	[ActionCategory("SkillMainAction")]
	[HutongGames.PlayMaker.Tooltip("SkillMainAction")]
	public class list : SkillMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			skillMain.m_charaView.Initialize();

			skillMain.m_goViewSkill_Setting.SetActive(false);
			skillMain.m_goViewSkill_List.SetActive(true);

			MonoBehaviourEx.DeleteObjects<BannerSkill>(skillMain.m_goListContentsRoot);
			foreach(DataSkillParam data in DataManager.Instance.dataSkill.list)
			{
				BannerSkill script = PrefabManager.Instance.MakeScript<BannerSkill>(skillMain.m_prefBannerSkill, skillMain.m_goListContentsRoot);
				script.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 90);
				MasterSkillParam master = DataManager.Instance.masterSkill.list.Find(p => p.skill_id == data.skill_id);
				script.Initialize(data,master);
			}




			skillMain.AllButtonClose();
			skillMain.m_btnEdit.gameObject.SetActive(true);
			skillMain.m_btnShop.gameObject.SetActive(true);
			skillMain.m_btnBack.gameObject.SetActive(true);
			skillMain.m_btnEdit.onClick.AddListener(() =>
			{
				Fsm.Event("edit");
			});
			skillMain.m_btnShop.onClick.AddListener(() =>
			{
				Fsm.Event("shop");
			});
			skillMain.m_btnBack.onClick.AddListener(() =>
			{
				Fsm.Event("back");
			});
		}

		public override void OnExit()
		{
			base.OnExit();
			skillMain.m_btnEdit.onClick.RemoveAllListeners();
			skillMain.m_btnShop.onClick.RemoveAllListeners();
			skillMain.m_btnBack.onClick.RemoveAllListeners();
		}
	}
}