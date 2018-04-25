using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {

    static InterstitialAd interstitial;

	// Use this for initialization
	static void Start () {
        MobileAds.Initialize(Constants.AdmobAppId);
	}
	
    public static void RequestInterstitial(string InterstitialAdId) {
        interstitial = new InterstitialAd(InterstitialAdId);
        interstitial.OnAdLoaded += OnLoaded;
        interstitial.OnAdFailedToLoad += OnLoadFailed;
        interstitial.OnAdOpening += OnOpening;

        AdRequest request = new AdRequest.Builder().Build();

        interstitial.LoadAd(request);
    }

    public static bool IsInterstitialLoaded() {
        return interstitial != null && interstitial.IsLoaded();
    }

    public static void ShowIntertitial() {
        if (interstitial != null && interstitial.IsLoaded()) {
            interstitial.Show();
        }
    }

    public static void DestroyInterstitial() {
        if (interstitial != null) {
            interstitial.Destroy();
        }
    }

    static void OnLoaded(object sender, EventArgs e) {
        print("Ad loaded");
    }

    static void OnLoadFailed(object sender, AdFailedToLoadEventArgs e) {
        print("Ad load failed: " + e.Message);
    }

    static void OnOpening(object sender, EventArgs e) {
        print("Ad opening");
    }
}
