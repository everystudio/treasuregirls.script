using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillMain : MonoBehaviour
{
	public CharaView m_charaView;


	public Button m_btnSet;
	public Button m_btnBuy;
	public TextMeshProUGUI m_txtPrice;
	public Button m_btnEdit;
	public Button m_btnList;
	public Button m_btnShop;
	public Button m_btnBack;

	// 使ってる場所で内容が変わるので扱い注意
	public List<BannerSkill> skill_banner_list;

	public GameObject m_goSettingSkillRoot;
	public GameObject m_prefBannerSkill;

	public GameObject m_goListContentsRoot;

	public GameObject m_goViewSkill_Setting;
	public GameObject m_goViewSkill_List;

	public void SkillBannerSelect(int _iSkillId)
	{
		foreach (BannerSkill banner in skill_banner_list)
		{
			banner.Select(_iSkillId);
		}
	}


	public void AllButtonClose()
	{
		m_btnSet.gameObject.SetActive(false);
		m_btnBuy.gameObject.SetActive(false);
		m_btnEdit.gameObject.SetActive(false);
		m_btnList.gameObject.SetActive(false);
		m_btnShop.gameObject.SetActive(false);
		m_btnBack.gameObject.SetActive(false);

		m_btnSet.onClick.RemoveAllListeners();
		m_btnBuy.onClick.RemoveAllListeners();
		m_btnEdit.onClick.RemoveAllListeners();
		m_btnList.onClick.RemoveAllListeners();
		m_btnShop.onClick.RemoveAllListeners();
		m_btnBack.onClick.RemoveAllListeners();


	}

}
