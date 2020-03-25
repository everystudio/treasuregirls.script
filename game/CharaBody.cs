using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBody : MonoBehaviour
{
	public GameCharaMain game_chara_main;
	public bool IsEnemy;
	public Enemy enemy;
	void OnCollisionEnter2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == Defines.TAG_ENEMY)
		{
			//Debug.Log("start enemy");
			IsEnemy = true;
		}
	}

	void OnCollisionExit2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == Defines.TAG_ENEMY)
		{
			//Debug.Log("exit enemy");
			IsEnemy = false;
		}
	}
	public void EventAttackSlash()
	{
		AttackEffect script = PrefabManager.Instance.MakeScript<AttackEffect>(game_chara_main.m_prefAttack, game_chara_main.m_goAttackRoot);
		script.transform.localPosition = new Vector3(0.0f,0.0f,-1.0f);
		script.transform.localScale = Vector3.one * 2.0f;

		script.Initialize(game_chara_main.m_dataUnitParam, "enemy");

	}
	public void EventAttackEnd()
	{
		game_chara_main.OnAttackEnd.Invoke();
	}
}
