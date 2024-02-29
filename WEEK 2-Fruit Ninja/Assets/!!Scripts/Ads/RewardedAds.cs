using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public string AndroidAdUnitID;
    public string IOSAdUnitID;

    private string _adUnitID;

    private void Awake()
    {
#if UNITY_IOS
        _adUnitID = IOSAdUnitID;
#elif UNITY_ANDROID
        _adUnitID = AndroidAdUnitID;
#endif
    }

    public void LoadAd()
    {
        Debug.Log("Loading rewarded Ad");
        Advertisement.Load(_adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Reward ad loaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Reward ad load FAILED");
    }


    public void ShowAd()
    {
        Debug.Log("Showing Ad");
        Advertisement.Show(_adUnitID, this);

        LoadAd();
    }
    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Rewarded clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Rewarded show complete");
        if(placementId == _adUnitID && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            GameManager.Instance.ContinueWithAd();
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Rewarded FAILED");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Rewarded show start");
    }
}
