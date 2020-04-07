using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class PanelResult : MonoBehaviour
{
	public TextMeshProUGUI m_txtClearMessage;

	public GameObject m_prefPrizeIcon;
	public GameObject m_goPrizeRoot;

	public Button m_btnCamp;
	public Button m_btnRetry;
	public Button m_btnRetry10;
	public Button m_btnNext;

	public SpriteAtlas m_spriteAtlas;

	public void Initialize( int _iFloorId )
	{
		m_txtClearMessage.text = string.Format("{0}F Clear!!", _iFloorId);

		m_prefPrizeIcon.SetActive(false);

		MonoBehaviourEx.DeleteObjects<IconResultPrize>(m_goPrizeRoot);
		foreach( DataItemParam data in DataManager.Instance.dataGetItem.list)
		{
			IconResultPrize script = PrefabManager.Instance.MakeScript<IconResultPrize>(m_prefPrizeIcon, m_goPrizeRoot);
			script.Initialize(data);
		}
	}

}
