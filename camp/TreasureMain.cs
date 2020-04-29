using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureMain : MonoBehaviour
{
	public GameObject m_prefIconInventry;

	public TreasureInfo m_treasureInfo;
	public Button m_btnBuy;
	public Button m_btnGradeup;

	public List<IconInventry> equip_treasure_list;
	public List<IconInventry> treasure_list;
	public GameObject m_rootSettingTreasure;

	public GameObject m_goRootList;
	public GameObject m_goListContents;
	public TextMeshProUGUI m_txtListTitle;

	public Button m_btnSet;
	public Button m_btnSyoji;
	public Button m_btnEdit;
	public Button m_btnAlbum;

	public Button m_btnBack;

	public GameObject m_goBulkPriceRoot;
	public TextMeshProUGUI m_txtBuyBulkPrice;
	public Button m_btnMenuBulk;
	public Button m_btnBuyBulk;
	public Button m_btnBuyBulkCancel;


	// 売却系
	public GameObject m_goBuyWindow;
	public IconInventry icon_buy;
	public TextMeshProUGUI m_txtPrice;
	public Button m_btnBuyYes;
	public Button m_btnBuyCancel;

	public void ButtonClose()
	{
		m_btnBuy.onClick.RemoveAllListeners();
		m_btnGradeup.onClick.RemoveAllListeners();

		m_btnSet.onClick.RemoveAllListeners();
		m_btnSyoji.onClick.RemoveAllListeners();
		m_btnEdit.onClick.RemoveAllListeners();
		m_btnAlbum.onClick.RemoveAllListeners();
		m_btnBack.onClick.RemoveAllListeners();

		m_btnBuyYes.onClick.RemoveAllListeners();
		m_btnBuyCancel.onClick.RemoveAllListeners();


		//m_btnBuy.gameObject.SetActive(false);
		//m_btnGradeup.gameObject.SetActive(false);

		m_btnSet.gameObject.SetActive(false);
		m_btnSyoji.gameObject.SetActive(false);
		m_btnEdit.gameObject.SetActive(false);
		m_btnAlbum.gameObject.SetActive(false);
		m_btnBack.gameObject.SetActive(false);

		//m_btnBuyYes.gameObject.SetActive(false);
		//m_btnBuyCancel.gameObject.SetActive(false);

	}

	public void SelectEquip(int _iEquip)
	{
		foreach (IconInventry icon in equip_treasure_list)
		{
			icon.OnSelect(icon.m_dataTreasure.equip == _iEquip);
		}
	}

	public void SelectListData(int _iSerial)
	{
		foreach( IconInventry icon in treasure_list)
		{
			icon.SelectTreasure(_iSerial);
		}
	}

}
