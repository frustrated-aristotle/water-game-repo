using UnityEngine;
using UnityEngine.SceneManagement;
public class MaxInit : MonoBehaviour
{
    public GameObject playerMoney;          //UI 3d text object
    private int availableMoney;
    // Start is called before the first frame update
    void Start()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            // AppLovin SDK is initialized, start loading ads
        //    LoadInterstitialAd();
          //  LoadRewardedAd();
        };

        MaxSdk.SetSdkKey("6l6DtzPTwOgQzn7T2-2C_6BJfrSmrnaKlH27LGFKVBMyGSNNk8hBTpagvfEKpLMk2sMqNEAugbC1GlqG_D0To7");
        MaxSdk.InitializeSdk();

        // Register rewarded ad callbacks
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoaded;
        //  MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplay;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayed;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClicked;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHidden;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedReward;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to load an interstitial ad
    private void LoadInterstitialAd()
    {
        MaxSdk.LoadInterstitial("8e9c916e73d0874f");
    }

    // Function to load a rewarded ad
    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd("c36470f1aed7dc0d");
    }

    // Function to show an interstitial ad
    public void ShowInterstitialAd()
    {
        if (MaxSdk.IsInterstitialReady("8e9c916e73d0874f"))
        {
            MaxSdk.ShowInterstitial("8e9c916e73d0874f");
        }
        else
        {
            Debug.LogWarning("Interstitial ad is not ready. Make sure to load it before showing.");
            LoadInterstitialAd(); // Load a new interstitial ad
        }
    }

    // Function to show a rewarded ad
    public void ShowRewardedAd()
    {
        if (MaxSdk.IsRewardedAdReady("c36470f1aed7dc0d"))
        {
            Debug.Log("Rewarded on");
            MaxSdk.ShowRewardedAd("c36470f1aed7dc0d");
        }
        else
        {
            Debug.LogWarning("Rewarded ad is not ready. Make sure to load it before showing.");
            LoadRewardedAd(); // Load a new rewarded ad
        }
    }

    // Rewarded ad loaded callback
    private void OnRewardedAdLoaded(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Rewarded ad loaded: " + adUnitId);
    }

    // Rewarded ad failed to display callback
    private void OnRewardedAdFailedToDisplay(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        Debug.Log("Rewarded ad failed to display: " + adUnitId + ", Error: " + errorInfo.Message);
    }

    // Rewarded ad displayed callback
    private void OnRewardedAdDisplayed(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Rewarded ad displayed: " + adUnitId);
    }

    // Rewarded ad clicked callback
    private void OnRewardedAdClicked(string adUnitId,MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Rewarded ad clicked: " + adUnitId);
    }

    // Rewarded ad hidden callback
    private void OnRewardedAdHidden(string adUnitId,MaxSdkBase.AdInfo adInfo)
    {
        Debug.Log("Rewarded ad hidden: " + adUnitId);
    }

    // Rewarded ad received reward callback
    private void OnRewardedAdReceivedReward(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        string rewardName = reward.Label;
        int rewardAmount = reward.Amount;

        Debug.Log("Rewarded ad received reward: " + adUnitId + ", Reward name: " + rewardName + ", Reward amount: " + rewardAmount);

        availableMoney += 200;
        PlayerPrefs.SetInt("PlayerMoney", availableMoney);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // TODO: Implement the logic to reward the player based on the received reward
    }
}