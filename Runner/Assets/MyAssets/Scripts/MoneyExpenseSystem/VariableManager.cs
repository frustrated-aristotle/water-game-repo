using System.Collections.Generic;
using System.Linq;
using HyperCasual.Gameplay;
using HyperCasual.Runner;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    [SerializeField]private List<Cloud> clouds = new List<Cloud>();

    //Fields that will be reached from outside:
    public int pourAmount = 50;

    #region Money Stuff
    
    private int initialMoneyAmount = 70;
    private float currentMoneyAmount=70;
    private float moneyAmountMultiplier = 1f;

    public float MoneyAmount
    {
        get
        {
            //UpdateRate(ref currentMoneyAmount, ref initialMoneyAmount, ref moneyAmountMultiplier);
            return 3; //SaveManager.Instance.MoneyAmount;
        }
        set
        {
            //SaveManager.Instance.MoneyAmount = value;
        }
    }
    #endregion
    
    #region  Income Increase Cost Stuff
   [SerializeField] protected int initialIncomeIncreaseCost = 300;
    private float currentIncomeIncreaseCost = 300;
    private float incomeIncreaseCostMultiplier = 1;
    
    public float IncomeIncreaseCost 
    {
        get
        {
            UpdateRate(ref currentIncomeIncreaseCost, ref initialIncomeIncreaseCost, ref incomeIncreaseCostMultiplier);
            return currentIncomeIncreaseCost;
        }
        set
        {
            currentIncomeIncreaseCost = (int)value;
        }
    }
    public void OnIncomeIncreasePurchased()
    {
        if (IncomeIncreaseCost <= Inventory.Instance.TotalGold)
        {
            Debug.Log("Money Amount Before : " + MoneyAmount);
            Inventory.Instance.TotalGold = Inventory.Instance.TotalGold - (int)IncomeIncreaseCost;
            incomeIncreaseCostMultiplier += 0.2f;
            IncomeIncreaseCost = initialIncomeIncreaseCost * incomeIncreaseCostMultiplier;
            //SaveManager.Instance.IncomeIncreaseCostMultiplier = incomeIncreaseCostMultiplier;
            moneyAmountMultiplier += 0.2f;
            MoneyAmount += MoneyAmount * 30 / 100;
            //SaveManager.Instance.MoneyAmount = MoneyAmount;
            Debug.Log("Money Amount After : " + MoneyAmount);
            UpdateCollectibles();
            GameoverScreen.Instance.UpdateGameEndUpgradeButtons();
        }
        else
        {
            GameoverScreen.Instance.UpdateGameEndUpgradeButtons(Color.red , false);
        }
    }

    private void UpdateCollectibles()
    {
        List<Collectable> collectibles = new List<Collectable>();
        collectibles = GameObject.FindObjectsOfType<Collectable>().ToList();
        foreach (Collectable collectible in collectibles )
        {
            collectible.ChangeCount();
        }
    }
    #endregion
    
    #region Bullet Power Increase Cost Stuff

    public int initialBulletPowerIncreaseCost;
    private float currentBulletPowerIncreaseCost;
    private float bulletPowerIncreaseCostMultiplier = 1;
    
    public float BulletPowerIncreaseCost 
    {
        get
        {
            UpdateRate(ref currentBulletPowerIncreaseCost, ref initialBulletPowerIncreaseCost, ref bulletPowerIncreaseCostMultiplier);
            return currentBulletPowerIncreaseCost;
        }
        set
        {
            currentBulletPowerIncreaseCost = (int)value;
        }
        
    }

    public void OnBulletPowerIncreasePurchased()
    {
        if (BulletPowerIncreaseCost <= SaveManager.Instance.Currency)
        {
            UpgradeBullet();
            //Inventory.Instance.TotalGold = Inventory.Instance.TotalGold - (int)BulletPowerIncreaseCost;
            GunFire.Instance.Rate -= 0.05f; 
            //GameManager.Instance.InitBulletPower();
            //U-FlowUpgradeIsPurchased();
            //We are making the cloud rate multiplier 1 at awake.
            //PurchaseHandler.PurchaseUpgrade("BulletPowerIncreaseCost");
            GameoverScreen.Instance.UpdateGameEndUpgradeButtons();
        }
        else
        {
             GameoverScreen.Instance.UpdateGameEndUpgradeButtons(Color.red , false);
        }
    }

    private void UpgradeBullet()
    {
        int currentBulletPower = (int)SaveManager.Instance.BulletPower;
        int newBulletPower = currentBulletPower + (int)(currentBulletPower * 60 / 100f);
        SaveManager.Instance.BulletPower = newBulletPower;
    }

    #endregion
    
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
            
            Hud.Instance.UpdateUpgradeButtonText();
            Inventory.Instance.IncreaseBucketCapacityFromStartingMenu();
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
            // UpdateRate(ref currentCloudRateIncreaseCost, ref initialCloudRateIncreaseCost,
            //     ref cloudRateIncreaseCostMultiplier);
            return 3; //SaveManager.Instance.CloudUpradeCost;
        }
        set
        {
            currentCloudRateIncreaseCost = (int)value;
        }
        
    }
    #endregion
    
    #region Cloud Rates Related
    private int initialCloudRate = 15;
    private float currentCloudRate=15;
    //private float cloudRateMultiplier = 1f;

    public float CloudRate
    {
        get
        {
            float val = (SaveManager.Instance.CloudRate > currentCloudRate)
                ? SaveManager.Instance.CloudRate
                : currentCloudRate; 
            //UpdateRate(ref currentCloudRate, ref initialCloudRate, ref cloudRateMultiplier);
            return val;
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
            FlowUpgradeIsPurchased();
            CloudRate += CloudRate * 70 / 100;
            SaveManager.Instance.CloudRate = CloudRate;
            
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
    
    /// <summary>
    /// This method will update the given value by
    /// multiplying the initial rate and its multiplier.
    /// </summary>
    /// <param name="rateToUpdate"></param>
    /// <param name="initialRate"></param>
    /// <param name="rateMultiplier"></param>
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
        //SaveManager.Instance.CloudRateCostMultiplier = cloudRateIncreaseCostMultiplier;
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

        //initialBulletPowerIncreaseCost = SaveManager.Instance.InitialBulletPowerIncreaseCost;
        //SaveManager.Instance.CloudRateMultiplier = 1;
        //SaveManager.Instance.CloudRateCostMultiplier = 1;
        //SaveManager.Instance.BucketCapacityIncreaseCostMultiplier = 1;
        //SaveManager.Instance.BulletPowerIncreaseCostMultiplier = 1;
        //SaveManager.Instance.IncomeIncreaseCostMultiplier = 1;
        //SaveManager.Instance.MoneyAmountMultiplier = 1;
        //cloudRateMultiplier = SaveManager.Instance.CloudRateMultiplier;
        //cloudRateIncreaseCostMultiplier = SaveManager.Instance.CloudRateCostMultiplier;
        //bucketCapacityIncreaseCostMultiplier = SaveManager.Instance.BucketCapacityIncreaseCostMultiplier;
        //bulletPowerIncreaseCostMultiplier = SaveManager.Instance.BulletPowerIncreaseCostMultiplier;
        //incomeIncreaseCostMultiplier = SaveManager.Instance.IncomeIncreaseCostMultiplier;
        //moneyAmountMultiplier = SaveManager.Instance.MoneyAmountMultiplier;
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
