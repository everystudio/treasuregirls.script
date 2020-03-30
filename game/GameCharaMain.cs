using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameCharaMain : Singleton<GameCharaMain>
{
    public ArrowButton m_arrowLeft;
    public ArrowButton m_arrowRight;

    public SpriteRenderer m_sprChara;

    public Rigidbody2D rb2d;
    public BoxCollider2D collidor2d;

    public CharaBody m_charaBody;
    public EnergyBar m_hpBar;

    public float move_power = 1.0f;
    public float gravity = 20.0f;

    public bool is_move;

    public Animator m_animator;
    public DataUnitParam m_dataUnitParam = new DataUnitParam();

    public UnityEvent OnAttackEnd = new UnityEvent();

    public GameObject m_goAttackRoot;
    public GameObject m_prefAttack;
	public bool is_goal;

    public void Heal( int _iHeal )
    {
        m_dataUnitParam.Heal(_iHeal);
        m_hpBar.SetValueCurrent(m_dataUnitParam.hp);
    }

    public void Damage( int _iDamage)
    {
        m_dataUnitParam.Damage(_iDamage);
        m_hpBar.SetValueCurrent(m_dataUnitParam.hp);
    }
}
