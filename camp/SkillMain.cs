using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillMain : MonoBehaviour
{
	public CharaView m_charaView;


	public Button m_btnSet;
	public Button m_btnBuy;
	public Button m_btnEdit;
	public Button m_btnList;
	public Button m_btnShop;
	public Button m_btnBack;

	public void AllButtonClose()
	{
		m_btnSet.gameObject.SetActive(false);
		m_btnBuy.gameObject.SetActive(false);
		m_btnEdit.gameObject.SetActive(false);
		m_btnList.gameObject.SetActive(false);
		m_btnShop.gameObject.SetActive(false);
		m_btnBack.gameObject.SetActive(false);
	}

}
