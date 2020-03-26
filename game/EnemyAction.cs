using HutongGames.PlayMaker;
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
		public FsmInt enemy_id;
		public FsmInt level;
		public override void OnEnter()
		{
			base.OnEnter();
			MasterEnemyParam master_enemy = DataManager.Instance.masterEnemy.list.Find(p => p.enemy_id == enemy_id.Value);
			enemy.dataUnitParam.BuildEnemy(master_enemy,level.Value);
			Finish();
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

			enemy.attack_timer = enemy.attack_interval;
		}
		public override void OnUpdate()
		{
			base.OnUpdate();
			enemy.hp_bar.SetValueMax(enemy.dataUnitParam.hp_max);
			enemy.hp_bar.SetValueCurrent(enemy.dataUnitParam.hp);


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
		}
		public override void OnUpdate()
		{
			base.OnUpdate();
			enemy.hp_bar.SetValueMax(enemy.dataUnitParam.hp_max);
			enemy.hp_bar.SetValueCurrent(enemy.dataUnitParam.hp);

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
		}
		public override void OnUpdate()
		{
			enemy.hp_bar.SetValueMax(enemy.dataUnitParam.hp_max);
			enemy.hp_bar.SetValueCurrent(enemy.dataUnitParam.hp);

			base.OnUpdate();
			enemy.attack_timer -= Time.deltaTime;

			if(enemy.dataUnitParam.hp <= 0)
			{
				Fsm.Event("dead");
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

			enemy.m_enemyBody.gameObject.layer = LayerMask.NameToLayer("dead");

			GameObject.Destroy(enemy.m_rbEnemy);
			GameObject.Destroy(enemy.m_bcEnemy);

			enemy.m_animatorBody.SetBool("dead", true);
			Finish();
		}
	}
}
