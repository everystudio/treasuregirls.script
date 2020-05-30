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
		public FsmInt continue_count;
		public override void OnEnter()
		{
			base.OnEnter();
			gamemain.IsGoal = false;
			gamemain.player_chara.gameObject.SetActive(false);

			if (0 < continue_count.Value)
			{
				gamemain.m_goContinueCountRoot.SetActive(true);
				gamemain.m_txtContinueCount.text = continue_count.Value.ToString();
			}
			else
			{
				gamemain.m_goContinueCountRoot.SetActive(false);
			}


			gamemain.m_fGameTime = 0.0f;
			gamemain.m_txtFloor.text = string.Format("{0}", "-----");

#if UNITY_EDITOR
			if (DataManager.Instance.IsTest)
			{
				Time.timeScale = DataManager.Instance.TestTimeScale;
				if ( DataManager.Instance.test_result == 0)
				{
					DataManager.Instance.test_floor_id += 1;
				}
				else if (DataManager.Instance.test_result == 1 || 0 < DataManager.Instance.armor_interval )
				{
					DataManager.Instance.test_weapon_level += 1;
					if(10 < DataManager.Instance.test_weapon_level)
					{
						int[] sword_weapon_id_arr = new int[5]
						{
							7 , 28 , 49,55,141
						};
						DataManager.Instance.test_weapon_index += 1;
						DataManager.Instance.test_weapon_id = sword_weapon_id_arr[DataManager.Instance.test_weapon_index];
						DataManager.Instance.test_weapon_level = 1;
					}
					DataManager.Instance.armor_interval -= 1;
				}
				else if(DataManager.Instance.test_result == 2)
				{
					DataManager.Instance.test_armor_level += 1;
					DataManager.Instance.armor_interval = 5;
				}
				else
				{
					Debug.LogError("hairanaihazu");
				}
				DataManager.Instance.game_data.WriteInt("floor_id", DataManager.Instance.test_floor_id);

				DataWeaponParam weapon = DataManager.Instance.dataWeapon.list.Find(p => 0 < p.equip);
				weapon.weapon_id = DataManager.Instance.test_weapon_id;
				weapon.level = DataManager.Instance.test_weapon_level;

				foreach (DataArmorParam armor in DataManager.Instance.dataArmor.list)
				{
					MasterArmorParam armor_master = DataManager.Instance.masterArmor.list.Find(p => p.armor_id == armor.armor_id);
					MasterArmorParam test_armor_master = DataManager.Instance.masterArmor.list.Find(p => 
					p.position == armor_master.position &&
					p.level == DataManager.Instance.test_armor_level);

					armor.armor_id = test_armor_master.armor_id;
					armor.level = test_armor_master.level;
				}

			}
#endif
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

			float fAutoPotionRate = DataManager.Instance.user_data.ReadFloat(Defines.KEY_AUTOPOTION_RATE);

			gamemain.m_btnAuto.Initialize(DataManager.Instance.user_data.ReadInt(Defines.KEY_USE_AUTOMOVE) != 0);
			gamemain.m_btnAutoPotion.Initialize( 
				DataManager.Instance.user_data.ReadInt(Defines.KEY_USE_AUTOPOTION) != 0,
				fAutoPotionRate);

			gamemain.player_chara.gameObject.SetActive(true);

			gamemain.m_panelPauseMenu.m_soundvolumeBGM.SetVolume(DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_BGM));
			BGMControl.Instance.Play("anime_06_loop");

			gamemain.m_panelPauseMenu.m_soundvolumeSE.SetVolume(DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_SE));
			//Debug.Log( string.Format("floor_id={0}", DataManager.Instance.game_data.ReadInt("floor_id")));

			int floor_id = DataManager.Instance.game_data.ReadInt("floor_id");

			gamemain.m_txtFloor.text = string.Format("{0}", floor_id);

			gamemain.master_floor_param = DataManager.Instance.masterFloor.list.Find(p => p.floor_id == floor_id);
			gamemain.master_stage_param = DataManager.Instance.masterStage.list.Find(p => p.stage_id == gamemain.master_floor_param.stage_id);

			gamemain.background.spr_renderer.sprite = gamemain.m_spriteAtlasBackground.GetSprite(gamemain.master_stage_param.bg_name);

			//gamemain.player_chara.Damage(10);


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
			//gamemain.m_prefEnemyHpBar.gameObject.SetActive(false);

			foreach( GameObject zako_pos in gamemain.zako_position)
			{
				MonoBehaviourEx.DeleteObjects<Transform>(zako_pos);
				create_enemy(
					gamemain.master_floor_param.enemy_id,
					gamemain.master_floor_param.enemy_level,
					zako_pos,false);
			}
			MonoBehaviourEx.DeleteObjects<Transform>(gamemain.boss_position);
			create_enemy(
				gamemain.master_floor_param.boss_enemy_id,
				gamemain.master_floor_param.boss_level,
				gamemain.boss_position,true);


			#endregion
		}

		private void create_enemy( int _iEnemyId , int _iEnemyLevel , GameObject _goRoot , bool _bIsBoss )
		{
			Enemy script = PrefabManager.Instance.MakeScript<Enemy>(gamemain.m_prefEnemy.gameObject, _goRoot);
			script.transform.localPosition = Vector3.zero;
			/*
			EnergyBarToolkit.EnergyBarFollowObject boss_hp_bar = PrefabManager.Instance.MakeScript<EnergyBarToolkit.EnergyBarFollowObject>(
				gamemain.m_prefEnemyHpBar.gameObject, gamemain.panel_energy_bar);
			boss_hp_bar.followObject = script.m_enemyBody.gameObject;
			script.hp_bar = boss_hp_bar.gameObject.GetComponent<EnergyBar>();
			*/
			script.SetEnemyData(_iEnemyId , _iEnemyLevel , _bIsBoss);
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
		public int battle_time;
		private bool bIsPausing;
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
			bIsPausing = false;
			gamemain.m_btnPause.onClick.AddListener(() =>
			{
				//Debug.Log("pause");
				Fsm.Event("pause");
				/*
				bIsPausing = !bIsPausing;
				gamemain.m_goPauseCover.SetActive(bIsPausing);
				if (bIsPausing)
				{
					Time.timeScale = 0.0f;
				}
				else
				{
					Time.timeScale = 1.0f;
				}
				*/
			});
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
			gamemain.m_fGameTime += Time.deltaTime;
			int last_time = battle_time - (int)gamemain.m_fGameTime;
			gamemain.m_txtLastTime.text = string.Format("{0:D2}:{1:D2}", last_time / 60, last_time % 60);

			if (gamemain.IsGoal)
			{
				DataManager.Instance.test_result = 0;
				Fsm.Event("goal");
			}
			else if(battle_time < gamemain.m_fGameTime)
			{
				DataManager.Instance.test_result = 1;
				Fsm.Event("timeover");
			}
			else if(gamemain.player_chara.m_dataUnitParam.hp <= 0)
			{
				DataManager.Instance.test_result = 2;
				Fsm.Event("gameover");
			}
		}

		public override void OnExit()
		{
			base.OnExit();
			gamemain.icon_potion.m_btn.onClick.RemoveAllListeners();
			gamemain.m_btnPause.onClick.RemoveAllListeners();
			foreach (IconSkill icon in gamemain.icon_skill_arr)
			{
				icon.OnClickIcon.RemoveAllListeners();
			}
		}
	}

	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class pause : GameMainActionBase
	{
		private float save_game_speed;
		public override void OnEnter()
		{
			base.OnEnter();
			//Debug.Log("pause enter");

			save_game_speed = Time.timeScale;
			Time.timeScale = 0.0f;

			gamemain.m_panelPauseMenu.gameObject.SetActive(true);

			gamemain.m_panelPauseMenu.m_btnClose.onClick.AddListener(() =>
			{

				Finish();
			});
			gamemain.m_panelPauseMenu.m_btnReturnCamp.onClick.AddListener(() =>
			{
				Fsm.Event("camp");
			});

		}
		public override void OnExit()
		{
			//Debug.Log("pause exit");
			base.OnExit();
			Time.timeScale = save_game_speed;
			gamemain.m_panelPauseMenu.gameObject.SetActive(false);

			gamemain.m_panelPauseMenu.m_btnClose.onClick.RemoveAllListeners();
			gamemain.m_panelPauseMenu.m_btnReturnCamp.onClick.RemoveAllListeners();

			DataManager.Instance.user_data.WriteFloat(Defines.KEY_AUTOPOTION_RATE, gamemain.m_panelPauseMenu.m_autoPotionRate.rate);

			DataManager.Instance.user_data.WriteFloat(Defines.KEY_SOUNDVOLUME_BGM, gamemain.m_panelPauseMenu.m_soundvolumeBGM.rate);
			DataManager.Instance.user_data.WriteFloat(Defines.KEY_SOUNDVOLUME_SE, gamemain.m_panelPauseMenu.m_soundvolumeSE.rate);

			float fAutoPotionRate = DataManager.Instance.user_data.ReadFloat(Defines.KEY_AUTOPOTION_RATE);
			gamemain.m_btnAutoPotion.recover_rate = gamemain.m_panelPauseMenu.m_autoPotionRate.rate;
			//Debug.Log(gamemain.m_btnAutoPotion.recover_rate);
		}
	}

	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class result : GameMainActionBase
	{
		public FsmInt continue_count;
		public override void OnEnter()
		{
			base.OnEnter();

			bool bFirstClear = false;

			int floor_id = DataManager.Instance.game_data.ReadInt("floor_id");
			MasterFloorParam current_floor = DataManager.Instance.masterFloor.list.Find(p => p.floor_id == floor_id);
			MasterStageParam current_stage = DataManager.Instance.masterStage.list.Find(p => p.stage_id == current_floor.stage_id);

			DataFloorParam data_floor = DataManager.Instance.dataFloor.list.Find(p => p.floor_id == current_floor.floor_id);
			if(data_floor == null)
			{
				data_floor = new DataFloorParam();
				DataManager.Instance.dataFloor.list.Add(data_floor);
				data_floor.floor_id = floor_id;
			}
			data_floor.count += 1;
			// 初クリア判定は2(クリア状態じゃなければ)
			bFirstClear = data_floor.status != 2;
			data_floor.status = 2;
			if( DataManager.Instance.IsTest)
			{
				// マジックナンバーか！
				DataManager.Instance.armor_interval = 5;
				DataWeaponParam weapon = DataManager.Instance.dataWeapon.list.Find(p => 0 < p.equip);
				data_floor.test_weapon_id = weapon.weapon_id;
				data_floor.test_weapon_level = weapon.level;
				DataArmorParam armor = DataManager.Instance.dataArmor.list[0];
				data_floor.test_armor_level = armor.level;
			}

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
			DataManager.Instance.dataPotion.Save();

			// 獲得Coinの補正
			DataItemParam data_item_coin = DataManager.Instance.dataGetItem.list.Find(p => p.item_id == Defines.ITEM_ID_COIN);
			int get_coin_num = 0;
			if( data_item_coin != null)
			{
				get_coin_num = data_item_coin.num;
			}
			get_coin_num = Mathf.CeilToInt((float)get_coin_num * (float)((float)gamemain.player_chara.m_dataUnitParam.coin / 100.0f));

			gamemain.m_goFadePanel.SetActive(true);
			gamemain.m_panelResult.Initialize(floor_id , (int)gamemain.m_fGameTime);
			gamemain.m_panelResult.gameObject.SetActive(true);

			foreach( DataItemParam get in DataManager.Instance.dataGetItem.list)
			{
				if( get.item_id == 1)
				{
					DataManager.Instance.AddCoin(get.num);
				}
				else if (get.item_id == 2)
				{
					DataManager.Instance.AddGem(get.num);
				}
				else if (get.item_id == 3)
				{
					DataManager.Instance.AddKey(get.num);
				}
				else if (get.item_id == 4)
				{
					DataManager.Instance.AddGoldKey(get.num);
				}
				else
				{
					DataManager.Instance.dataItem.Add(get.item_id, get.num);
				}
			}
			DataManager.Instance.dataItem.Save();
			DataManager.Instance.dataGetItem.list.Clear();

			DataManager.Instance.user_data.Save();

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
			gamemain.m_panelResult.m_btnRetry10.onClick.RemoveAllListeners();
			gamemain.m_panelResult.m_btnRetry10.onClick.AddListener(() =>
			{
				Fsm.Event("retry10");
			});

			gamemain.m_panelResult.m_btnNext.interactable = 0 < current_floor.next_floor_id;
			gamemain.m_panelResult.m_btnNext.onClick.RemoveAllListeners();
			gamemain.m_panelResult.m_btnNext.onClick.AddListener(() =>
			{
				Fsm.Event("next");
			});

			gamemain.m_panelResult.m_btnContinueEnd.onClick.RemoveAllListeners();
			gamemain.m_panelResult.m_btnContinueEnd.onClick.AddListener(() =>
			{
				button_active(true);
				continue_count.Value = 0;
			});
			gamemain.m_panelResult.m_btnQuickNext.onClick.RemoveAllListeners();
			gamemain.m_panelResult.m_btnQuickNext.onClick.AddListener(() =>
			{
				Fsm.Event("retry");
			});

			if( 0 < continue_count.Value)
			{
				button_active(false);
				StartCoroutine(auto_retry());
			}
			else
			{
				button_active(true);
			}

			if (DataManager.Instance.IsTest)
			{
				Fsm.Event("retry");
			}
			if( current_floor.next_floor_id == 0 && bFirstClear)
			{
				Fsm.Event("ending");
			}

		}

		private void button_active(bool _bFlag)
		{
			gamemain.m_panelResult.m_btnCamp.gameObject.SetActive(_bFlag);
			gamemain.m_panelResult.m_btnRetry.gameObject.SetActive(_bFlag);
			gamemain.m_panelResult.m_btnRetry10.gameObject.SetActive(_bFlag);
			gamemain.m_panelResult.m_btnNext.gameObject.SetActive(_bFlag);
			gamemain.m_panelResult.m_btnContinueEnd.gameObject.SetActive(!_bFlag);
			gamemain.m_panelResult.m_btnQuickNext.gameObject.SetActive(!_bFlag);
		}

		private IEnumerator auto_retry()
		{
			yield return new WaitForSecondsRealtime(2.0f);
			if( 0 < continue_count.Value)
			{
				Fsm.Event("retry");
			}
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
	public class player_dead : GameMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			gamemain.player_chara.OnDeadEnd.AddListener(OnDeadEnd);
			gamemain.player_chara.m_animator.SetTrigger("dead");
		}

		private void OnDeadEnd()
		{
			Finish();
		}
	}

	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class show_gameover : GameMainActionBase
	{
		public float wait_time;
		private float time;
		public override void OnEnter()
		{
			base.OnEnter();
			time = 0.0f;
			gamemain.m_goGameOver.SetActive(true);
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			time += Time.deltaTime;
			if( wait_time < time)
			{
				Finish();
			}
		}
	}

	[ActionCategory("GameMainAction")]
	[HutongGames.PlayMaker.Tooltip("GameMainAction")]
	public class to_camp : GameMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			if (DataManager.Instance.IsTest)
			{
				Fsm.Event("test");
			}
			else
			{
				Finish();
			}
		}
	}


}
