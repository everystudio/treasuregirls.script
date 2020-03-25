using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : MonoBehaviour
{
	public Enemy enemy;

	public bool IsHitPlayer = true;
	void OnCollisionEnter2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == "player")
		{
			IsHitPlayer = true;
		}
	}

	void OnCollisionExit2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == "player")
		{
			IsHitPlayer = false;
		}
	}

}
