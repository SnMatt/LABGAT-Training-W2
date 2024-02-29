using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    public string AndroidGameID;
    public string IOSGameID;

    public bool IsTestingMode = true;

    private string _gameID;

    private void Awake()
    {
        InitializeAd();
    }

    private void InitializeAd()
    {
#if UNITY_IOS
        _gameID = IOSGameID;
#elif UNITY_ANDROID
        _gameID = AndroidGameID;
#elif UNITY_EDITOR
        _gameID = AndroidGameID;
#endif

        if(!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameID, IsTestingMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ad initialized");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ad Failed");
    }
}
