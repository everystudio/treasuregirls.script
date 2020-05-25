using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpeedControlAction
{
	[ActionCategory("SpeedControlAction")]
	[HutongGames.PlayMaker.Tooltip("SpeedControlAction")]
	public abstract class SpeedControlActionBase : FsmStateAction
	{
		protected SpeedControl speedcontrol;
		public override void OnEnter()
		{
			base.OnEnter();
			speedcontrol = Owner.gameObject.GetComponent<SpeedControl>();
		}
	}

	[ActionCategory("SpeedControlAction")]
	[HutongGames.PlayMaker.Tooltip("SpeedControlAction")]
	public class startup : SpeedControlActionBase
	{
		public FsmInt speed;
		public override void OnEnter()
		{
			base.OnEnter();
			if( false == DataManager.Instance.user_data.HasKey(Defines.GAMESPEED))
			{
				DataManager.Instance.user_data.WriteInt(Defines.GAMESPEED, 1);
			}
			speed.Value = DataManager.Instance.user_data.ReadInt(Defines.GAMESPEED);
			Finish();
		}
	}

	[ActionCategory("SpeedControlAction")]
	[HutongGames.PlayMaker.Tooltip("SpeedControlAction")]
	public class idle : SpeedControlActionBase
	{
		public FsmInt speed;
		public UnityEngine.U2D.SpriteAtlas sprite_atlas;
		public override void OnEnter()
		{
			base.OnEnter();
			if (speed.Value < Defines.GAMESPEED_MIN)
			{
				speed.Value = Defines.GAMESPEED_MIN;
			}
			else if (Defines.GAMESPEED_MAX < speed.Value)
			{
				speed.Value = Defines.GAMESPEED_MIN;
			}
			speedcontrol.m_img.sprite = sprite_atlas.GetSprite(get_sprite(speed.Value));
			Time.timeScale = speed.Value;
		}
		private string get_sprite( int _iSpeed)
		{
			if( _iSpeed < Defines.GAMESPEED_MIN)
			{
				_iSpeed = Defines.GAMESPEED_MIN;
			}
			else if( Defines.GAMESPEED_MAX < _iSpeed)
			{
				_iSpeed = Defines.GAMESPEED_MAX;
			}
			return string.Format("gamespeed_{0}", _iSpeed);
		}
		public override void OnExit()
		{
			base.OnExit();
			Time.timeScale = 1.0f;
		}
	}



}
