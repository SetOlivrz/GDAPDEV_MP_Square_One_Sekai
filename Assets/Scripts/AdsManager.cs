using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{

    public string GameID
    {
        get
        {
#if UNITY_ANDROID
            return "4302981";
#elif UNITY_IOS
                return "4302980";
#else
                // Provide a default value here
            return "4302981";
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
        //Advertisement.AddListener(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInterstitialAd()
    {

        Advertisement.Load(sampleInterstitial, this);
        Advertisement.Show(sampleInterstitial, this);
    }

    public void ShowRewardedAd()
    {
        Advertisement.Load(sampleRewarded, this);
        Advertisement.Show(sampleRewarded, this);
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

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new System.NotImplementedException();
    }
}
