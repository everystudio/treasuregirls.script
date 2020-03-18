using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreasureMain : MonoBehaviour
{
	public GameObject m_prefIconInventry;

	public TreasureInfo m_treasureInfo;

	public List<IconInventry> setting_treasure_list;
	public GameObject m_rootSettingTreasure;

	public GameObject m_goRootList;
	public GameObject m_goListContents;
	public TextMeshProUGUI m_txtListTitle;

	public Button m_btnSet;
	public Button m_btnSyoji;
	public Button m_btnEdit;
	public Button m_btnAlbum;

	public Button m_btnBack;

}
