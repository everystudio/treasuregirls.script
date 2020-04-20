using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharaMain : MonoBehaviour
{
	public GameObject m_goIconRoot;
	public GameObject m_prefIconChara;

	public Button m_btnSet;
	public Button m_btnBuy;
	public TextMeshProUGUI m_txtBtnSet;
	public TextMeshProUGUI m_txtBtnBuy;
	public TextMeshProUGUI m_txtBtnPrice;

	public GameObject m_goGemLessCover;

	public List<IconCharaList> icon_chara_list = new List<IconCharaList>();

	public CharaView m_charaView;

}
