using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class IconPotion : MonoBehaviour
{
	public DataPotionParam m_dataPotion;
	public MasterPotionParam m_masterPotion;

	public Button m_btn;
	public Image m_imgPotion;
	public TextMeshProUGUI m_txtNum;
	public TextMeshProUGUI m_txtCoolTime;
	public TextMeshProUGUI m_txtName;

	public SpriteAtlas m_spriteAtlas;
	public Animator m_animator;
	public bool is_gamemode;

	public bool request_use;

	public void Initialize(DataPotionParam _data , MasterPotionParam _master)
	{
		m_dataPotion = _data;
		m_masterPotion = _master;

		m_btn.onClick.RemoveAllListeners();

		if (m_masterPotion == null)
		{
			Debug.Log(m_dataPotion.potion_id);
		}
		m_txtName.text = m_masterPotion.name;
		m_imgPotion.sprite = m_spriteAtlas.GetSprite(m_masterPotion.sprite_name);

		m_txtNum.text = string.Format("x{0}", _data.num);
		m_txtCoolTime.text = string.Format("{0:0.00}秒", m_masterPotion.cool_time);
	}

	public void UseUpdate()
	{
		m_txtNum.text = string.Format("x{0}", m_dataPotion.num);

		if (0 < m_dataPotion.num)
		{
			m_btn.interactable = false;

			m_animator.SetTrigger("charge");
		}
		else
		{
			m_animator.SetBool("enable", false);
		}

	}


	public void InitializeGame(DataPotionParam _data, MasterPotionParam _master)
	{
		Initialize(_data, _master);
		m_animator.speed = 1/_master.cool_time;
		m_animator.SetBool("enable", 0 < _data.num);
	}

	public void CooltimeEnd()
	{
		// クールタイム終わりました
		if (request_use)
		{
			m_btn.onClick.Invoke();
		}
		else
		{
			m_btn.interactable = true;
		}
	}

}
