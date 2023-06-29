using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
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
        const string initBulletPowerIncreaseCost = "InitBulletPowerIncreaseCost";
        const string moneyAmountMultiplier= "MoneyAmountMultiplier";
        const string incomeIncreaseCostMultiplier = "IncomeIncreaseCostMultiplier";
        const string bulletPowerIncreaseCostMultiplier = "BulletPowerIncreaseCostMultiplier";
        const string bucketCapacityIncreaseCostMultiplier = "BucketCapacityIncreaseCostMultiplier";
        const string cloudRateCostMultiplier = "CloudRateCostMultiplier";
        const string cloudRateMultiplier="ClooudMultiplier";
        const string cloudRate = "CloudRate";
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

        public int InitialBulletPowerIncreaseCost
        {
            get => PlayerPrefs.GetInt(initBulletPowerIncreaseCost);
            set => PlayerPrefs.SetInt(initBulletPowerIncreaseCost, value);
        }

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

        public int Capacity
        {
            get => PlayerPrefs.GetInt(capacity);
            set => PlayerPrefs.SetInt(capacity, value);
        }
        public float XP
        {
            get => PlayerPrefs.GetFloat(k_Xp); 
            set => PlayerPrefs.SetFloat(k_Xp, value);
        }

        public bool IsQualityLevelSaved => PlayerPrefs.HasKey(k_QualityLevel);
        
        public int QualityLevel 
        { 
            get => PlayerPrefs.GetInt(k_QualityLevel); 
            set => PlayerPrefs.SetInt(k_QualityLevel, value);
        }

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

        public float CloudRateMultiplier 
        { 
            get => PlayerPrefs.GetFloat(cloudRateMultiplier);
            set => PlayerPrefs.SetFloat(cloudRateMultiplier, value); 
        }

        public float CloudRateCostMultiplier
        {
            get => PlayerPrefs.GetFloat(cloudRateCostMultiplier);
            set => PlayerPrefs.SetFloat(cloudRateCostMultiplier, value);
        }

        public float BucketCapacityIncreaseCostMultiplier
        {
            get => PlayerPrefs.GetFloat(bucketCapacityIncreaseCostMultiplier);
            set => PlayerPrefs.SetFloat(bucketCapacityIncreaseCostMultiplier, value);
        }

        public float BulletPowerIncreaseCostMultiplier
        {
            get => PlayerPrefs.GetFloat(bulletPowerIncreaseCostMultiplier);
            set => PlayerPrefs.SetFloat(bulletPowerIncreaseCostMultiplier, value);
        }

        public float IncomeIncreaseCostMultiplier
        {
            get => PlayerPrefs.GetFloat(incomeIncreaseCostMultiplier); 
            set => PlayerPrefs.SetFloat(incomeIncreaseCostMultiplier, value);   
        }

        public float MoneyAmountMultiplier
        {
            get => PlayerPrefs.GetFloat(moneyAmountMultiplier);
            set => PlayerPrefs.SetFloat(moneyAmountMultiplier, value);  
        }
        public AudioSettings LoadAudioSettings()
        {
            return PlayerPrefsUtils.Read<AudioSettings>(k_AudioSettings);
        }

        public void SaveAudioSettings(AudioSettings audioSettings)
        {
            PlayerPrefsUtils.Write(k_AudioSettings, audioSettings);
        }
    }
}