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
	public class ready : GameMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			gamemain.IsGoal = false;
			gamemain.player_chara.gameObject.SetActive(false);

			Finish();
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
			gamemain.m_btnAutoPotion.Initialize(true , 0.5f);

			gamemain.player_chara.gameObject.SetActive(true);


			Debug.Log( string.Format("floor_id={0}", DataManager.Instance.game_data.ReadInt("floor_id")));

			int floor_id = DataManager.Instance.game_data.ReadInt("floor_id");

			MasterFloorParam current_floor = DataManager.Instance.masterFloor.list.Find(p => p.floor_id == floor_id);
			MasterStageParam current_stage = DataManager.Instance.masterStage.list.Find(p => p.stage_id == current_floor.stage_id);

			gamemain.background.spr_renderer.sprite = gamemain.m_spriteAtlasBackground.GetSprite(current_stage.bg_name);

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

			#region 敵
			gamemain.m_prefEnemy.gameObject.SetActive(false);
			gamemain.m_prefEnemyHpBar.gameObject.SetActive(false);

			foreach( GameObject zako_pos in gamemain.zako_position)
			{
				MonoBehaviourEx.DeleteObjects<Transform>(zako_pos);
				create_enemy(current_floor.enemy_id, current_floor.enemy_level, zako_pos);
			}
			MonoBehaviourEx.DeleteObjects<Transform>(gamemain.boss_position);
			create_enemy(current_floor.boss_enemy_id, current_floor.boss_level, gamemain.boss_position);


			#endregion
		}

		private void create_enemy( int _iEnemyId , int _iEnemyLevel , GameObject _goRoot)
		{
			Enemy script = PrefabManager.Instance.MakeScript<Enemy>(gamemain.m_prefEnemy.gameObject, _goRoot);
			script.transform.localPosition = Vector3.zero;
			EnergyBarToolkit.EnergyBarFollowObject boss_hp_bar = PrefabManager.Instance.MakeScript<EnergyBarToolkit.EnergyBarFollowObject>(
				gamemain.m_prefEnemyHpBar.gameObject, gamemain.panel_energy_bar);
			boss_hp_bar.followObject = script.m_enemyBody.gameObject;
			script.hp_bar = boss_hp_bar.gameObject.GetComponent<EnergyBar>();

			script.SetEnemyData(_iEnemyId , _iEnemyLevel);
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			if(gamemain.player_chara.is_goal == false)
			{
				Finish();
			}
			else
			{
				Debug.Log("aaaaaaaa");
			}
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

			int floor_id = DataManager.Instance.game_data.ReadInt("floor_id");
			MasterFloorParam current_floor = DataManager.Instance.masterFloor.list.Find(p => p.floor_id == floor_id);
			MasterStageParam current_stage = DataManager.Instance.masterStage.list.Find(p => p.stage_id == current_floor.stage_id);

			DataFloorParam data_floor = DataManager.Instance.dataFloor.list.Find(p => p.floor_id == current_floor.floor_id);
			data_floor.count += 1;
			data_floor.status = 2;

			// 次のフロアが必要な場合
			if ( 0 < current_floor.next_floor_id)
			{
				DataFloorParam data_next_floor = DataManager.Instance.dataFloor.list.Find(p => p.floor_id == current_floor.next_floor_id);
				if(data_next_floor == null)
				{
					MasterFloorParam master_next_floor = DataManager.Instance.masterFloor.list.Find(p => p.floor_id == current_floor.next_floor_id);

					data_next_floor = new DataFloorParam();
					data_next_floor.floor_id = master_next_floor.floor_id;
					data_next_floor.stage_id = master_next_floor.stage_id;
					data_next_floor.status = 1;
					data_next_floor.count = 0;
					DataManager.Instance.dataFloor.list.Add(data_next_floor);

					// ステージが異なる場合はここで追加
					if( current_floor.stage_id != master_next_floor.stage_id)
					{

						DataStageParam data_new_stage = DataManager.Instance.dataStage.list.Find(p => p.stage_id == master_next_floor.stage_id);
						if (data_new_stage == null)
						{
							data_new_stage = new DataStageParam();
							data_new_stage.stage_id = master_next_floor.stage_id;
							data_new_stage.status = 1;
							DataManager.Instance.dataStage.list.Add(data_new_stage);
						}
					}
				}
			}

			// 現在のフロア攻略チェック
			DataFloorParam check_stage_complete = DataManager.Instance.dataFloor.list.Find(p => p.stage_id == current_floor.stage_id && p.status != 2);
			if (check_stage_complete == null)
			{
				DataStageParam data_current_stage = DataManager.Instance.dataStage.list.Find(p => p.stage_id == current_floor.stage_id);
				data_current_stage.status = 2;
			}


			DataManager.Instance.dataStage.Save();
			DataManager.Instance.dataFloor.Save();


			gamemain.m_goFadePanel.SetActive(true);
			gamemain.m_panelResult.Initialize(floor_id);
			gamemain.m_panelResult.gameObject.SetActive(true);



			gamemain.m_panelResult.m_btnCamp.onClick.RemoveAllListeners();
			gamemain.m_panelResult.m_btnCamp.onClick.AddListener(() =>
			{
				Fsm.Event("camp");
			});
			gamemain.m_panelResult.m_btnRetry.onClick.RemoveAllListeners();
			gamemain.m_panelResult.m_btnRetry.onClick.AddListener(() =>
			{
				Fsm.Event("retry");
			});

			gamemain.m_panelResult.m_btnNext.interactable = 0 < current_floor.next_floor_id;
			gamemain.m_panelResult.m_btnNext.onClick.RemoveAllListeners();
			gamemain.m_panelResult.m_btnNext.onClick.AddListener(() =>
			{
				Fsm.Event("next");
			});

		}

		public override void OnExit()
		{
			base.OnExit();
			gamemain.m_goFadePanel.SetActive(false);
			gamemain.m_panelResult.gameObject.SetActive(false);

		}
	}
	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class next_floor : GameMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			int floor_id = DataManager.Instance.game_data.ReadInt("floor_id");
			MasterFloorParam current_floor = DataManager.Instance.masterFloor.list.Find(p => p.floor_id == floor_id);

			if( 0 < current_floor.next_floor_id)
			{
				DataManager.Instance.game_data.WriteInt("floor_id", current_floor.next_floor_id);
				DataManager.Instance.game_data.Save();
			}

			Finish();
		}
	}


	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class to_camp : GameMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			Finish();
		}
	}


}
