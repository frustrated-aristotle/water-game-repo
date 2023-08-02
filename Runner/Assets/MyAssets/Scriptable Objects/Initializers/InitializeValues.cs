using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;

[CreateAssetMenu(fileName = "ValueInitializer", menuName = "GameManager/ValueInitializer", order = 1)]
public class InitializeValues : ScriptableObject
{
    [SerializeField] private float initialCurrency;
    //VALUES
    [SerializeField] private float initialMoneyValue;
    [SerializeField] private float initialMoneyValueUpgradeCost;
    
    [SerializeField] private float initialBucketCapacity;
    [SerializeField] private float initialBucketCapacityUpgradeCost;
    
    [SerializeField] private float initialBulletPower;
    [SerializeField] private float initialBulletPowerUpgradeCost;
    
    
    [SerializeField] private float initialFaucetRate;
    [SerializeField] private float initialFaucetRateUpgradeCost;

    [SerializeField] private float initialCloudRate;
    [SerializeField] private float initialCloudRateUpgradeCost;

    private List<Tuple<string, float>> keyAndValueForSaveManager = new List<Tuple<string, float>>();
    
    public void MainInitializer()
    {
        InitTuples();    
        InitValues(keyAndValueForSaveManager);
        SaveManager.Instance.Currency = (int)initialCurrency;
        SaveManager.Instance.BulletLevel = 0;
    }

    private void InitTuples()
    {
        
        Tuple<string, float> tupleBucketCap = new Tuple<string, float>("BucketCapacity", initialBucketCapacity);
        keyAndValueForSaveManager.Add(tupleBucketCap);
        
        Tuple<string, float> tupleBucketCapUpgrade = new Tuple<string, float>("BucketCapacityUpgradeCost", initialBucketCapacityUpgradeCost);
        keyAndValueForSaveManager.Add(tupleBucketCapUpgrade);
        
        Tuple<string, float> tupleCloud = new Tuple<string, float>("CloudRateUpgradeCost", initialCloudRateUpgradeCost);
        keyAndValueForSaveManager.Add(tupleCloud);
        
        Tuple<string, float> tupleCloudUpgrade = new Tuple<string, float>("CloudRate", initialCloudRate);
        keyAndValueForSaveManager.Add(tupleCloudUpgrade);
        
        Tuple<string, float> tupleBulletPowerUpgrade = new Tuple<string, float>("BulletPowerIncreaseCost", initialBulletPowerUpgradeCost);
        keyAndValueForSaveManager.Add(tupleBulletPowerUpgrade);
        
        Tuple<string, float> tupleBulletPower = new Tuple<string, float>("BulletPower", initialBulletPower);
        keyAndValueForSaveManager.Add(tupleBulletPower);
        
        Tuple<string, float> tupleFaucetRate = new Tuple<string, float>("FaucetRate", initialFaucetRate);
        keyAndValueForSaveManager.Add(tupleFaucetRate);
        
        Tuple<string, float> tupleFaucetRateUpgrade = new Tuple<string, float>("FaucetRateUpgradeCost", initialFaucetRateUpgradeCost);
        keyAndValueForSaveManager.Add(tupleFaucetRateUpgrade);
        
        Tuple<string, float> tupleMoneyValue = new Tuple<string, float>("MoneyValue", initialMoneyValue);
        keyAndValueForSaveManager.Add(tupleMoneyValue);
        
        Tuple<string, float> tupleMoneyValueUpgrade = new Tuple<string, float>("MoneyValueUpgradeCost" ,initialMoneyValueUpgradeCost);
        keyAndValueForSaveManager.Add(tupleMoneyValueUpgrade);
    }
    private void InitValues(List<Tuple<string, float>> tupleList)
    {
        foreach (Tuple<string,float> tuple in tupleList)
        {
            PlayerPrefs.SetFloat(tuple.Item1, tuple.Item2);
        }
    }
}
