using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class IconStage : MonoBehaviour
{
	public Button m_btn;
	public TextMeshProUGUI m_txtStage;
	public TextMeshProUGUI m_txtStatus;
	public Image m_imgThumb;

	public SpriteAtlas m_spriteAtlas;

	public void Initialize(DataStageParam _data , MasterStageParam _master)
	{
		m_txtStage.text = string.Format("Stage{0}", _master.stage_id);

		if(_data.status == 1)
		{
			m_txtStatus.text = "<color=red>挑戦中</color>";
		}
		else if(_data.status == 2)
		{
			m_txtStatus.text = "<color=#0FF>クリア</color>";
		}

		m_imgThumb.sprite = m_spriteAtlas.GetSprite(_master.thumb_name);
	}

}
