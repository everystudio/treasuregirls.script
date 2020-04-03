using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMain : MonoBehaviour
{

	public PanelStage m_panelStage;




	public Button m_btnQuest;

	public CharaView m_charaView;

	public WeaponInfo m_weaponInfo;
	public List<IconInventry> treasure_list;
	public List<IconArmor> icon_armor_list;

	#region Armor
	public GameObject m_prefIconArmor;
	public GameObject m_goArmorLeft;
	public GameObject m_goArmorRight;
	#endregion

	#region Treasure
	public GameObject m_prefIconTreasure;
	public GameObject m_goTreasureRoot;
	#endregion

	public TextMeshProUGUI m_txtTotalHP;
	public TextMeshProUGUI m_txtTotalDef;
	public TextMeshProUGUI m_txtTotalMind;
	public TextMeshProUGUI m_txtTotalLuck;

}
