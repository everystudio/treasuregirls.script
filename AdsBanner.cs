﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdsBanner : MonoBehaviour {

	public bool m_bAutoShow = true;
	[SerializeField]
	string adUnitIdAndroid = "";
	[SerializeField]
	string adUnitIdIOS = "";
	BannerView view = null;        // 縦画面

	public AdPosition banner_position;

	void Start()
	{
		if (adUnitIdAndroid.Equals(""))
		{
			Debug.LogError("no set AdsBanner.adUnitIdAndroid");
		}
		if (adUnitIdIOS.Equals(""))
		{
			Debug.LogError("no set AdsBanner.adUnitIdIOS");
		}

		if (m_bAutoShow)
		{
			Show();
		}
	}

	public void Show()
	{
		// iPadはバナー表示させない！
		if(SystemInfo.deviceModel.Contains("iPad"))
		{
			return;
		}

		if(view != null)
		{
			view.Show();
			return;
		}
		string strUnitId = "";
#if UNITY_ANDROID
		strUnitId = adUnitIdAndroid;
#elif UNITY_IOS
		strUnitId = adUnitIdIOS;
#endif
		view = new BannerView(strUnitId, AdSize.SmartBanner, banner_position);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
			//.AddTestDevice("B58A62380C00BF9DC7BA75C756B5F550")
			.AddTestDevice("30ec665ef7c68238905003e951174579")
			.Build();
		// Load the banner with the request.
		view.LoadAd(request);
	}
	public void Hide()
	{
		if( view != null)
		{
			view.Hide();
		}
	}
}
