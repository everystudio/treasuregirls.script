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
		private bool ntp_timer_initialized = false;
		public override void OnEnter()
		{
			base.OnEnter();
			ntp_timer_initialized = NTPTimer.Instance.Initialized;
			if (ntp_timer_initialized == false)
			{
				NTPTimer.Instance.RequestRefresh((_result) =>
				{
					if (_result == false)
					{
						Fsm.Event("network_error");
					}
					ntp_timer_initialized = _result;
				});
			}

			// ここ入らないけどね
			if(DataManager.Instance.Initialized && ntp_timer_initialized)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			base.OnUpdate();
			if (DataManager.Instance.Initialized && ntp_timer_initialized)
			{
				Finish();
			}
		}
	}

}
