using System;
using GoogleMobileAds.Api;
using UnityEngine;

public class Admob : MonoSingleton<Admob>
{
    private readonly string _adUnitIdAndroid = "ca-app-pub-5714181718235393/3449502210";

    private string _adUnitId;

    private InterstitialAd interstitialAd;

    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //초기화 완료
        });
        _adUnitId = _adUnitIdAndroid;

        LoadInterstitialAd();
    }

    public void LoadInterstitialAd() //광고 로드
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        var adRequest = new AdRequest.Builder()
                .AddKeyword("unity-admob-sample")
                .Build();

        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });
        RegisterEventHandlers(interstitialAd); //이벤트 등록
    }

    public void ShowAd() //광고 보기
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            LoadInterstitialAd(); //광고 재로드

            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad) //광고 이벤트
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {

            //보상 주기

            Debug.Log(string.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

	//수동으로 광고 재로드를 하고 싶다면 추가하기 (선언 필요)
    private void RegisterReloadHandler(InterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed += (null);
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            LoadInterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            LoadInterstitialAd();
        };
    }
}
