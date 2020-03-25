using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public EnemyBody m_enemyBody;
	public SpriteRenderer m_sprEnemy;
	public Rigidbody2D m_rbEnemy;

	public EnemySearch enemy_search;

	public float move;
	public float attack_inverval;

	public EnergyBar hp_bar;

	public DataUnitParam dataUnitParam = new DataUnitParam();

	private void Start()
	{
		if (m_sprEnemy.gameObject.GetComponent<BoxCollider2D>() != null)
		{
			Destroy(m_sprEnemy.gameObject.GetComponent<BoxCollider2D>());
		}
		m_sprEnemy.gameObject.AddComponent<BoxCollider2D>();

	}
}
