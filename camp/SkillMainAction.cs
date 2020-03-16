using HutongGames.PlayMaker;
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

			skillMain.AllButtonClose();
			skillMain.m_btnEdit.gameObject.SetActive(true);
			skillMain.m_btnShop.gameObject.SetActive(true);
			skillMain.m_btnEdit.onClick.AddListener(() =>
			{
				Fsm.Event("edit");
			});
			skillMain.m_btnShop.onClick.AddListener(() =>
			{
				Fsm.Event("shop");
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
			foreach (IconSkill skill in skillMain.m_charaView.m_iconSkillList)
			{
				skill.OnClickIcon.AddListener(OnSkillIcon);
			}

			skillMain.AllButtonClose();

			skillMain.m_btnSet.gameObject.SetActive(true);
			skillMain.m_btnBack.gameObject.SetActive(true);
			skillMain.m_btnShop.gameObject.SetActive(true);
			skillMain.m_btnBack.onClick.AddListener(() =>
			{
				Fsm.Event("back");
			});
			skillMain.m_btnBack.onClick.AddListener(() =>
			{
				Fsm.Event("back");
			});
			skillMain.m_btnShop.onClick.AddListener(() =>
			{
				Fsm.Event("shop");
			});
		}
		private void OnSkillIcon(DataSkillParam arg0)
		{
			icon_position = arg0.position;
			skillMain.m_charaView.SkillSelect(icon_position);
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
		public override void OnEnter()
		{
			base.OnEnter();
			skillMain.m_charaView.Initialize();

			skillMain.AllButtonClose();
			skillMain.m_btnEdit.gameObject.SetActive(true);
			skillMain.m_btnBack.gameObject.SetActive(true);
			skillMain.m_btnEdit.onClick.AddListener(() =>
			{
				Fsm.Event("edit");
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
			skillMain.m_btnBack.onClick.RemoveAllListeners();
		}
	}

}
