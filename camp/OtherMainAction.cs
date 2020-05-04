using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OtherMainAction 
{
	[ActionCategory("OtherMainAction")]
	[HutongGames.PlayMaker.Tooltip("OtherMainAction")]
	public abstract class OtherMainActionBase : FsmStateAction
	{
		protected OtherMain other;
		public override void OnEnter()
		{
			base.OnEnter();
			other = Owner.GetComponent<OtherMain>();
		}
	}

	[ActionCategory("OtherMainAction")]
	[HutongGames.PlayMaker.Tooltip("OtherMainAction")]
	public class sound : OtherMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();

			other.m_goVolumeSetting.SetActive(true);
			other.m_goHelpRoot.SetActive(false);

			ButtonInteractable(false);

			other.m_svBGM.OnChangeEvent.AddListener(() =>
			{
				ButtonInteractable(true);
			});
			other.m_svSE.OnChangeEvent.AddListener(() =>
			{
				ButtonInteractable(true);
			});

			other.m_btnVolumeSet.onClick.AddListener(() =>
			{
				ButtonInteractable(false);

				DataManager.Instance.user_data.WriteFloat(Defines.KEY_SOUNDVOLUME_BGM, other.m_svBGM.rate);
				DataManager.Instance.user_data.WriteFloat(Defines.KEY_SOUNDVOLUME_SE, other.m_svSE.rate);
			});

			other.m_btnVolumeCancel.onClick.AddListener(() =>
			{
				float rate_bgm = DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_BGM);
				float rate_se = DataManager.Instance.user_data.ReadFloat(Defines.KEY_SOUNDVOLUME_SE);
				other.m_svBGM.SetVolume(rate_bgm);
				other.m_svBGM.SetSlider(rate_bgm);
				other.m_svSE.SetVolume(rate_se);
				other.m_svSE.SetSlider(rate_se);
			});

			other.m_btnVolumeDefault.onClick.AddListener(() =>
			{
				DataManager.Instance.user_data.WriteFloat(Defines.KEY_SOUNDVOLUME_BGM, Defines.SOUND_VOLME_DEFAULT);
				DataManager.Instance.user_data.WriteFloat(Defines.KEY_SOUNDVOLUME_SE, Defines.SOUND_VOLME_DEFAULT);
				other.m_svBGM.SetVolume(Defines.SOUND_VOLME_DEFAULT);
				other.m_svBGM.SetSlider(Defines.SOUND_VOLME_DEFAULT);
				other.m_svSE.SetVolume(Defines.SOUND_VOLME_DEFAULT);
				other.m_svSE.SetSlider(Defines.SOUND_VOLME_DEFAULT);
			});
		}

		private void ButtonInteractable(bool _bFlag)
		{
			other.m_btnVolumeSet.interactable = _bFlag;
			other.m_btnVolumeCancel.interactable = _bFlag;
			other.m_btnVolumeDefault.interactable = _bFlag;
		}

		public override void OnExit()
		{
			base.OnExit();
			other.m_goVolumeSetting.SetActive(false);
			other.m_svBGM.OnChangeEvent.RemoveAllListeners();
			other.m_svSE.OnChangeEvent.RemoveAllListeners();
			other.m_btnVolumeSet.onClick.RemoveAllListeners();

			other.m_btnVolumeCancel.onClick.RemoveAllListeners();
			other.m_btnVolumeDefault.onClick.RemoveAllListeners();

		}

	}
	[ActionCategory("OtherMainAction")]
	[HutongGames.PlayMaker.Tooltip("OtherMainAction")]
	public class help : OtherMainActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			other.m_goVolumeSetting.SetActive(false);
			other.m_goHelpRoot.SetActive(true);

			other.m_prefBannerHelp.SetActive(false);
			MonoBehaviourEx.DeleteObjects<BannerHelp>(other.m_goBannerRoot);
			foreach( MasterHelpParam param in DataManager.Instance.masterHelp.list)
			{
				BannerHelp banner = PrefabManager.Instance.MakeScript<BannerHelp>(other.m_prefBannerHelp, other.m_goBannerRoot);
				banner.m_txtTitle.text = param.title;
				banner.m_masterParam = param;
				banner.m_btn.onClick.AddListener(() =>
				{
					//Debug.Log(banner.m_masterParam.help_id);
					other.m_txtHelpOutline.text = banner.m_masterParam.message;
				});
			}
			other.m_txtHelpOutline.text = "-----";
		}

	}

}





