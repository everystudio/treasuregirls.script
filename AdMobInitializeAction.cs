using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
namespace AdMobInitializeAction 
{
	[ActionCategory("AdMobInitializeAction")]
	[HutongGames.PlayMaker.Tooltip("AdMobInitializeAction")]
	public class RequestAdMobInitialize : FsmStateAction
	{
		public override void OnEnter()
		{
			base.OnEnter();
			if (AdMobInitialize.Instance.IsAdMobInitialized)
			{
				Finish();
			}
			else
			{
				MobileAds.Initialize(initStatus =>
				{
					Debug.Log(initStatus);
					// 何が正常終了化わからん
					AdMobInitialize.Instance.AdMobInitialized(true);
					Finish();
				});
			}
		}
	}
	[ActionCategory("AdMobInitializeAction")]
	[HutongGames.PlayMaker.Tooltip("AdMobInitializeAction")]
	public class CheckAdMobInitialize : FsmStateAction
	{
		public override void OnUpdate()
		{
			base.OnUpdate();
			if (AdMobInitialize.Instance.IsAdMobInitialized)
			{
				Finish();
			}
		}
	}

}
