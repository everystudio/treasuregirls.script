using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMainAction
{
	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public abstract class GameMainActionBase : FsmStateAction
	{
		protected GameMain gamemain;
		public override void OnEnter()
		{
			base.OnEnter();
			gamemain = Owner.GetComponent<GameMain>();
		}
	}

	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class gamestart : GameMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			gamemain.m_btnAuto.Initialize(true);

			Debug.Log( string.Format("floor_id={0}", DataManager.Instance.game_data.ReadInt("floor_id")));

			gamemain.player_chara.Damage(10);


			DataPotionParam data_potion = DataManager.Instance.dataPotion.list.Find(p => p.is_use == true);
			MasterPotionParam master_potion = DataManager.Instance.masterPotion.list.Find(p => p.potion_id == data_potion.potion_id);
			gamemain.icon_potion.InitializeGame(data_potion, master_potion);

			gamemain.icon_skill_arr = gamemain.m_goIconRoot.GetComponentsInChildren<IconSkill>();
			for ( int i = 0; i < gamemain.icon_skill_arr.Length; i++)
			{
				int position = i+1;
				DataSkillParam data = DataManager.Instance.dataSkill.list.Find(p => p.position == position);
				if (data == null)
				{
					data = new DataSkillParam(0, position);
				}
				MasterSkillParam master = DataManager.Instance.masterSkill.list.Find(p => p.skill_id == data.skill_id);
				gamemain.icon_skill_arr[i].InitializeGame(data, master);
			}
			Finish();
		}
	}


	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class playing : GameMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			gamemain.icon_potion.m_btn.onClick.AddListener(use_potion);
			foreach (IconSkill icon in gamemain.icon_skill_arr)
			{
				icon.OnClickIcon.RemoveAllListeners();
				icon.OnClickIcon.AddListener((_icon) =>
				{
					MasterSkillParam skill = DataManager.Instance.masterSkill.list.Find(p => p.skill_id == _icon.m_data.skill_id);
					gamemain.player_chara.Skill(_icon.m_data, skill);
					_icon.UseSkill();
				});
			}
		}

		private void use_potion()
		{
			DataPotionParam data_potion = DataManager.Instance.dataPotion.list.Find(p => p.is_use == true);
			MasterPotionParam master_potion = DataManager.Instance.masterPotion.list.Find(p => p.potion_id == data_potion.potion_id);

			if (0 < data_potion.num)
			{
				data_potion.num -= 1;
				gamemain.player_chara.Heal(master_potion.heal);

				gamemain.icon_potion.UseUpdate();
			}
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			if (gamemain.IsGoal)
			{
				Fsm.Event("goal");
			}
		}

		public override void OnExit()
		{
			base.OnExit();
			gamemain.icon_potion.m_btn.onClick.RemoveAllListeners();
			foreach (IconSkill icon in gamemain.icon_skill_arr)
			{
				icon.OnClickIcon.RemoveAllListeners();
			}
		}
	}

	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class result : GameMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			gamemain.m_goFadePanel.SetActive(true);
			gamemain.m_panelResult.Initialize();
			gamemain.m_panelResult.gameObject.SetActive(true);
		}
	}


}
