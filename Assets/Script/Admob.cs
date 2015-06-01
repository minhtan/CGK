using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class Admob : MonoBehaviour {

    private BannerView bannerView;

    void Start() {
        RequestBanner();
        bannerView.Hide();
    }

    private void RequestBanner()
    {
        #if UNITY_EDITOR
        string adUnitId = "ca-app-pub-1381738027977117/9609598583";
        #elif UNITY_ANDROID
                    string adUnitId = "ca-app-pub-1381738027977117/9609598583";
        #elif UNITY_IPHONE
                    string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
        #else
                    string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        // Register for ad events.
        bannerView.AdLoaded += HandleAdLoaded;
        bannerView.AdFailedToLoad += HandleAdFailedToLoad;
        bannerView.AdOpened += HandleAdOpened;
        bannerView.AdClosing += HandleAdClosing;
        bannerView.AdClosed += HandleAdClosed;
        bannerView.AdLeftApplication += HandleAdLeftApplication;
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());
    }

    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddTestDevice("3E7C4426E7E3EA61")
                .Build();
    }

    public void showAds(){
        bannerView.Show();
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        print("HandleAdLoaded event received.");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        print("HandleAdOpened event received");
    }

    void HandleAdClosing(object sender, EventArgs args)
    {
        print("HandleAdClosing event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        print("HandleAdLeftApplication event received");
    }

    #endregion
}
