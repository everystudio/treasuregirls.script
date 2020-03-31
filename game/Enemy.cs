using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
	public EnemyBody m_enemyBody;
	public SpriteRenderer m_sprEnemy;
	public Rigidbody2D m_rbEnemy;
	[HideInInspector] public BoxCollider2D m_bcEnemy;
	public EnemySearch enemy_search;

	public float move;
	public float attack_interval;
	public float attack_timer;

	public EnergyBar hp_bar;

	public Animator m_animatorBody;

	public DataUnitParam dataUnitParam = new DataUnitParam();
	public UnityEvent OnAttackEnd = new UnityEvent();

	public DropObject drop_object;

	private void Start()
	{
		if (m_sprEnemy.gameObject.GetComponent<BoxCollider2D>() != null)
		{
			Destroy(m_sprEnemy.gameObject.GetComponent<BoxCollider2D>());
		}
		m_bcEnemy = m_sprEnemy.gameObject.AddComponent<BoxCollider2D>();

		MasterItemParam master_item = new MasterItemParam();
		master_item.item_id = 1;
		master_item.sprite_name = "BlueLootBox_18_t";
		drop_object.Initialize(master_item);
	}

	public void Damage(DataUnitParam _attack , MasterSkillParam _skill)
	{
		int damage = _attack.CalcDamage(dataUnitParam, null);
		dataUnitParam.hp -= damage;
	}
}
