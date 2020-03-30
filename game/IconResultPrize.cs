using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconResultPrize : MonoBehaviour
{
	public Image m_imgIcon;
	public TextMeshProUGUI m_txtNum;
	public UnityEngine.U2D.SpriteAtlas m_spriteAtlas;

	public void Initialize(DataItemParam _get_item)
	{
		MasterItemParam master = DataManager.Instance.masterItem.list.Find(p => p.item_id == _get_item.item_id);
		m_imgIcon.sprite = m_spriteAtlas.GetSprite(master.sprite_name);
		m_txtNum.text = string.Format("x{0}", _get_item.num);
	}

}
