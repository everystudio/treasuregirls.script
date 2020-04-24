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
    public OverrideSprite m_overrideSprite;

    public void Skill( DataSkillParam _data , MasterSkillParam _master)
    {
        Debug.Log(_master.prefab_name);
        AttackEffect script = PrefabManager.Instance.MakeScript<AttackEffect>(_master.prefab_name, m_goAttackRoot);
        Debug.Log(script);
        script.transform.localPosition = new Vector3(0.0f, 0.0f, -1.0f);
        script.transform.localScale = Vector3.one * 2.0f;

        script.Initialize(m_dataUnitParam,_master, "enemy");

    }

    public void Heal( int _iHeal )
    {
        m_dataUnitParam.Heal(_iHeal);
        m_hpBar.SetValueCurrent(m_dataUnitParam.hp);
    }

    public void Damage( int _iDamage)
    {
        m_dataUnitParam.Damage(_iDamage);
        //Debug.Log(string.Format("player hp:{0} damage:{1}", m_dataUnitParam.hp, _iDamage));
        m_hpBar.SetValueCurrent(m_dataUnitParam.hp);
    }

    public void Damage(DataUnitParam _attack, MasterSkillParam _skill)
    {
        int damage = _attack.CalcDamage(m_dataUnitParam, null);
        Damage(damage);
    }


}
