using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BannerFloor : MonoBehaviour
{
	public TextMeshProUGUI m_txtFloorNo;
	public TextMeshProUGUI m_txtStatus;

	public Button m_btn;

	public int floor_id;

	public void Initialize( DataFloorParam _data , MasterFloorParam _master)
	{
		if (_data != null)
		{
			floor_id = _data.floor_id;
		}
		else
		{
			floor_id = 0;
		}
		m_txtFloorNo.text = string.Format("{0}F", _master.floor_no);
		if(_data != null && _data.status == 1)
		{
			m_txtStatus.text = "挑戦中";
		}
		else if(_data != null && _data.status == 2)
		{
			m_txtStatus.text = "<color=#0FF>クリア</color>";
		}
		else
		{
			m_txtStatus.text = "<color=#F00>未開放</color>";
			m_btn.gameObject.SetActive(false);
		}

	}
}
