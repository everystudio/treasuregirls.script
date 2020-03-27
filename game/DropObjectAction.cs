using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DropObjectAction 
{
	[ActionCategory("DropObjectAction")]
	[HutongGames.PlayMaker.Tooltip("DropObjectAction")]
	public abstract class DropObjectActionBase : FsmStateAction
	{
		protected DropObject drop;
		public override void OnEnter()
		{
			base.OnEnter();
			drop = Owner.GetComponent<DropObject>();
		}
	}

	[ActionCategory("DropObjectAction")]
	[HutongGames.PlayMaker.Tooltip("DropObjectAction")]
	public class setup : DropObjectActionBase
	{
		public int power;
		public override void OnEnter()
		{
			base.OnEnter();
		}
		public override void OnUpdate()
		{
			base.OnUpdate();
			if( drop.m_master != null)
			{
				drop.m_spr.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1).normalized * power);
				Finish();
			}
		}
	}

	[ActionCategory("DropObjectAction")]
	[HutongGames.PlayMaker.Tooltip("DropObjectAction")]
	public class idle : DropObjectActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			drop.OnGet.AddListener(() =>
			{
				Finish();
			});
		}
	}

	[ActionCategory("DropObjectAction")]
	[HutongGames.PlayMaker.Tooltip("DropObjectAction")]
	public class get : DropObjectActionBase
	{
		public override void OnEnter()
		{
			base.OnEnter();
			//Debug.Log(drop.m_spr.gameObject.GetComponent<Rigidbody2D>());
			GameObject.Destroy(drop.m_spr.gameObject.GetComponent<Rigidbody2D>());
			GameObject.Destroy(drop.m_spr.gameObject.GetComponent<BoxCollider2D>());

			drop.transform.position = drop.m_spr.gameObject.transform.position;
			drop.m_spr.gameObject.transform.localPosition = Vector3.zero;

			drop.m_animator.enabled = true;
			drop.m_animator.SetTrigger("get");

			GameObject.Destroy(drop.gameObject, 3.0f);
		}

	}



}
