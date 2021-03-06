﻿using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraControlAction
{
	[ActionCategory("CameraControlAction")]
	[HutongGames.PlayMaker.Tooltip("CameraControlAction")]
	public abstract class CameraControlBase : FsmStateAction
	{
		protected CameraControl cameraControl;
		public override void OnEnter()
		{
			base.OnEnter();
			cameraControl = Owner.GetComponent<CameraControl>();
		}
	}

	[ActionCategory("CameraControlAction")]
	[HutongGames.PlayMaker.Tooltip("CameraControlAction")]
	public class Follow : CameraControlBase
	{
		public float offset_x;
		public float offset_y;
		public override void OnEnter()
		{
			base.OnEnter();
		}
		public override void OnUpdate()
		{
			base.OnUpdate();
			cameraControl.transform.position = new Vector3(
				cameraControl.m_gameChara.m_sprChara.gameObject.transform.position.x + offset_x,
				cameraControl.m_gameChara.m_sprChara.gameObject.transform.position.y + offset_y,
				cameraControl.transform.position.z
				);
		}
	}

}