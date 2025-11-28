using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance { get; private set; }

    [SerializeField]
    private BannerAds bannerController;
    [SerializeField]
    private InterstitialAds interstitialController;
    [SerializeField]
    private RewardedAds rewardedController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowRewardedAd()
    {
        rewardedController.ShowRewardedAd();
    }
    
    public void SubscribeToRewardedAdResult(Action<UnityAdsShowCompletionState> callback) {
        rewardedController.OnAddCompleted += callback;
    }

    private IEnumerator DisplayBannerAd()
    {
        while (true)
        {
            bannerController.LoadBannerAd();
            yield return new WaitForSeconds(5f);
            bannerController.ShowBannerAd();
            yield return new WaitForSeconds(10f);
            bannerController.HideBannerAd();
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator DisplayInterstitialAd()
    {
        yield return new WaitForSeconds(20f);
        interstitialController.ShowInterstitialAd();
    }
}
