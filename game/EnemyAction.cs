﻿using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAction
{
	[ActionCategory("EnemyAction")]
	[HutongGames.PlayMaker.Tooltip("EnemyAction")]
	public abstract class EnemyActionlBase : FsmStateAction
	{
		protected Enemy enemy;
		public override void OnEnter()
		{
			base.OnEnter();
			enemy = Owner.GetComponent<Enemy>();
		}
	}

	[ActionCategory("EnemyAction")]
	[HutongGames.PlayMaker.Tooltip("EnemyAction")]
	public class Setup : EnemyActionlBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

		}

		public override void OnUpdate()
		{
			base.OnUpdate();

			if (enemy.is_setenemy)
			{
				MasterEnemyParam master_enemy = DataManager.Instance.masterEnemy.list.Find(p => p.enemy_id == enemy.enemy_id);
				enemy.dataUnitParam.BuildEnemy(master_enemy, enemy.enemy_level);
				if (enemy.is_boss)
				{
					//enemy.gameObject.transform.localScale = Vector3.one * 2.0f;
				}
				enemy.m_sprEnemy.sprite = enemy.m_sprAtlas.GetSprite(master_enemy.sprite_name);
				if (enemy.m_sprEnemy.gameObject.GetComponent<BoxCollider2D>() != null)
				{
					GameObject.Destroy(enemy.m_sprEnemy.gameObject.GetComponent<BoxCollider2D>());
				}
				enemy.m_bcEnemy = enemy.m_sprEnemy.gameObject.AddComponent<BoxCollider2D>();
				enemy.attack_timer = enemy.attack_interval;

				Finish();
			}
		}
	}

	[ActionCategory("EnemyAction")]
	[HutongGames.PlayMaker.Tooltip("EnemyAction")]
	public class Search : EnemyActionlBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			enemy.enemy_search.IsFindPlayer = false;
			enemy.enemy_search.gameObject.SetActive(true);

			//enemy.attack_timer = enemy.attack_interval;
			//Rigidbody2D rb2 = enemy.m_enemyBody.gameObject.GetComponent<Rigidbody2D>();
			//rb2.AddForce(new Vector2(1.0f, 0.0f) * 1000);

		}
		public override void OnUpdate()
		{
			base.OnUpdate();
			//enemy.hp_bar.SetValueMax(enemy.dataUnitParam.hp_max);
			//enemy.hp_bar.SetValueCurrent(enemy.dataUnitParam.hp);


			if (enemy.enemy_search.IsFindPlayer)
			{
				enemy.enemy_search.gameObject.SetActive(false);
				Finish();
			}
		}
	}

	[ActionCategory("EnemyAction")]
	[HutongGames.PlayMaker.Tooltip("EnemyAction")]
	public class Move : EnemyActionlBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			//Debug.Log("move");
		}
		public override void OnUpdate()
		{
			base.OnUpdate();

			if (enemy.m_enemyBody.IsAir)
			{
				Fsm.Event("air");
				return;
			}


			//enemy.hp_bar.SetValueMax(enemy.dataUnitParam.hp_max);
			//enemy.hp_bar.SetValueCurrent(enemy.dataUnitParam.hp);

			Vector3 dir = GameCharaMain.Instance.m_sprChara.gameObject.transform.position -
				enemy.m_sprEnemy.gameObject.transform.position;

			float distance = Vector3.SqrMagnitude(dir);

			float move_speed = enemy.move;
			if (dir.x < 0)
			{
				move_speed *= -1;
			}
			//enemy.m_rbEnemy.velocity = new Vector2(move_speed, enemy.m_rbEnemy.velocity.y);
			enemy.m_rbEnemy.transform.localPosition += new Vector3(move_speed * Time.deltaTime, 0.0f, 0.0f);


			if (enemy.dataUnitParam.hp <= 0)
			{
				Fsm.Event("dead");
			}
			else if (enemy.m_enemyBody.IsHitPlayer)
			{
				Finish();
			}
		}
	}



	[ActionCategory("EnemyAction")]
	[HutongGames.PlayMaker.Tooltip("EnemyAction")]
	public class Battle : EnemyActionlBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			//Debug.Log("battle");
		}
		public override void OnUpdate()
		{
			//enemy.hp_bar.SetValueMax(enemy.dataUnitParam.hp_max);
			//enemy.hp_bar.SetValueCurrent(enemy.dataUnitParam.hp);

			base.OnUpdate();
			enemy.attack_timer -= Time.deltaTime * ((float)enemy.dataUnitParam.speed * 0.2f);

			if(enemy.dataUnitParam.hp <= 0)
			{
				Fsm.Event("dead");
			}
			if (enemy.m_enemyBody.IsAir)
			{
				Fsm.Event("air");
			}
			else if (enemy.m_enemyBody.IsHitPlayer == false)
			{
				Fsm.Event("not_found");
			}
			else if (0.0f < enemy.attack_timer )
			{
				// まだ
			}
			else
			{
				Fsm.Event("attack");
			}
		}
	}

	[ActionCategory("EnemyAction")]
	[HutongGames.PlayMaker.Tooltip("EnemyAction")]
	public class Attack : EnemyActionlBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			// 絶対消す
			enemy.OnAttackEnd.AddListener(() =>
			{
				enemy.attack_timer += enemy.attack_interval;

				Finish();
			});
			enemy.m_animatorBody.SetTrigger("attack");
		}

		public override void OnUpdate()
		{
			base.OnUpdate();

			if (enemy.dataUnitParam.hp <= 0)
			{
				Fsm.Event("dead");
			}

		}
	}

	[ActionCategory("EnemyAction")]
	[HutongGames.PlayMaker.Tooltip("EnemyAction")]
	public class Dead : EnemyActionlBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			// 一応ね
			//enemy.hp_bar.SetValueMax(enemy.dataUnitParam.hp_max);
			//enemy.hp_bar.SetValueCurrent(enemy.dataUnitParam.hp);
			enemy.m_animatorBody.enabled = true;

			DropObject drop = PrefabManager.Instance.MakeScript<DropObject>(enemy.drop_object.gameObject, enemy.gameObject.transform.parent.gameObject);
			drop.transform.position = enemy.m_enemyBody.gameObject.transform.position;

			MasterItemParam master_item = DataManager.Instance.masterItem.list.Find(p => p.item_id == 1);
			drop.Initialize(master_item, GameMain.Instance.master_floor_param.GetCoinNum());

			int[] drop_item_prob_arr = new int[5]
			{
				1000,	// 落とさない
				150,	// ノーマルドロップ		
				30,		// レアドロップ
				150,	// カギ
				5,		// ゴールドキー
			};
			drop_item_prob_arr[0] -= GameMain.Instance.player_chara.m_dataUnitParam.luck;
			if( drop_item_prob_arr[0] < 0)
			{
				drop_item_prob_arr[0] = 0;
			}

			int drop_index = UtilRand.GetIndex(drop_item_prob_arr);
			int drop_item_id = 0;
			if( drop_index == 1)
			{
				drop_item_id = GameMain.Instance.master_floor_param.drop_item_id;
			}
			else if( drop_index == 2)
			{
				drop_item_id = GameMain.Instance.master_floor_param.rare_item_id;
			}
			else if( drop_index == 3)
			{
				// たまたまです
				drop_item_id = 3;
			}
			else if( drop_index == 4)
			{
				// たまたまです
				drop_item_id = 4;
			}

			if ( 0 < drop_item_id)
			{
				DropObject drop_item = PrefabManager.Instance.MakeScript<DropObject>(enemy.drop_object.gameObject, enemy.gameObject.transform.parent.gameObject);
				drop.transform.position = enemy.m_enemyBody.gameObject.transform.position;
				MasterItemParam master_item_drop = DataManager.Instance.masterItem.list.Find(p => p.item_id == drop_item_id);
				drop.Initialize(master_item_drop, 1);
			}

			enemy.m_enemyBody.gameObject.layer = LayerMask.NameToLayer("dead");

			GameObject.Destroy(enemy.m_rbEnemy);
			GameObject.Destroy(enemy.m_bcEnemy);

			enemy.m_animatorBody.SetBool("dead", true);

			//GameObject.Destroy(enemy.hp_bar.gameObject, 3);
			GameObject.Destroy(enemy.gameObject, 3);
			Finish();
		}
	}

	[ActionCategory("EnemyAction")]
	[HutongGames.PlayMaker.Tooltip("EnemyAction")]
	public class Air : EnemyActionlBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			Debug.Log("air");
		}
		public override void OnUpdate()
		{
			base.OnUpdate();
			if( enemy.m_enemyBody.IsAir == false)
			{
				enemy.m_animatorBody.enabled = true;
				Finish();
			}
		}
	}

}
