using System.Collections.Generic;
using System.Linq;
using HyperCasual.Gameplay;
using HyperCasual.Runner;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    [SerializeField]private List<Cloud> clouds = new List<Cloud>();

    #region Bucket Capacity Increase Cost Stuff

    private int initialBucketCapacityIncreaseCost = 300;
    private float currentBucketCapacityIncreaseCost = 300;
    private float bucketCapacityIncreaseCostMultiplier = 1;
    
    public float BucketCapacityIncreaseCost 
    {
        get
        {
            UpdateRate(ref currentBucketCapacityIncreaseCost, ref initialBucketCapacityIncreaseCost, ref bucketCapacityIncreaseCostMultiplier);
            return currentBucketCapacityIncreaseCost;
        }
        set
        {
            currentBucketCapacityIncreaseCost = (int)value;
        }
        
    }

    public void OnBucketCapacityIncreasePurchased()
    {
        if (BucketCapacityIncreaseCost <= Inventory.Instance.TotalGold)
        {
            Inventory.Instance.TotalGold = Inventory.Instance.TotalGold - (int)BucketCapacityIncreaseCost;
            //U-FlowUpgradeIsPurchased();
            //We are making the cloud rate multiplier 1 at awake.
            bucketCapacityIncreaseCostMultiplier += 0.2f;
            BucketCapacityIncreaseCost = initialBucketCapacityIncreaseCost * bucketCapacityIncreaseCostMultiplier;
            SaveManager.Instance.BucketCapacityIncreaseCostMultiplier = bucketCapacityIncreaseCostMultiplier;
            Hud.Instance.UpdateUpgradeButtonText();
        }
        else
        {
            Hud.Instance.UpdateUpgradeButtonText(Color.red , false);
        }
    }
    #endregion

    #region Cloud Rate Increase Stuff
    
    private int initialCloudRateIncreaseCost = 300;
    private float currentCloudRateIncreaseCost = 300;
    private float cloudRateIncreaseCostMultiplier = 1f;

    public float CloudRateIncreaseCost 
    {
        get
        {
            UpdateRate(ref currentCloudRateIncreaseCost, ref initialCloudRateIncreaseCost,
                ref cloudRateIncreaseCostMultiplier);
            return currentCloudRateIncreaseCost;
        }
        set
        {
            currentCloudRateIncreaseCost = (int)value;
        }
        
    }
    #endregion
    
    #region Cloud Rates Related
    private int initialCloudRate = 5;
    private float currentCloudRate=5;
    private float cloudRateMultiplier = 1f;

    public float CloudRate
    {
        get
        {
            UpdateRate(ref currentCloudRate, ref initialCloudRate, ref cloudRateMultiplier);
            return currentCloudRate;
        }
        set
        {
            currentCloudRate = value;
        }
    }

    public void IncreaseCloudRate()
    {
        if (CloudRateIncreaseCost <= Inventory.Instance.TotalGold)
        {
            Inventory.Instance.TotalGold = Inventory.Instance.TotalGold - (int)CloudRateIncreaseCost;
            Inventory.Instance.IncreaseBucketCapacityFromStartingMenu();
            FlowUpgradeIsPurchased();
            //We are making the cloud rate multiplier 1 at awake.
            cloudRateMultiplier += 0.2f;
            CloudRate = initialCloudRate * cloudRateMultiplier;
            SaveManager.Instance.CloudRateMultiplier = cloudRateMultiplier;
            ModifyCloudsFlowRate();
        }
        else
        {
            Hud.Instance.UpdateUpgradeButtonText(Color.red , true);
        }
    }
    public void ModifyCloudsFlowRate()
    {
        FindClouds();
        foreach (Cloud cloud in clouds)
        {
            cloud.Rate = (int)CloudRate;
        }
    }
    #endregion

    #region Upgrade Functions
    
    private void UpdateRate(ref float rateToUpdate, ref int initialRate, ref float rateMultiplier)
    {
        rateToUpdate = initialRate * rateMultiplier;
    }

    private void FlowUpgradeIsPurchased()
    {
        //Increase Multiplier
        cloudRateIncreaseCostMultiplier += 0.2f;
        //Change the text of upgrade cost in ui.
        Hud.Instance.UpdateUpgradeButtonText();
        SaveManager.Instance.CloudRateCostMultiplier = cloudRateIncreaseCostMultiplier;
    }
    #endregion

    #region Unnecessary

    #region Singleton
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }

        SaveManager.Instance.CloudRateMultiplier = 1;
        SaveManager.Instance.CloudRateCostMultiplier = 1;
        SaveManager.Instance.BucketCapacityIncreaseCostMultiplier = 1;
        cloudRateMultiplier = SaveManager.Instance.CloudRateMultiplier;
        cloudRateIncreaseCostMultiplier = SaveManager.Instance.CloudRateCostMultiplier;
        bucketCapacityIncreaseCostMultiplier = SaveManager.Instance.BucketCapacityIncreaseCostMultiplier;
    }
    #endregion
    public static VariableManager Instance;
    private void FindClouds()
    {
        clouds = FindObjectsOfType<Cloud>().ToList();
    }
    private void OnEnable()
    {
        FindClouds();
        
    }
    #endregion

}
