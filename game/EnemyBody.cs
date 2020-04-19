using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
	public Enemy enemy;
	public bool IsAir = true;
	public bool IsHitPlayer = true;
	void OnCollisionEnter2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == "player")
		{
			IsHitPlayer = true;
		}
		else if(_collision.gameObject.name == "floor")
		{
			IsAir = false;
		}
	}

	void OnCollisionExit2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == "player")
		{
			IsHitPlayer = false;
		}
		else if (_collision.gameObject.name == "floor")
		{
			IsAir = true;
		}
	}
	public void EventAttackSlash()
	{
		//Debug.Log("EventAttackSlash");
		AttackEffect script = PrefabManager.Instance.MakeScript<AttackEffect>(GameMain.Instance.player_chara.m_prefAttack, enemy.m_posAttack);
		script.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);
		script.transform.localScale = Vector3.one * 2.0f;

		script.Initialize(enemy.dataUnitParam, null, "player");
	}
	public void EventAttackEnd()
	{
		//Debug.Log("EventAttackEnd");
		enemy.OnAttackEnd.Invoke();
	}

}
