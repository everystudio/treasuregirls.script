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

	public Sprite m_sprStarOn;
	public Sprite m_sprStarOff;

	public GameObject m_goShowRoot;
	public GameObject m_goNotInventry;
	public TextMeshProUGUI m_txtNotInventry;

	public GameObject m_goEquip;

	// おたからバージョン
	public DataTreasureParam m_dataTreasure;
	public DataTreasureEvent OnClickTreasure = new DataTreasureEvent();

	public DataWeaponParam m_dataWeapon;
	public DataWeaponEvent OnClickWeapon = new DataWeaponEvent();

	public void OnSelect(bool _bFlag)
	{
		m_animator.SetBool("select", _bFlag);
	}

	public void SelectTreasure(int _iSerial)
	{
		bool bFlag = false;
		if(m_dataTreasure != null)
		{
			bFlag = m_dataTreasure.serial == _iSerial;
		}
		else if( m_dataWeapon != null)
		{
			bFlag = m_dataWeapon.serial == _iSerial;
		}
		OnSelect(bFlag);
	}

	public void Initialize(DataTreasureParam _data , MasterTreasureParam _master)
	{
		m_dataTreasure = _data;

		if (_master != null)
		{
			m_goShowRoot.SetActive(true);
			m_goNotInventry.SetActive(false);
			m_imgIcon.sprite = m_spriteAtlas.GetSprite(_master.sprite_name);
			m_goEquip.SetActive(0 < _data.equip);

			if (0 < _data.level)
			{
				m_txtLevel.text = string.Format("+{0}", _data.level);
			}
			else
			{
				m_txtLevel.text = "";
			}
			for (int i = 0; i < star_list.Count; i++)
			{
				star_list[i].sprite = i < _master.rarity ? m_sprStarOn : m_sprStarOff;
			}
		}
		else
		{
			m_goShowRoot.SetActive(false);
			m_goNotInventry.SetActive(true);
		}

		m_btn.onClick.RemoveAllListeners();
		m_btn.onClick.AddListener(() =>
		{
			OnClickTreasure.Invoke(m_dataTreasure);
		});

	}


	public void Initialize(DataWeaponParam _data, MasterWeaponParam _master)
	{
		m_dataWeapon = _data;

		if (_master != null)
		{
			m_goShowRoot.SetActive(true);
			m_goNotInventry.SetActive(false);
			m_imgIcon.sprite = m_spriteAtlas.GetSprite(_master.sprite_name);
			m_goEquip.SetActive(0 < _data.equip);

			if (0 < _data.level)
			{
				m_txtLevel.text = string.Format("+{0}", _data.level);
			}
			else
			{
				m_txtLevel.text = "";
			}
			for (int i = 0; i < star_list.Count; i++)
			{
				star_list[i].sprite = i < _master.rarity ? m_sprStarOn : m_sprStarOff;
			}
		}
		else
		{
			m_goShowRoot.SetActive(false);
			m_goNotInventry.SetActive(true);
		}

		m_btn.onClick.RemoveAllListeners();
		m_btn.onClick.AddListener(() =>
		{
			OnClickWeapon.Invoke(m_dataWeapon);
		});

	}


}
