using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
	public DataUnitParam m_data;
	public MasterSkillParam m_skill;
	public string m_strTargetTag;

	public List<GameObject> history;

	public void Initialize(DataUnitParam _data ,MasterSkillParam _skill, string _target_tag)
	{
		m_data = _data;
		m_skill = _skill;
		m_strTargetTag = _target_tag;
		Destroy(gameObject, 5.0f);
	}


	void OnTriggerEnter2D(Collider2D _collider)
	{
		//Debug.Log(_collider.gameObject.tag);

		if (_collider.gameObject.tag == m_strTargetTag)
		{
			if( history.Contains(_collider.gameObject) == false)
			{
				if( m_strTargetTag == "enemy")
				{
					EnemyBody eb = _collider.gameObject.GetComponent<EnemyBody>();
					if( eb != null)
					{
						eb.enemy.Damage(m_data, m_skill);
					}
				}
				history.Add(_collider.gameObject);
			}
		}
	}


}
