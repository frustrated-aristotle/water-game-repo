using System;
using System.Collections.Generic;
using HyperCasual.Core;
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
                if (currentValue <= 17 )
                {
                    addition = 2;
                }
                else
                {
                    UIManager.Instance.flowRateUpgradeButton.gameObject.SetActive(false);
                }
                break;
            case ValueTypes.MONEY:
                if (currentValue <= 310 )
                {
                    addition = 40;
                }
                else
                {
                    GameoverScreen.Instance.DeactivateBulletUpgrade(GameoverScreen.Instance.incomeGO);
                }
                break;
            case ValueTypes.FAUCET:
                if (currentValue <= 17)
                {
                    addition = 2;
                }
                else
                {
                    UIManager.Instance.flowRateUpgradeButton.gameObject.SetActive(false);
                }
                break;
            case ValueTypes.CAPACITY:
                if (currentValue <= 900)
                {
                    addition = 100;
                }
                else
                {
                    UIManager.Instance.bucketCapacityUpgradeButton.gameObject.SetActive(false);
                }
                break;
            case ValueTypes.BULLETPOWER:
                float mainRate = GunFire.Instance.Rate;
                bool isIt = mainRate >= 0.3f; 
                if (isIt)
                {
                    Debug.Log("rate: " + GunFire.Instance.Rate + " and it is : " + isIt);
                    float rate = GunFire.Instance.Rate-0.05f;
                    GunFire.Instance.Rate = rate;
                    addition = 0;
                }
                else
                {
                    GameoverScreen.Instance.DeactivateBulletUpgrade(GameoverScreen.Instance.bulletGO);
                }
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




