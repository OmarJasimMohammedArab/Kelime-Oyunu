using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    //public string appId;
    //public string adBannerId;
    //public string adIntersitialId;
    //public AdPosition bannerPosition;
    //public bool testDevice = false;

    //private BannerView _bannerView;
    //private InterstitialAd _Interstitial;

    //public static AdManager Instance;
    //public static Action OnInterstitialAdsClosed;

    //public void Awake()
    //{
    //    if (Instance == false)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(this);
    //    }
    //    else
    //    {
    //        Destroy(this);
    //    }
    //}

    //void Start()
    //{
    //    MobileAds.Initialize(appId);
    //    this.CreateBanner(CreateRequest());
    //    this.CreateInterstitialAd(CreateRequest());

    //    this._Interstitial.OnAdClosed += InterstitialAdsClosed;


    //}

    //private void OnDisable()
    //{
    //    this._Interstitial.OnAdClosed -= InterstitialAdsClosed;
    //}

    //private void InterstitialAdsClosed(object sender, EventArgs e)
    //{
    //    if (OnInterstitialAdsClosed != null)
    //        OnInterstitialAdsClosed();
    //}

    //private AdRequest CreateRequest()
    //{
    //    AdRequest request;
    //    if (testDevice)
    //        request = new AdRequest.Builder().AddTestDevice(SystemInfo.deviceUniqueIdentifier).Build();
    //    else
    //        request = new AdRequest.Builder().Build();

    //    return request;
    //}

    //public void CreateInterstitialAd(AdRequest request)
    //{
    //    this._Interstitial = new InterstitialAd(adIntersitialId);
    //    this._Interstitial.LoadAd(request);
    //}

    //public void ShowInterstitialAd()
    //{
    //    if (this._Interstitial.IsLoaded())
    //    {
    //        this._Interstitial.Show();
    //    }
    //    this._Interstitial.LoadAd(CreateRequest());
    //}

    //public void CreateBanner(AdRequest request)
    //{
    //    this._bannerView = new BannerView(adBannerId, AdSize.SmartBanner, bannerPosition);
    //    this._bannerView.LoadAd(request);
    //    HideBanner();
    //}

    //public void HideBanner()
    //{
    //    _bannerView.Hide();
    //}

    //public void ShowBanner()
    //{
    //    _bannerView.Show();
    //}
}
