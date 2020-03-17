using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class BannerSkill : MonoBehaviour
{
	public Image m_imgIcon;
	public TextMeshProUGUI m_txtName;
	public TextMeshProUGUI m_txtOutline;
	public GameObject m_goEmpty;
	public Button m_btn;
	public TextMeshProUGUI m_txtCoolTime;

	public SpriteAtlas m_spriteAtlas;
	public DataSkillEvent OnClickBanner = new DataSkillEvent();

	public DataSkillParam m_dataSkillParam;
	public MasterSkillParam m_masterSkillParam;

	public Animator m_animator;

	public void Select(int _iSkillId)
	{
		m_animator.SetBool("select", m_dataSkillParam.skill_id == _iSkillId);
	}

	public void Initialize(DataSkillParam _data, MasterSkillParam _master)
	{
		m_dataSkillParam = _data;
		m_masterSkillParam = _master;

		Select(-1);

		if ( 0 < _master.skill_id)
		{
			m_goEmpty.SetActive(false);
			m_imgIcon.gameObject.SetActive(true);
			m_imgIcon.sprite = m_spriteAtlas.GetSprite(_master.sprite_name);
			m_txtCoolTime.text = string.Format("クールタイム:{0:0.00}秒", _master.cool_time);
		}
		else
		{
			m_goEmpty.SetActive(true);
			m_imgIcon.gameObject.SetActive(false);
			m_txtCoolTime.text = "";
		}

		m_txtName.text = _master.name;
		m_txtOutline.text = _master.outline;

		m_btn.onClick.RemoveAllListeners();
		m_btn.onClick.AddListener(() =>
		{
			OnClickBanner.Invoke(m_dataSkillParam);
		});
	}

}
