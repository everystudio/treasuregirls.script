using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class RewardAd : Singleton<RewardAd> {
	public RewardedAd rewardBasedVideo;

	public bool m_bLoaded;
	public bool m_bReward;
	public bool ad_load_error;

	public enum STATUS
	{
		NONE		= 0,
		LOADING		,
		STANDBY		,
		PLAYING		,
		MAX			,
	}
	public STATUS m_eRewardAdStatus;

    public void Start()
	{
		m_bLoaded = false;
		ad_load_error = false;

		m_eRewardAdStatus = STATUS.NONE;


#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-5869235725006697/2305991456";
#else
            string adUnitId = "unexpected_platform";
#endif
		// Get singleton reward based video ad reference.
		this.rewardBasedVideo = new RewardedAd(adUnitId);

		// Called when an ad request has successfully loaded.
		rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		// Called when an ad request failed to load.
		rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		// Called when an ad is shown.
		rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		// Called when the ad starts to play.
		//rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
		// Called when the user should be rewarded for watching a video.
		rewardBasedVideo.OnUserEarnedReward += HandleUserEarnedReward;
		// Called when the ad is closed.
		rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		// Called when the ad click caused the user to leave the application.
		//rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

		this.RequestRewardBasedVideo();
	}
	/*
	private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
	{
		//throw new NotImplementedException();
	}*/

	private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
	{
		//throw new NotImplementedException();
		//this.RequestRewardBasedVideo();

		// ムービーを閉じたら次のやつをロードする
		RequestRewardBasedVideo();
	}

	private void HandleUserEarnedReward(object sender, Reward e)
	{
		//throw new NotImplementedException();
		m_bReward = true;
	}
	/*
	private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
	{
		//throw new NotImplementedException();
		// 広告の再生開始
	}
	*/

	private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
	{
		m_bLoaded = false;
		//throw new NotImplementedException();
		// 広告が開かれた

		m_eRewardAdStatus = STATUS.PLAYING;
	}

	private void HandleRewardBasedVideoFailedToLoad(object sender, AdErrorEventArgs args)
	{
		//throw new NotImplementedException();
		ad_load_error = true;
	}

	private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
	{
		// 広告の読み込み開始
		//throw new NotImplementedException();

		m_eRewardAdStatus = STATUS.STANDBY;
		m_bLoaded = true;
	}



	public void RequestRewardBasedVideo()
	{
		if (ad_load_error)
		{
			Debug.LogError("ad load error");
			return;
		}

		m_eRewardAdStatus = STATUS.LOADING;

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder()
			.AddTestDevice("B58A62380C00BF9DC7BA75C756B5F550")
			.AddTestDevice("30ec665ef7c68238905003e951174579")
			.Build();
		// Load the rewarded video ad with the request.
		this.rewardBasedVideo.LoadAd(request);
	}
}
