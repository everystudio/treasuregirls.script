using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class IconArmor : MonoBehaviour
{
	public Button m_btn;
	public Image m_imgIcon;

	public TextMeshProUGUI m_txtLevel;
	public Animator m_animator;

	public SpriteAtlas m_spriteAtlasArmor;

	public DataArmorEvent OnClickDataArmor = new DataArmorEvent();
	public DataArmorParam m_dataArmor;

	public void IconSelect( string _position)
	{
		m_animator.SetBool("select", m_dataArmor.position == _position);
	}

	public void Initialize( DataArmorParam _data , List<MasterArmorParam> _masterList)
	{
		m_dataArmor = _data;
		MasterArmorParam master_current = _masterList.Find(p => p.armor_id == _data.armor_id);
		//Debug.Log(master_current.sprite_name);
		m_imgIcon.sprite = m_spriteAtlasArmor.GetSprite(master_current.sprite_name);

		if (1 < master_current.level)
		{
			m_txtLevel.text = string.Format("＋{0}", master_current.level-1);
		}
		else
		{
			m_txtLevel.text = "";
		}

		m_btn.onClick.RemoveAllListeners();
		m_btn.onClick.AddListener(() =>
		{
			OnClickDataArmor.Invoke(m_dataArmor);
		});



	}
}
