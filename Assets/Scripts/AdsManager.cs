using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    public string GameID
    {
        get
        {
#if UNITY_ANDROID
            return "4302981";
#elif UNITY_IOS
            return "4302980";
#endif
        }
    }

    public const string sampleRewarded = "sampleRewarded";
    public const string sampleInterstitial = "sampleInterstitial";
    public const string sampleBanner = "sampleBanner";
    public void Awake()
    {
        Advertisement.Initialize(GameID, true);
    }
    void Start()
    {
        Advertisement.AddListener(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInterstitialAd()
    {
        if(Advertisement.IsReady(sampleInterstitial))
        {
            Advertisement.Show(sampleInterstitial);
        }

        else
        {
            Debug.Log("No ads: " + sampleInterstitial);
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(sampleRewarded))
        {
            Advertisement.Show(sampleRewarded);
        }

        else
        {
            Debug.Log("No ads: " + sampleRewarded);
        }
    }

    private IEnumerator ShowBannerRoutine()
    {
        while(!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.CENTER);
        Advertisement.Banner.Show(sampleBanner);
    }

    public void ShowBanner()
    {
        StartCoroutine(ShowBannerRoutine());
    }

    public void HideBanner()
    {
        if(Advertisement.Banner.isLoaded)
            Advertisement.Banner.Hide();
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Loading done: " + placementId);
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Ad error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad shown: " + placementId);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Debug.Log("Watched ad");
        if(placementId == sampleRewarded)
        {
            PlayerData.gold += 100;
        }
    }
}
