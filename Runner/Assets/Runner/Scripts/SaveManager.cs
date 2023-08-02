using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using MyAssets.Scripts.PurchaseHandler;
using UnityEngine;
using AudioSettings = HyperCasual.Core.AudioSettings;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A simple class used to save a load values
    /// using PlayerPrefs.
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the SaveManager.
        /// </summary>
        public static SaveManager Instance => s_Instance;
        static SaveManager s_Instance;
        const string normalSpeed = "NormalSpeed";
        const string horizontalSpeed = "HorizontalSpeed";
        
        const string secondBlendShapeValue = "SecondBlendShapeValue";
        const string firstBlendShapeValue = "FirstBlendShapeValue";

        const string k_LevelProgress = "LevelProgress";
        const string k_Currency = "Currency";
        const string k_Xp = "Xp";
        const string k_AudioSettings = "AudioSettings";
        const string k_QualityLevel = "QualityLevel";
        const string capacity = "Capacity";

        
        void Awake()
        {
            s_Instance = this;
        }

        #region Initial Values


        public float NormalSpeed
        {
            get => PlayerPrefs.GetFloat(normalSpeed);
            set => PlayerPrefs.SetFloat(normalSpeed, value);
        }

        public float HorizontalSpeed
        {
            get => PlayerPrefs.GetFloat(horizontalSpeed);
            set => PlayerPrefs.SetFloat(horizontalSpeed, value);
        }
        #endregion
        /// <summary>
        /// Save and load level progress as an integer
        /// </summary>
        public int LevelProgress 
        { 
            get => PlayerPrefs.GetInt(k_LevelProgress); 
            set => PlayerPrefs.SetInt(k_LevelProgress, value);
        }

        /// <summary>
        /// Save and load currency as an integer
        /// </summary>
        public int Currency 
        { 
            get => PlayerPrefs.GetInt(k_Currency); 
            set => PlayerPrefs.SetInt(k_Currency, value);
        }
        public float XP
        {
            get => PlayerPrefs.GetFloat(k_Xp); 
            set => PlayerPrefs.SetFloat(k_Xp, value);
        }
        #region BLEND SHAPES
        public float FirstBlendShapeValue
        {
            get => PlayerPrefs.GetFloat(firstBlendShapeValue);
            set => PlayerPrefs.SetFloat(firstBlendShapeValue, value);
        }

        public float SecondBlendShapeValue
        {
            get => PlayerPrefs.GetFloat(secondBlendShapeValue);
            set => PlayerPrefs.SetFloat(secondBlendShapeValue, value);
        }
        #endregion

        #region CLOUDS
        private const string cloudRateUpgradeCost = "CloudRateUpgradeCost";
        private const string cloudRate = "CloudRate";
        public float CloudRateUpgradeCost
        {
            get => PlayerPrefs.GetFloat(cloudRateUpgradeCost);
            set => PlayerPrefs.GetFloat(cloudRateUpgradeCost, value);
        }
        public float CloudRate
        {
            get => PlayerPrefs.GetFloat(cloudRate);
            set => PlayerPrefs.SetFloat(cloudRate, value);
        }
        
        #endregion
        #region BUCKET CAPACITY

        private const string bucketCapacity = "BucketCapacity";
        private const string bucketCapacityUpgradeCost = "BucketCapacityUpgradeCost";
        
        public float BucketCapacity
        {
            get => PlayerPrefs.GetFloat(bucketCapacity);
            set => PlayerPrefs.SetFloat(bucketCapacity, value);
        }

        public float BucketCapacityUpgradeCost
        {
            get => PlayerPrefs.GetFloat(bucketCapacityUpgradeCost);
            set => PlayerPrefs.SetFloat(bucketCapacityUpgradeCost, value);
        }

        #endregion
        #region BULLET
        private const string bulletPowerIncreaseCost = "BulletPowerIncreaseCost";
        
        private const string bulletPower = "BulletPower";

        public float BulletPower
        {
            get => PlayerPrefs.GetFloat(bulletPower);
            set => PlayerPrefs.SetFloat(bulletPower, value);
        }

        public float BulletPowerUpgradeCost
        {
            get => PlayerPrefs.GetFloat(bulletPowerIncreaseCost);
            set => PlayerPrefs.SetFloat(bulletPowerIncreaseCost, value);
        }

        public float BulletLevel
        {
            get => PlayerPrefs.GetFloat("BulletLevel");
            set => PlayerPrefs.SetFloat("BulletLevel", value);
        }
        #endregion
        #region FAUCET
        private const string faucetRate = "FaucetRate";
        private const string faucetRateUpgradeCost = "FaucetRateUpgradeCost";
        public float FaucetRate
        {
            get => PlayerPrefs.GetFloat(faucetRate);
            set => PlayerPrefs.GetFloat(faucetRate, value);
        }
        public float FaucetRateUpgradeCost
        {
            get => PlayerPrefs.GetFloat(faucetRateUpgradeCost);
            set => PlayerPrefs.SetFloat(faucetRateUpgradeCost, value);
        }
        #endregion
        #region MONEY
        private const string moneyValue = "MoneyValue";
        private const string moneyValueUpgradecost = "MoneyValueUpgradeCost";
        public float MoneyValue
        {
            get => PlayerPrefs.GetFloat(moneyValue);
            set => PlayerPrefs.SetFloat(moneyValue, value);
        }
        public float MoneyValueUpgradeCost
        {
            get => PlayerPrefs.GetFloat(moneyValueUpgradecost);
            set => PlayerPrefs.SetFloat(moneyValueUpgradecost, value);
        }
        #endregion

        public int IsInitialized
        {
            get => PlayerPrefs.GetInt("IsInit");
            set => PlayerPrefs.SetInt("IsInit", value);
        }

        #region GenericGetAndSet

        public float GenericGet(UpgradeTypes type)
        {
            string key = PurchaseHandler.DictionaryUpgradeTypes[type];
            return PlayerPrefs.GetFloat(key);
        }

        public void GenericSet(UpgradeTypes type, float value)
        {
            string key = PurchaseHandler.DictionaryUpgradeTypes[type];
            PlayerPrefs.SetFloat(key, value);
        }

        public float GenericGet(ValueTypes type)
        {
            string key = PurchaseHandler.DictionaryValueTypes[type];
            return PlayerPrefs.GetFloat(key);
        }
        public void GenericSet(ValueTypes type, float value)
        {
            string key = PurchaseHandler.DictionaryValueTypes[type];
            PlayerPrefs.SetFloat(key, value);
        }
        #endregion
        #region PRE-MADE
        public AudioSettings LoadAudioSettings()
        {
            return PlayerPrefsUtils.Read<AudioSettings>(k_AudioSettings);
        }
        public void SaveAudioSettings(AudioSettings audioSettings)
        {
            PlayerPrefsUtils.Write(k_AudioSettings, audioSettings);
        }
        public bool IsQualityLevelSaved => PlayerPrefs.HasKey(k_QualityLevel);
        public int QualityLevel 
        { 
            get => PlayerPrefs.GetInt(k_QualityLevel); 
            set => PlayerPrefs.SetInt(k_QualityLevel, value);
        }
        #endregion

    }
}