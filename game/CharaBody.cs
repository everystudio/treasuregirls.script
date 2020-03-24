using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBody : MonoBehaviour
{
	public bool IsEnemy;
	public Enemy enemy;
	void OnCollisionEnter2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == Defines.TAG_ENEMY)
		{
			Debug.Log("start enemy");
			IsEnemy = true;
		}
	}

	void OnCollisionExit2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == Defines.TAG_ENEMY)
		{
			Debug.Log("exit enemy");
			IsEnemy = false;
		}
	}

}
