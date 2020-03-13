using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;



public class IconCharaList : MonoBehaviour
{
	[SerializeField]
	private Button m_btn;

	public Image m_imgIcon;
	public TextMeshProUGUI m_txtStatus;
	public Animator m_animator;
	public UnityEventInt OnClickIcon = new UnityEventInt();

	private MasterCharaParam m_masterCharaParam;
	private DataCharaParam m_dataCharaParam;

	public void SetData( DataCharaParam _data)
	{
		if( _data.chara_id == m_masterCharaParam.chara_id)
		{
			m_dataCharaParam = _data;
		}
	}

	[SerializeField]
	private SpriteAtlas m_spriteAtlas;

	public void Select(int _iSelectCharaId)
	{
		bool bFlag;
		if(m_dataCharaParam != null)
		{
			bFlag = m_dataCharaParam.chara_id == _iSelectCharaId;
			m_txtStatus.text = m_dataCharaParam.IsStatus(DataChara.STATUS.USING) ? "使用中" : "";
		}
		else
		{
			bFlag = m_masterCharaParam.chara_id == _iSelectCharaId;
			m_txtStatus.text = "";
		}
		m_animator.SetBool("select", bFlag);
	}

	public void Initialize( MasterCharaParam _master , DataCharaParam _data)
	{
		m_imgIcon.sprite = m_spriteAtlas.GetSprite(_master.GetIconName());
		// 塗る
		//Debug.Log(m_spriteAtlas.GetSprite("kuso"));
		m_masterCharaParam = _master;
		m_dataCharaParam = _data;

		m_btn.onClick.RemoveAllListeners();
		m_btn.onClick.AddListener(() =>
		{
			OnClickIcon.Invoke(m_masterCharaParam.chara_id);
		});
	}
}
