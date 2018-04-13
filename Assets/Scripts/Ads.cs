using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {

	BannerView bannerView;
	InterstitialAd interstitialAd;

	#if UNITY_IPHONE
		string appId = "";
		string adUnitId = "";
	#elif UNITY_ANDROID
		string appId = "";
		string adUnitId = "";
	#else
		string appId = "";
		string adUnitId = "";
	#endif

	// Use this for initialization
	void Start () {
		MobileAds.Initialize (appId);

		LoadAd ();
	}

	public void LoadAd() {
		interstitialAd = new InterstitialAd (adUnitId);
		AdRequest adRequest = new AdRequest.Builder ().Build ();

		interstitialAd.LoadAd (adRequest);
	}

	public void ShowAs() {
		if (interstitialAd.IsLoaded ()) {
			interstitialAd.Show ();
		}
	}

	public void OnAdClosed(object sender, EventArgs args) {
		interstitialAd.Destroy ();
		LoadAd ();
	}
}
