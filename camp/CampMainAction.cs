using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CampMainAction
{
	[ActionCategory("CampMainAction")]
	[HutongGames.PlayMaker.Tooltip("CampMainAction")]
	public abstract class CampMainActionBase : FsmStateAction
	{
		protected CampMain campMain;
		public override void OnEnter()
		{
			base.OnEnter();
			campMain = Owner.gameObject.GetComponent<CampMain>();
		}
	}

	[ActionCategory("CampMainAction")]
	[HutongGames.PlayMaker.Tooltip("CampMainAction")]
	public class setup : CampMainActionBase
	{
		public UnityEngine.Audio.AudioMixer mixer;
		public override void OnEnter()
		{
			base.OnEnter();
			//gamemain.m_panelPauseMenu.m_soundvolumeBGM.SetVolume(DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_BGM));

			mixer.SetFloat("BGM", Mathf.Lerp(
				Defines.SOUND_VOLME_MIN,
				Defines.SOUND_VOLUME_MAX,
				DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_BGM)));
			mixer.SetFloat("SE", Mathf.Lerp(
				Defines.SOUND_VOLME_MIN,
				Defines.SOUND_VOLUME_MAX,
				DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_SE)));

			BGMControl.Instance.Play("peaceful_loop");
			Finish();
		}
	}




}
