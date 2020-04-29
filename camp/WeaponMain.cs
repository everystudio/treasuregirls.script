using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponMain : MonoBehaviour
{
	public WeaponInfo m_weaponInfo;

	public Button m_btnBuy;
	public Button m_btnGradeup;

	public Button m_btnEquip;
	public Button m_btnAlbum;
	public Button m_btnList;

	public Button m_btnBulk;
	public Button m_btnBuyBulk;
	public Button m_btnCancel;
	public GameObject m_goBulkBuyWindow;
	public TextMeshProUGUI m_txtBulkCoin;

	public List<IconInventry> weapon_list;
	public GameObject m_goListContents;
	public GameObject m_prefIconInventry;

	// 売却系
	public GameObject m_goBuyWindow;
	public IconInventry icon_buy;
	public TextMeshProUGUI m_txtPrice;
	public Button m_btnBuyYes;
	public Button m_btnBuyCancel;

	public TextMeshProUGUI m_txtListTitle;

	public void SelectListData(int _iSerial)
	{
		foreach (IconInventry icon in weapon_list)
		{
			icon.SelectTreasure(_iSerial);
		}
	}
	public void SelectListData_weapon_id(int _iWeaponId)
	{
		foreach (IconInventry icon in weapon_list)
		{
			icon.OnSelect(icon.m_dataWeapon.weapon_id == _iWeaponId);
		}
	}

}
