using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CampMainAction
{
	[ActionCategory("TutorialManagerAction")]
	[HutongGames.PlayMaker.Tooltip("TutorialManagerAction")]
	public abstract class CampMainActionBase : FsmStateAction
	{
		protected CampMain campMain;
		public override void OnEnter()
		{
			base.OnEnter();
			campMain = Owner.gameObject.GetComponent<CampMain>();
		}
	}



}
