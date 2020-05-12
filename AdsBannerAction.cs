using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdsBannerAction 
{
	[ActionCategory("AdsBannerAction")]
	[HutongGames.PlayMaker.Tooltip("AdsBannerAction")]
	public abstract class AdsBannerActionBase : FsmStateAction
	{
		protected AdsBanner banner;
		public override void OnEnter()
		{
			base.OnEnter();
			banner = Owner.GetComponent<AdsBanner>();
		}
	}


	[ActionCategory("AdsBannerAction")]
	[HutongGames.PlayMaker.Tooltip("AdsBannerAction")]
	public class Show : AdsBannerActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			banner.Show();
		}
	}


}
