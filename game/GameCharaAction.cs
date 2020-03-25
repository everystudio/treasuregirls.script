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
			chara.m_dataUnitParam.BuildPlayer();
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
			else if (is_move)
			{
				chara.rb2d.velocity = new Vector2(move_speed, chara.rb2d.velocity.y);
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
		public override void OnEnter()
		{
			base.OnEnter();
			time = 0.0f;
		}
		public override void OnUpdate()
		{
			base.OnUpdate();

			time += Time.deltaTime * chara.m_dataUnitParam.speed;

			if( 300 < time)
			{
				Fsm.Event("attack");
			}

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

			if (is_move)
			{
				Fsm.Event("escape");
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


}
