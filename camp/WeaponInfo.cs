using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInfo : MonoBehaviour
{
	public IconInventry m_icon;
	public TextMeshProUGUI m_txtName;
	public TextMeshProUGUI m_txtOutline;

	public TextMeshProUGUI m_txtGradeupPrice;

	public Button m_btnGradeup;
	public Button m_btnBuyCheck;

	public DataWeaponParam m_data;
	public MasterWeaponParam m_master;

	public TextMeshProUGUI m_txtAttack;
	public TextMeshProUGUI m_txtSpeed;


	public void Setup(DataWeaponParam _data, MasterWeaponParam _master)
	{
		m_icon.Initialize(_data, _master);
		if (_master != null)
		{
			m_txtName.text = _master.name;
			m_txtOutline.text = _master.name;
		}
		else
		{
			m_txtName.text = "なし";
			m_txtOutline.text = "-----";
		}

		if (10 <= _data.level)
		{
			m_txtGradeupPrice.text = "強化上限";
			m_btnGradeup.interactable = false;
		}
		else
		{
			if (_master != null)
			{
				int price = MasterWeapon.GetGradeupPrice(_data, _master);
				m_txtGradeupPrice.text = price.ToString();

				m_btnGradeup.interactable = price <= DataManager.Instance.GetGold();
			}
			else
			{
				m_txtGradeupPrice.text = "-----";
				m_btnGradeup.interactable = false;
			}
		}
		m_btnBuyCheck.interactable = _data.weapon_id != 0 && _data.equip == 0;


		if (_master != null)
		{
			MasterWeaponParam equip_level = _master.GetParam(_data.level);
			m_txtAttack.text = equip_level.attack.ToString();
			m_txtSpeed.text = equip_level.speed.ToString();
		}
		else
		{
			m_txtAttack.text = "---";
			m_txtSpeed.text = "---";
		}
	}


}
