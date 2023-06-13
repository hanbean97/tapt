using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : PersistentSingleton<AdManager>
{

    private void Start()
    {
        //ad, sdk 초기화
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => { });

        //광고요청
        RequestBanner();
        RequestInterstitialAd();
        RequestRewardAd();
    }

    BannerView _bannerView;
    InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;

    //배너
    #region Banner
    void RequestBanner()
    {
        string _adUnitId;
#if UNITY_ANDROID
        _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        _adUnitId = "unused";
#endif

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBanner();
        }

        // Create a 320x50 banner at top of the screen
        //_bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Top);
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        _bannerView = new BannerView(_adUnitId, adaptiveSize, AdPosition.Top);

        var adRequest = new AdRequest();

        _bannerView.LoadAd(adRequest);
    }

    public void ShowBanner()
    {
        if (_bannerView != null)
            _bannerView.Show();
    }

    public void HideBanner()
    {
        if (_bannerView != null)
            _bannerView.Hide();
    }

    public void DestroyBanner()
    {
        if (_bannerView != null)
            _bannerView.Destroy();
    }
    #endregion

    //전면광고
    #region InterstitialAd
    void RequestInterstitialAd()
    {
        string _adUnitId;
#if UNITY_ANDROID
        _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        _adUnitId = "unused";
#endif

        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();

        InterstitialAd.Load(_adUnitId, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  return;
              }
              interstitialAd = ad;

              interstitialAd.OnAdFullScreenContentOpened += HandleOnAdOpned;
              interstitialAd.OnAdFullScreenContentClosed += HandleOnAdClosed;

          });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    public void HandleOnAdOpned()
    {

    }

    public void HandleOnAdClosed()
    {
        Debug.Log("RequestInterstitialAd======");
        RequestInterstitialAd();
    }
    #endregion

    //리워드
    #region ReawardAD
    void RequestRewardAd()
    {
        string _adUnitId;
#if UNITY_ANDROID
        _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        _adUnitId = "unused";
#endif

        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    return;
                }
                rewardedAd = ad;

                rewardedAd.OnAdFullScreenContentClosed += HandleOnRewardAdClosed;
            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));

                Debug.Log("보상 아이템 아이템 ammo 10개 지급");
               // PlayerStats.Instance.AddAmmor(10);
            });
        }
    }

    public void HandleOnRewardAdClosed()
    {
        RequestRewardAd();
    }
    #endregion
}
