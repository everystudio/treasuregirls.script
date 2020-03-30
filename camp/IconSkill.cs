using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class IconSkill : MonoBehaviour
{
	public Button m_btn;
	public Image m_imgSkill;
	public TextMeshProUGUI m_txtCoolTime;
	public TextMeshProUGUI m_txtSkillName;

	public TextMeshProUGUI m_txtEmpty;
	public Animator m_animator;

	public DataSkillParam m_data;
	public MasterSkillParam m_master;

	public SpriteAtlas m_spriteAtlas;

	public DataSkillEvent OnClickIcon = new DataSkillEvent(); 

	public void Select(int _iPosition)
	{
		m_animator.SetBool("select", m_data.position == _iPosition);
	}

	public void Initialize(DataSkillParam _data , MasterSkillParam _master)
	{
		m_data = _data;
		m_master = _master;
		Select(0);

		if ( m_data.skill_id == 0)
		{
			m_imgSkill.gameObject.SetActive(false);
			m_txtEmpty.gameObject.SetActive(true);
		}
		else
		{
			m_imgSkill.gameObject.SetActive(true);
			m_imgSkill.sprite = m_spriteAtlas.GetSprite(m_master.sprite_name);
			m_txtEmpty.gameObject.SetActive(false);
		}

		m_txtSkillName.text = m_master.name;
		m_txtCoolTime.text = string.Format("{0:0.00}秒", m_master.cool_time);

		m_btn.onClick.RemoveAllListeners();
		m_btn.onClick.AddListener(() =>
		{
			OnClickIcon.Invoke(m_data);
		});
	}
	public void InitializeGame(DataSkillParam _data, MasterSkillParam _master)
	{
		Initialize(_data, _master);

		if(_data.skill_id != 0)
		{
			m_animator.speed = 1 / _master.cool_time;
			m_animator.SetBool("enable", true);
		}
	}

	public void CooltimeEnd()
	{
		// クールタイム終わりました
		m_btn.interactable = true;
	}
}
