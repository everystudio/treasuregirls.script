using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
	public bool is_setenemy;
	public int enemy_id;
	public int enemy_level;
	public void SetEnemyData( int _iEnemyId , int _iEnemyLevel)
	{
		is_setenemy = true;
		enemy_id = _iEnemyId;
		enemy_level = _iEnemyLevel;
	}
	public SpriteAtlas m_sprAtlas;

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
