using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataManagerAction 
{
	[ActionCategory("DataManagerAction")]
	[HutongGames.PlayMaker.Tooltip("DataManagerAction")]
	public class Wait_DataManagerInitialized : FsmStateAction
	{
		public override void OnEnter()
		{
			base.OnEnter();
			if(DataManager.Instance.Initialized)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			if (DataManager.Instance.Initialized)
			{
				Debug.Log("aaa");
				Finish();
			}
		}
	}

}
