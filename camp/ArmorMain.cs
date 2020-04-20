using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class ArmorMain : MonoBehaviour
{
	public Image m_imgChara;
	public List<IconArmor> icon_armor_list;

	public GameObject m_prefArmorIcon;
	public GameObject m_goRootLeft;
	public GameObject m_goRootRight;

	public TextMeshProUGUI m_txtTotalHP;
	public TextMeshProUGUI m_txtTotalDef;
	public TextMeshProUGUI m_txtTotalMind;
	public TextMeshProUGUI m_txtTotalLuck;

	public TextMeshProUGUI m_txtArmorHP;
	public TextMeshProUGUI m_txtArmorDef;
	public TextMeshProUGUI m_txtArmorMind;
	public TextMeshProUGUI m_txtArmorLuck;

	public TextMeshProUGUI m_txtArmorName;
	public Image m_imgArmorIcon;
	public Button m_btnUpGrade;
	public TextMeshProUGUI m_txtUpgradeGold;
	public SpriteAtlas m_spriteAtlasArmor;
	public SpriteAtlas m_spriteAtlasChara;

	public PanelPotion m_panelPotion;

	public void IconSelect( string _strPosition)
	{
		foreach( IconArmor icon in icon_armor_list)
		{
			icon.IconSelect(_strPosition);
		}
	}


	public void ShowParamsTotal()
	{
		int total_hp = 0;
		int total_def = 0;
		int total_mind = 0;
		int total_luck = 0;

		for( int i = 0; i < MasterArmor.ArmorPositionArr.Length; i++)
		{
			DataArmorParam data = DataManager.Instance.dataArmor.list.Find(p => p.position == MasterArmor.ArmorPositionArr[i]);
			MasterArmorParam master = DataManager.Instance.masterArmor.list.Find(p => p.armor_id == data.armor_id);
			total_hp += master.hp;
			total_def += master.def;
			total_mind += master.mind;
			total_luck += master.luck;
		}

		m_txtTotalHP.text = total_hp.ToString();
		m_txtTotalDef.text = total_def.ToString();
		m_txtTotalMind.text = total_mind.ToString();
		m_txtTotalLuck.text = total_luck.ToString();

	}
	public void ShowParamsArmor( string _strPosition )
	{
		//Debug.Log(_strPosition);
		int total_hp = 0;
		int total_def = 0;
		int total_mind = 0;
		int total_luck = 0;

		DataArmorParam data = DataManager.Instance.dataArmor.list.Find(p => p.position == _strPosition);
		MasterArmorParam master = DataManager.Instance.masterArmor.list.Find(p => p.armor_id == data.armor_id);

		m_imgArmorIcon.sprite = m_spriteAtlasArmor.GetSprite(master.sprite_name);
		m_txtArmorName.text = master.armor_name;

		if( 0 < master.next_id)
		{
			m_txtUpgradeGold.text = master.coin.ToString();
			m_btnUpGrade.interactable = DataManager.Instance.GetCoin() <= master.coin;
		}
		else
		{
			m_txtUpgradeGold.text = "-----";
			m_btnUpGrade.interactable = false;
		}


		total_hp += master.hp;
		total_def += master.def;
		total_mind += master.mind;
		total_luck += master.luck;

		m_txtArmorHP.text = total_hp.ToString();
		m_txtArmorDef.text = total_def.ToString();
		m_txtArmorMind.text = total_mind.ToString();
		m_txtArmorLuck.text = total_luck.ToString();


	}

}
