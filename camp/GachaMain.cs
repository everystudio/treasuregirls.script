using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;
using UnityEngine.UI;

public class GachaMain: Singleton<GachaMain>
{
	public GameObject m_goBackground;

	public GameObject m_prefChest;

	public GameObject m_goPanelOne;
	public GameObject m_goPanelTen;
	public GameObject m_goTenChestRoot;

	public Button m_btnCloseOne;
	public Button m_btnCloseTen;
	public Button m_btnBulkOpen;

	public SpriteAtlas m_sprAtlasIcons;
	public SpriteAtlas m_sprAtlasWeapon;
	public SpriteAtlas m_sprAtlasTreasure;

	private int open_count;
	public List<GachaChest> chest_list = new List<GachaChest>();


	public class ChestData
	{
		public int rarity;
		public Sprite spr_chest;
		public Sprite spr_item;
	}


	public UnityEvent OnGachaFinished = new UnityEvent();
	public void Close()
	{
		m_goBackground.SetActive(false);
		m_goPanelOne.SetActive(false);
		m_goPanelTen.SetActive(false);
	}

	public void GachaSingle(ChestData _data)
	{
		m_goBackground.SetActive(true);
		m_goPanelOne.SetActive(true);

		m_prefChest.SetActive(false);

		GachaChest chest = PrefabManager.Instance.MakeScript<GachaChest>(m_prefChest, m_goPanelOne);

		chest.Initialize(_data.spr_chest, _data.spr_item, _data.rarity);
		m_btnCloseOne.onClick.RemoveAllListeners();
		m_btnCloseOne.interactable = false;
		chest.OnChestOpen.AddListener(() =>
		{
			m_btnCloseOne.interactable = true;
			m_btnCloseOne.onClick.RemoveAllListeners();
			m_btnCloseOne.onClick.AddListener(() =>
			{
				OnGachaFinished.Invoke();
			});
		});
	}
	public void GachaMulti(List<ChestData> _list)
	{
		m_goBackground.SetActive(true);
		m_goPanelTen.SetActive(true);

		m_prefChest.SetActive(false);

		open_count = 0;
		m_btnCloseTen.interactable = false;
		m_btnBulkOpen.interactable = true;
		chest_list.Clear();
		MonoBehaviourEx.DeleteObjects<GachaChest>(m_goPanelTen);

		foreach(ChestData data in _list)
		{
			GachaChest chest = PrefabManager.Instance.MakeScript<GachaChest>(m_prefChest, m_goTenChestRoot);
			chest.Initialize(data.spr_chest, data.spr_item, data.rarity);
			m_btnCloseOne.onClick.RemoveAllListeners();
			chest.OnChestOpen.AddListener(() =>
			{
				open_count += 1;
				if (_list.Count <= open_count)
				{
					m_btnCloseTen.interactable = true;
					m_btnBulkOpen.interactable = false;
				}
			});
			chest_list.Add(chest);
		}

		m_btnCloseTen.onClick.RemoveAllListeners();
		m_btnCloseTen.onClick.AddListener(() =>
		{
			Debug.Log("m_btnCloseTen.onClick");
			OnGachaFinished.Invoke();
		});

		m_btnBulkOpen.onClick.AddListener(() =>
		{
			foreach (GachaChest chest in chest_list)
			{
				chest.m_btnChest.onClick.Invoke();
			}
		});



	}


	public void GachaSingleSample()
	{
		m_goBackground.SetActive(true);
		m_goPanelOne.SetActive(true);

		m_prefChest.SetActive(false);

		GachaChest chest = PrefabManager.Instance.MakeScript<GachaChest>(m_prefChest, m_goPanelOne);

		chest.Initialize(m_sprAtlasIcons.GetSprite("chest_t_01"), m_sprAtlasWeapon.GetSprite("150_t"),1);
		m_btnCloseOne.onClick.RemoveAllListeners();
		m_btnCloseOne.interactable = false;
		chest.OnChestOpen.AddListener(() =>
		{
			m_btnCloseOne.interactable = true;
			m_btnCloseOne.onClick.RemoveAllListeners();
			m_btnCloseOne.onClick.AddListener(() =>
			{
				OnGachaFinished.Invoke();
			});
		});
	}

	public void GachaTenSample()
	{
		Debug.Log("ten");
		m_goBackground.SetActive(true);
		m_goPanelTen.SetActive(true);

		m_prefChest.SetActive(false);

		open_count = 0;
		m_btnCloseTen.interactable = false;
		m_btnBulkOpen.interactable = true;
		chest_list.Clear();
		MonoBehaviourEx.DeleteObjects<GachaChest>(m_goPanelTen);

		for ( int i = 0; i < 10; i++)
		{
			GachaChest chest = PrefabManager.Instance.MakeScript<GachaChest>(m_prefChest, m_goTenChestRoot);
			chest.Initialize(m_sprAtlasIcons.GetSprite("chest_t_01"), m_sprAtlasWeapon.GetSprite("150_t"),1);
			m_btnCloseOne.onClick.RemoveAllListeners();
			chest.OnChestOpen.AddListener(() =>
			{
				open_count += 1;
				Debug.Log(open_count);
				if(10 <= open_count)
				{
					m_btnCloseTen.interactable = true;
					m_btnBulkOpen.interactable = false;
				}
			});
			chest_list.Add(chest);
		}

		m_btnCloseTen.onClick.RemoveAllListeners();
		m_btnCloseTen.onClick.AddListener(() =>
		{
			Debug.Log("m_btnCloseTen.onClick");
			OnGachaFinished.Invoke();
		});

		m_btnBulkOpen.onClick.AddListener(() =>
		{
			foreach( GachaChest chest in chest_list)
			{
				chest.m_btnChest.onClick.Invoke();
			}
		});

	}



}
