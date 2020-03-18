using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class IconInventry : MonoBehaviour
{
	public Button m_btn;
	public Image m_imgIcon;
	public List<Image> star_list;

	public Animator m_animator;

	public SpriteAtlas m_spriteAtlas;
	public TextMeshProUGUI m_txtLevel;


	public void Initialize(DataTreasureParam _data , MasterTreasureParam _master)
	{
		m_imgIcon.sprite = m_spriteAtlas.GetSprite(_master.sprite_name);

		if( 1 < _data.level)
		{
			m_txtLevel.text = string.Format("+{0}", _data.level);
		}
	}



}
