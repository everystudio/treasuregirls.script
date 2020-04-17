using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCharaAction
{
	[ActionCategory("GameCharaAction")]
	[HutongGames.PlayMaker.Tooltip("GameCharaAction")]
	public abstract class GameCharaActionBase : FsmStateAction
	{
		protected GameCharaMain chara;
		public override void OnEnter()
		{
			base.OnEnter();
			chara = Owner.GetComponent<GameCharaMain>();
		}
	}

	[ActionCategory("GameCharaAction")]
	[HutongGames.PlayMaker.Tooltip("GameCharaAction")]
	public class setup : GameCharaActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			// いったん自動リセット
			chara.m_charaBody.transform.localPosition = Vector3.zero;

			chara.is_goal = false;
			chara.m_dataUnitParam.BuildPlayer();

			chara.m_hpBar.SetValueMax(chara.m_dataUnitParam.hp_max);
			chara.m_hpBar.SetValueCurrent(chara.m_dataUnitParam.hp);
			Finish();
		}
	}


	[ActionCategory("GameCharaAction")]
	[HutongGames.PlayMaker.Tooltip("GameCharaAction")]
	public class idle : GameCharaActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
		}
		public override void OnUpdate()
		{
			base.OnUpdate();

			bool is_move = false;
			if (Input.GetKey(KeyCode.RightArrow) || chara.m_arrowRight.is_press)
			{
				is_move = true;
			}
			else if (Input.GetKey(KeyCode.LeftArrow) || chara.m_arrowLeft.is_press)
			{
				is_move = true;
			}
			else
			{
				// 特になにをするわけではない
			}

			if (chara.m_charaBody.IsEnemy)
			{
				Fsm.Event("battle");
			}
			else if ( is_move)
			{
				Fsm.Event("move");
			}
			else if( chara.is_goal)
			{
				Fsm.Event("goal");
			}

		}
	}


	[ActionCategory("GameCharaAction")]
	[HutongGames.PlayMaker.Tooltip("GameCharaAction")]
	public class move : GameCharaActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			//Debug.Log("OnFixedUpdate");

			bool is_move = false;
			float move_speed = 0.0f;
			if (Input.GetKey(KeyCode.RightArrow) || chara.m_arrowRight.is_press)
			{
				is_move = true;
				move_speed = chara.move_power * 1.0f;
			}
			else if (Input.GetKey(KeyCode.LeftArrow) || chara.m_arrowLeft.is_press)
			{
				is_move = true;
				move_speed = chara.move_power * -1.0f;
			}

			if (chara.m_charaBody.IsEnemy)
			{
				Fsm.Event("battle");
			}
			else if (chara.is_goal)
			{
				Fsm.Event("goal");
			}
			else if (is_move)
			{
				//chara.rb2d.velocity = new Vector2(move_speed, chara.rb2d.velocity.y);
				//Debug.Log("kasu");
				chara.rb2d.transform.localPosition += new Vector3(move_speed*Time.deltaTime, 0.0f, 0.0f);
				//chara.rb2d.MovePosition(chara.rb2d.position + new Vector2(1.0f,0.0f));

			}
			else
			{
				Finish();
			}



		}

	}

	[ActionCategory("GameCharaAction")]
	[HutongGames.PlayMaker.Tooltip("GameCharaAction")]
	public class battle : GameCharaActionBase
	{
		public float time;
		public float attack_interval;
		public override void OnEnter()
		{
			base.OnEnter();
			time = 0.0f;
		}
		public override void OnUpdate()
		{
			base.OnUpdate();

			time += Time.deltaTime * chara.m_dataUnitParam.speed;

			if(attack_interval < time)
			{
				Fsm.Event("attack");
			}

			bool is_move = false;
			if (Input.GetKey(KeyCode.RightArrow) || chara.m_arrowRight.is_press)
			{
				// 右にしか敵がいない前提
				//is_move = true;
			}
			else if (Input.GetKey(KeyCode.LeftArrow) || chara.m_arrowLeft.is_press)
			{
				is_move = true;
			}
			else
			{
				// 特になにをするわけではない
			}

			if (is_move)
			{
				Fsm.Event("escape");
			}
			else if( chara.m_charaBody.IsEnemy == false)
			{
				Finish();
			}


		}
	}

	[ActionCategory("GameCharaAction")]
	[HutongGames.PlayMaker.Tooltip("GameCharaAction")]
	public class escape : GameCharaActionBase
	{
		private float delay_time;
		public override void OnEnter()
		{
			base.OnEnter();
			delay_time = 0;
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			bool is_move = false;
			float move_speed = 0.0f;
			if (Input.GetKey(KeyCode.RightArrow) || chara.m_arrowRight.is_press)
			{
				is_move = true;
				move_speed = chara.move_power;
			}
			else if (Input.GetKey(KeyCode.LeftArrow) || chara.m_arrowLeft.is_press)
			{
				is_move = true;
				move_speed = -1.0f * chara.move_power;
			}

			if (is_move)
			{
				chara.rb2d.velocity = new Vector2(move_speed, chara.rb2d.velocity.y) ;
			}

			delay_time += Time.deltaTime;
			if( 0.5f < delay_time)
			{
				Finish();
			}
		}
	}
	[ActionCategory("GameCharaAction")]
	[HutongGames.PlayMaker.Tooltip("GameCharaAction")]
	public class attack : GameCharaActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			chara.OnAttackEnd.AddListener(() =>
			{
				Finish();
			});
			chara.m_animator.SetTrigger("attack");
		}
		public override void OnExit()
		{
			base.OnExit();
			chara.OnAttackEnd.RemoveAllListeners();
		}
	}
	[ActionCategory("GameCharaAction")]
	[HutongGames.PlayMaker.Tooltip("GameCharaAction")]
	public class goal : GameCharaActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			chara.m_animator.SetBool("win", true);

			GameMain.Instance.IsGoal = true;
		}
	}
}
