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
		float aspect_rate = (float)Screen.width / (float)Screen.height;
		float check_aspect = 13.0f / 9.0f;
		Debug.Log(check_aspect);
		Debug.Log(aspect_rate);

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
		/*
		// iPadはバナー表示させない！
		if(SystemInfo.deviceModel.Contains("iPad"))
		{
			return;
		}
		*/

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


		float aspect_rate = (float)Screen.width / (float)Screen.height;
		float check_aspect = 13.0f / 9.0f;

		if(aspect_rate < check_aspect)
		{
			view = new BannerView(strUnitId, AdSize.Banner, AdPosition.Top);
		}
		else
		{
			view = new BannerView(strUnitId, AdSize.SmartBanner, AdPosition.TopRight);
		}

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice("30ec665ef7c68238905003e951174579")
			.Build();

			//.AddTestDevice("B58A62380C00BF9DC7BA75C756B5F550")

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
