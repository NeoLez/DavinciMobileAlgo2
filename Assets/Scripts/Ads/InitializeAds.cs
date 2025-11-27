using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
{
    private const string AndroidID = "5992997";
    [SerializeField]
    private bool testMode = true;

    private void Awake()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(AndroidID, testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Initialization sucess.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Initialization failure.");
    }
}
