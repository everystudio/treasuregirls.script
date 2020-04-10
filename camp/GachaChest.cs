using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GachaChest : MonoBehaviour
{
	public Animator m_animator;

	public Image m_imgChest;
	public Image m_imgGetItem;

	public Button m_btnChest;

	public bool m_bOpened;

	public GameObject[] rarity_star_arr;

	public UnityEvent OnChestOpen = new UnityEvent();

	public void Initialize(Sprite _sprChest , Sprite _sprItem , int _iRarity)
	{
		m_imgChest.sprite = _sprChest;
		m_imgGetItem.sprite = _sprItem;
		m_bOpened = false;
		m_btnChest.onClick.RemoveAllListeners();
		m_btnChest.onClick.AddListener(() =>
		{
			m_animator.SetTrigger("open1");
		});

		for( int i = 0; i < rarity_star_arr.Length; i++)
		{
			rarity_star_arr[i].SetActive(i < _iRarity);
		}

	}
	public void HandleChestOpen()
	{
		if (m_bOpened == false)
		{
			m_bOpened = true;
			OnChestOpen.Invoke();
		}
	}
}
