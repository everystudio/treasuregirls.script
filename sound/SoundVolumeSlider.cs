using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundVolumeSlider : MonoBehaviour {


	[SerializeField]
	private UnityEngine.Audio.AudioMixer mixer;
	[SerializeField]
	private UnityEngine.UI.Slider slider;

	public string group_name;
	public UnityEvent OnChangeEvent = new UnityEvent();

	public string key_name
	{
		get
		{
			string key = "";
			if (group_name == "SE")
			{
				key = Defines.KEY_SOUNDVOLUME_SE;
			}
			else if (group_name == "BGM")
			{
				key = Defines.KEY_SOUNDVOLUME_BGM;
			}
			return key;
		}
	}

	public void OnChangeVolume()
	{
		rate = slider.value;
		SetVolume(rate);
		OnChangeEvent.Invoke();
	}

	public void SetVolume(float _fRate)
	{
		mixer.SetFloat(group_name, Mathf.Lerp(Defines.SOUND_VOLME_MIN, Defines.SOUND_VOLUME_MAX, _fRate));
	}
	public void SetSlider(float _fRate)
	{
		slider.value = _fRate;
	}

	public float rate;
	
	void OnEnable()
	{
		Debug.Log(slider.value);
		slider.value = DataManager.Instance.user_data.ReadFloat(key_name);
	}
}
