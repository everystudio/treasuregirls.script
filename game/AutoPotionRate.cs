using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPotionRate : MonoBehaviour
{
	[SerializeField]
	private TMPro.TextMeshProUGUI m_txtRate;
	[SerializeField]
	private UnityEngine.UI.Slider slider;

	public void OnChangeVolume()
	{
		rate = slider.value;
		m_txtRate.text = string.Format("{0}%", (int)(slider.value * 100.0f));
	}

	public float rate;

	void OnEnable()
	{
		slider.value = DataManager.Instance.user_data.ReadFloat(Defines.KEY_AUTOPOTION_RATE);

		m_txtRate.text = string.Format("{0}%", (int)(slider.value * 100.0f));
	}

}
