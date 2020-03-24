using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
	public bool IsFindPlayer = false;
	void OnTriggerEnter2D(Collider2D _collider)
	{
		Debug.Log(_collider.name);
		if( _collider.tag == "player")
		{
			IsFindPlayer = true;
		}
	}
}
