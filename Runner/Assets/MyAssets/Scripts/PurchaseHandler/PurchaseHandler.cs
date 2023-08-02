using System;
using System.Collections.Generic;
using HyperCasual.Runner;
using MyAssets.Scripts.PurchaseHandler;
using TMPro;
using UnityEngine;

public static class PurchaseHandler
{
    public static Action<UpgradeTypes> UpdateTexts;
    public static Action<UpgradeTypes> UpdateCosts;
    public static Action<ValueTypes> UpdateValues;

    public static void PurchaseUpgradeClicked(UpgradeTypes type , ValueTypes valType)
    {
        float currency = SaveManager.Instance.Currency;
        float cost = SaveManager.Instance.GenericGet(type);
        if (currency >= cost)
        {   
            if (UpdateValues != null)
            {
                UpdateValues(valType);
                UpdateCosts(type);
            }
            if (UpdateTexts != null)
            {
                UpdateTexts(type);
            }
        }
    }

    public static void UpdateValue(ValueTypes type)
    {
        float currentValue = SaveManager.Instance.GenericGet(type);
        //INCREASE VALUE
        //currentValue += currentValue  * 70 / 100;
        int addition = 0;
        switch (type)
        {
            case ValueTypes.CLOUD:
                addition = 2;
                break;
            case ValueTypes.MONEY:
                addition = 40;
                break;
            case ValueTypes.FAUCET:
                addition = 2;
                break;
            case ValueTypes.CAPACITY:
                addition = 50;
                break;
            case ValueTypes.BULLETPOWER:
                Debug.Log("level: " + SaveManager.Instance.BulletLevel + " and the fire rate is : " + GunFire.Instance.Rate);
                float level = SaveManager.Instance.BulletLevel + 1;
                SaveManager.Instance.BulletLevel = level;
                Debug.Log("inside the if: level is " + level + " and level %: "+level%2);
                if (level % 2 == 0)
                {
                    float rate = GunFire.Instance.Rate-0.05f;
                    GunFire.Instance.Rate = rate;
                    Debug.Log("GUNRATEEEEE: " + (GunFire.Instance.Rate));
                }
                addition = 25;
                break;
        }
        currentValue += addition;
        
        SaveManager.Instance.GenericSet(type, currentValue);

        
        if (type == ValueTypes.CAPACITY)
        {
            Inventory.Instance.SetCapacitiesAfterPurchaseUpgrade();
        }

        // if (type == ValueTypes.BULLETPOWER)
        // {
        //     float level = SaveManager.Instance.BulletLevel;
        //     SaveManager.Instance.BulletLevel = level + 1;
        //     if (SaveManager.Instance.BulletLevel % 2 == 0)
        //     {
        //         GunFire.Instance.Rate -= 0.05f;
        //     }
        //     //We should update this thing 
        // }
    }
    
    public static void UpdateCost(UpgradeTypes type)
    {
        float currentCost = SaveManager.Instance.GenericGet(type);
        float currentCurrency = SaveManager.Instance.Currency;
        currentCurrency -= currentCost;
        SaveManager.Instance.Currency = (int)currentCurrency;
        Inventory.Instance.totalMoneyAmount = (int)currentCurrency;
        //INCREASE COST
        currentCost += currentCost * 20 / 100;
        SaveManager.Instance.GenericSet(type, currentCost);
        Debug.Log("Currency In event: " + SaveManager.Instance.Currency);


    }
    

    public static void UpdateText(ref TextMeshProUGUI tm1, ref TextMeshProUGUI tm2, UpgradeTypes type1, UpgradeTypes type2)
    {
        int firstValue = (int)SaveManager.Instance.GenericGet(type1);
        int secondValue = (int)SaveManager.Instance.GenericGet(type2);
        tm1.text = firstValue.ToString();
        tm2.text = secondValue.ToString();
        tm1.color = Color.white;
        tm2.color = Color.white;
    }
    
    public static Dictionary<UpgradeTypes, string> DictionaryUpgradeTypes = new Dictionary<UpgradeTypes, string>()
    {
        { UpgradeTypes.MONEY_UPGRADE, "MoneyValueUpgradeCost" },
        { UpgradeTypes.CLOUD_UPGRADE, "CloudRateUpgradeCost" },
        { UpgradeTypes.FAUCET_UPGRADE, "FaucetRateUpgradeCost" },
        { UpgradeTypes.BULLETPOWER_UPGRADE, "BulletPowerIncreaseCost" },
        { UpgradeTypes.CAPACITY_UPGRADE, "BucketCapacityUpgradeCost" }
    };
    
    public static Dictionary<ValueTypes, string> DictionaryValueTypes = new Dictionary<ValueTypes, string>()
    {
        { ValueTypes.MONEY, "MoneyValue" },
        { ValueTypes.CLOUD, "CloudRate" },
        { ValueTypes.FAUCET, "FaucetRate" },
        { ValueTypes.BULLETPOWER, "BulletPower" },
        { ValueTypes.CAPACITY, "BucketCapacity" }
    };
}




