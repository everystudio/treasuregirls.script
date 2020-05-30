using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

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
			// デフォルトの設定にする場合
			Screen.sleepTimeout = SleepTimeout.SystemSetting;

			mixer.SetFloat("BGM", Mathf.Lerp(
				Defines.SOUND_VOLME_MIN,
				Defines.SOUND_VOLUME_MAX,
				DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_BGM)));
			mixer.SetFloat("SE", Mathf.Lerp(
				Defines.SOUND_VOLME_MIN,
				Defines.SOUND_VOLUME_MAX,
				DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_SE)));

			BGMControl.Instance.Play("peaceful_loop");

			if( !DataManager.Instance.user_data.HasKey(Defines.REVIEWED) && 1 < DataManager.Instance.dataFloor.list.FindAll(p=>2 <= p.status).Count)
			{
				Fsm.Event("review");
			}
			else
			{
				Finish();
			}
		}
	}

	[ActionCategory("CampMainAction")]
	[HutongGames.PlayMaker.Tooltip("CampMainAction")]
	public class review : CampMainActionBase
	{
		public GameObject panel_review;
		public UnityEngine.UI.Button btnClose;

		public override void OnEnter()
		{
			base.OnEnter();
			panel_review.SetActive(true);

			if (!DataManager.Instance.user_data.HasKey(Defines.REVIEWED))
			{
				DataManager.Instance.user_data.Write(Defines.REVIEWED, NTPTimer.Instance.now.ToString("yyyy/MM/dd HH:mm:ss"));
				DataManager.Instance.AddGem(100);
				DataManager.Instance.user_data.Save();
			}

			btnClose.onClick.AddListener(() =>
			{
#if UNITY_IOS
				if (Device.RequestStoreReview())
				{
					Finish();
				}
				else
				{
					Finish();
				}
#else
			Finish();
#endif
			});
		}
		public override void OnExit()
		{
			base.OnExit();
			panel_review.SetActive(false);
			btnClose.onClick.RemoveAllListeners();
		}
	}



}
