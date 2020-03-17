using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class IconPotion : MonoBehaviour
{
	public DataPotionParam m_dataPotion;
	public MasterPotionParam m_masterPotion;

	public Button m_btn;
	public Image m_imgPotion;
	public TextMeshProUGUI m_txtNum;
	public TextMeshProUGUI m_txtCoolTime;
	public TextMeshProUGUI m_txtName;

	public SpriteAtlas m_spriteAtlas;

	public void Initialize(DataPotionParam _data , MasterPotionParam _master)
	{
		m_dataPotion = _data;
		m_masterPotion = _master;

		m_btn.onClick.RemoveAllListeners();

		if (m_masterPotion == null)
		{
			Debug.Log(m_dataPotion.potion_id);
		}
		m_txtName.text = m_masterPotion.name;
		m_imgPotion.sprite = m_spriteAtlas.GetSprite(m_masterPotion.sprite_name);

		m_txtNum.text = string.Format("x{0}", _data.num);
		m_txtCoolTime.text = string.Format("{0:0.00}秒", m_masterPotion.cool_time);

	}


}
