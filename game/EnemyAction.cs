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
	public class Search : EnemyActionlBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			// 絶対消す
			enemy.hp_max = 100;
			enemy.hp = 100;

			enemy.hp_bar.SetValueMax(enemy.hp_max);
			enemy.hp_bar.SetValueCurrent(enemy.hp);
			enemy.enemy_search.IsFindPlayer = false;
			enemy.enemy_search.gameObject.SetActive(true);
		}
		public override void OnUpdate()
		{
			base.OnUpdate();
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

			Vector3 dir = GameCharaMain.Instance.m_sprChara.gameObject.transform.position -
				enemy.m_sprEnemy.gameObject.transform.position;

			float distance = Vector3.SqrMagnitude(dir);

			float move_speed = enemy.move;
			if (dir.x < 0)
			{
				move_speed *= -1;
			}
			enemy.m_rbEnemy.velocity = new Vector2(move_speed, enemy.m_rbEnemy.velocity.y);
			if (enemy.hp <= 0)
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
		private float interval;
		public override void OnEnter()
		{
			base.OnEnter();
			interval = 0;
		}
		public override void OnUpdate()
		{
			enemy.hp_bar.SetValueCurrent(enemy.hp);

			base.OnUpdate();
			interval += Time.deltaTime;

			if( enemy.hp <= 0)
			{
				Fsm.Event("dead");
			}
			else if (enemy.m_enemyBody.IsHitPlayer == false)
			{
				Fsm.Event("not_found");
			}
			else if (interval < enemy.attack_inverval)
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
			enemy.hp -= 35;

			Finish();
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

			Finish();
		}
	}
}
