using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoButtonPotion : MonoBehaviour
{
	public Button m_btn;
	public GameObject m_goCover;
	public IconPotion icon_potion;

	public float recover_rate;
	public bool m_bFlag;
	public void Initialize(bool _bFlag , float _rate )
	{
		m_bFlag = _bFlag;
		recover_rate = _rate;
		toggle_button(m_bFlag);

		m_btn.onClick.RemoveAllListeners();
		m_btn.onClick.AddListener(() =>
		{
			m_bFlag = !m_bFlag;
			toggle_button(m_bFlag);
		});
	
	}

	public void toggle_button(bool _bFlag)
	{
		m_goCover.SetActive(!_bFlag);

		if (_bFlag)
		{
			GameMain.Instance.player_chara.m_dataUnitParam.OnChangeHp.AddListener(ChangeHp);
		}
		else
		{
			icon_potion.request_use = false;
			GameMain.Instance.player_chara.m_dataUnitParam.OnChangeHp.RemoveListener(ChangeHp);
		}
	}

	public void ChangeHp(int _iHp , int _iHpMax)
	{
		float rate = (float)_iHp / (float)_iHpMax;

		if(_iHp <= 0)
		{
			// すでになくなってる
			return;
		}

		/*
		Debug.Log(rate);
		Debug.Log(_iHp);
		Debug.Log(_iHpMax);
		*/
		if( rate < recover_rate)
		{
			if( icon_potion.m_btn.interactable)
			{
				icon_potion.m_btn.onClick.Invoke();
			}
			else
			{
				icon_potion.request_use = true;
			}
		}
	}



}
