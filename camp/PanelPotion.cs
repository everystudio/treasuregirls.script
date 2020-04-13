using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class PanelPotion : MonoBehaviour
{
	public IconPotion m_iconPotion;

	public Button m_btnUpgrade;
	public Button m_btnAdd;

	public TextMeshProUGUI m_txtLabelUpgrade;
	public TextMeshProUGUI m_txtLabelAdd;

	public TextMeshProUGUI m_txtUpgradePrice;
	public TextMeshProUGUI m_txtAddPrice;

	public void Initialize( int _iCoin )
	{
		DataPotionParam data_potion = DataManager.Instance.dataPotion.list.Find(p => p.is_use == true);
		MasterPotionParam master_potion = DataManager.Instance.masterPotion.list.Find(p => p.potion_id == data_potion.potion_id);
		m_iconPotion.Initialize(data_potion, master_potion);

		if (0 < master_potion.next_potion_id)
		{
			m_txtUpgradePrice.text = master_potion.upgrade_coin.ToString();
		}
		else
		{
			m_txtUpgradePrice.text = "-----";
		}
		m_txtAddPrice.text = master_potion.add_coin.ToString();

		m_btnUpgrade.interactable = (master_potion.upgrade_coin <= _iCoin);
		m_btnAdd.interactable = (master_potion.add_coin <= _iCoin && data_potion.num < Defines.POTION_LIMIT );


		if(master_potion.next_potion_id == 0)
		{
			m_txtLabelUpgrade.text = "アップグレード\n強化上限です";
		}
		else if(_iCoin < master_potion.upgrade_coin)
		{
			m_txtLabelUpgrade.text = "コインが\n足りません";
		}
		else
		{
			m_txtLabelUpgrade.text = "ポーション\nアップグレード";
		}


		if ( Defines.POTION_LIMIT <= data_potion.num)
		{
			m_txtLabelAdd.text = "これ以上\n購入できません";
		}
		else if (_iCoin < master_potion.add_coin)
		{
			m_txtLabelAdd.text = "コインが\n足りません";
		}
		else
		{
			m_txtLabelAdd.text = "追加購入";
		}

	}

}
