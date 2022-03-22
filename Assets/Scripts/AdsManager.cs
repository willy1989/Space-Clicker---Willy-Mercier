using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : Singleton<AdsManager>, IUnityAdsListener
{
    private bool testMode = true;

    private const string gameId = "4671537";

    private const string interstitialAdPlacementID = "Interstitial_Android";
    private const string bannerAdPlacementID = "Banner_Android";
    private const string rewardedAdPlacementID = "Rewarded_Android";

    private const float interstitialAdCountDown = 10f;
    private float currentInterstitialAdCountDown = interstitialAdCountDown;
    private IEnumerator interstitialAdCoroutine;

    private void Awake()
    {
        SetInstance();

        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);

        Advertisement.Load(interstitialAdPlacementID);
        ResetInterstitialAdCountDown();
    }

    private void Start()
    {
        WaveManager.Instance.SpawnWaveAction += ShowInterstitialAd;
    }

    public void ShowInterstitialAd()
    {
        Debug.Log("ShowInterstitialAd");

        if (Advertisement.IsReady(interstitialAdPlacementID) && currentInterstitialAdCountDown <= 0f)
        {
            Advertisement.Show(interstitialAdPlacementID);

            Debug.Log("Actually showing add");
        }
    }

    private void ResetInterstitialAdCountDown()
    {
        if (interstitialAdCoroutine != null)
            StopCoroutine(interstitialAdCoroutine);

        currentInterstitialAdCountDown = interstitialAdCountDown;

        interstitialAdCoroutine = startInterstitialAdCountDown();

        StartCoroutine(interstitialAdCoroutine);
    }

    private IEnumerator startInterstitialAdCountDown()
    {
        while (currentInterstitialAdCountDown > 0f)
        {
            currentInterstitialAdCountDown -= Time.deltaTime;
            yield return null;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log(placementId + " ads are ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Add error message: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log(placementId + " ad started showing");
    }

}
