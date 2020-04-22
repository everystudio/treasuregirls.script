using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureInfo : MonoBehaviour
{

	public IconInventry m_icon;
	public TextMeshProUGUI m_txtName;
	public TextMeshProUGUI m_txtOutline;

	public TextMeshProUGUI m_txtGradeupPrice;

	public Button m_btnGradeup;
	public Button m_btnBuyCheck;

	public DataTreasureParam m_dataTreasure;
	public MasterTreasureParam m_masterTreasure;

	public void Setup(DataTreasureParam _data , MasterTreasureParam _master)
	{
		m_icon.Initialize(_data, _master);
		if (_master != null)
		{
			m_txtName.text = _master.name;
			m_txtOutline.text = _master.GetOutline();
		}
		else
		{
			m_txtName.text = "なし";
			m_txtOutline.text = "-----";
		}

		if ( 10 <= _data.level)
		{
			m_txtGradeupPrice.text = "強化上限";
			m_btnGradeup.interactable = false;
		}
		else
		{
			if (_master != null)
			{
				int price = MasterTreasure.GetGradeupPrice(_data, _master);
				m_txtGradeupPrice.text = price.ToString();

				m_btnGradeup.interactable = price <= DataManager.Instance.GetCoin();
			}
			else
			{
				m_txtGradeupPrice.text = "-----";
				m_btnGradeup.interactable = false;
			}
		}
		m_btnBuyCheck.interactable = _data.treasure_id != 0 && _data.equip == 0;
	}


}
