using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Gameplay;
using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A simple inventory class that listens to game events and keeps track of the amount of in-game currencies
    /// collected by the player
    /// </summary>
    public class Inventory : AbstractSingleton<Inventory>
    {
        [SerializeField]
        GenericGameEventListener m_GoldEventListener;
        [SerializeField]
        GenericGameEventListener m_KeyEventListener;
        [SerializeField]
        GenericGameEventListener m_WinEventListener;
        [SerializeField]
        GenericGameEventListener m_LoseEventListener;

        public int m_TempGold;
        public int m_TotalGold;
        public int totalMoneyAmount;
        public int TotalGold { 
            get=> m_TotalGold;
            private set => m_TotalGold = value;

        }
        float m_TempXp;
        float m_TotalXp;
        int m_TempKeys;
        public float soilAmount;
        public float acidAmount;
        [SerializeField]
        private float bucketCapacity;
        [SerializeField]
        private float tempBucketCapacity;
        private float bucketFilledAmount;

        public float BucketCapacity 
        {
            get => tempBucketCapacity;
            set
            {
                tempBucketCapacity += value;
            }
        }
        public float BucketFilledAmount
        {
            get => bucketFilledAmount;
            set
            {
                bucketFilledAmount += value;
                UpdateWaterLevel();
            }
        }

        private void UpdateWaterLevel()
        {
            float filledRate = bucketFilledAmount / tempBucketCapacity * 100;
            float waterRate = -80 * filledRate / 100;
            //-80-waterrate
            SkinnedMeshRenderer child = PlayerController.Instance.transform.GetChild(5).GetChild(1)
                .GetComponent<SkinnedMeshRenderer>(); 
            child.SetBlendShapeWeight(1, -80-waterRate);
            TextMeshPro textMP = GameObject.Find("playerWaterText").GetComponent<TextMeshPro>(); 
            textMP.text = BucketFilledAmount.ToString();
            TextMeshPro bucketText = GameObject.Find("playerCapacityText").GetComponent<TextMeshPro>();
            bucketText.text = "Capacity: " + BucketCapacity + "\nEmpty Portion"+(BucketCapacity - BucketFilledAmount).ToString();
        }
        /// <summary>
        /// Temporary const
        /// Users keep accumulating XP when playing the game and they're rewarded as they hit a milestone.
        /// Milestones are simply a threshold to reward users for playing the game. We need to come up with
        /// a proper formula to calculate milestone values but because we don't have a plan for the milestone
        /// rewards yet, we have simple set the value to something users can never reach. 
        /// </summary>
        const float k_MilestoneFactor = 1.2f;

        Hud m_Hud;
        LevelCompleteScreen m_LevelCompleteScreen;

        void Start()
        {
            m_GoldEventListener.EventHandler = OnGoldPicked;
            m_KeyEventListener.EventHandler = OnKeyPicked;
            m_WinEventListener.EventHandler = OnWin;
            m_LoseEventListener.EventHandler = OnLose;
            
            m_TempGold = 0;
            m_TotalGold = SaveManager.Instance.Currency;
            bucketCapacity = SaveManager.Instance.Capacity;
            tempBucketCapacity = bucketCapacity;
            
            m_TempXp = 0;
            m_TotalXp = SaveManager.Instance.XP;
            m_TempKeys = 0;

            m_LevelCompleteScreen = UIManager.Instance.GetView<LevelCompleteScreen>();
            m_Hud = UIManager.Instance.GetView<Hud>();
        } 

        void OnEnable()
        {
            m_GoldEventListener.Subscribe();
            m_KeyEventListener.Subscribe();
            m_WinEventListener.Subscribe();
            m_LoseEventListener.Subscribe();
        }

        void OnDisable()
        {
            m_GoldEventListener.Unsubscribe();
            m_KeyEventListener.Unsubscribe();
            m_WinEventListener.Unsubscribe();
            m_LoseEventListener.Unsubscribe();
        }

        void OnGoldPicked()
        {
            if (m_GoldEventListener.m_Event is ItemPickedEvent goldPickedEvent)
            {
                Debug.LogError("GoldPicked");
                m_TempGold += goldPickedEvent.Count;
                m_TotalGold += goldPickedEvent.Count;
                m_Hud.GoldValue = m_TotalGold;
            }
            else
            {
                throw new Exception($"Invalid event type!");
            }
        }

        void OnKeyPicked()
        {
            if (m_KeyEventListener.m_Event is ItemPickedEvent keyPickedEvent)
            {
                m_TempKeys += keyPickedEvent.Count;
            }
            else
            {
                throw new Exception($"Invalid event type!");
            }
        }

        void OnWin()
        {
            SaveManager.Instance.Currency = m_TotalGold;

            m_LevelCompleteScreen.GoldValue = m_TotalGold;
            m_LevelCompleteScreen.XpSlider.minValue = m_TotalXp;
            m_LevelCompleteScreen.XpSlider.maxValue = k_MilestoneFactor * (m_TotalXp + m_TempXp);
            m_LevelCompleteScreen.XpValue = m_TotalXp + m_TempXp;

            m_LevelCompleteScreen.StarCount = m_TempKeys;

            m_TotalXp += m_TempXp;
            m_TempXp = 0f;
            SaveManager.Instance.XP = m_TotalXp;
        }

        void OnLose()
        {
            Debug.Log("OnLoseEvent");
            m_TotalXp += m_TempXp;
            m_TempXp = 0f;
            SaveManager.Instance.XP = m_TotalXp;
        }

        void Update()
        {
            if (m_Hud.gameObject.activeSelf)
            {
                m_TempXp += PlayerController.Instance.Speed * Time.deltaTime;
                m_Hud.XpValue = m_TempXp;
                
                if (SequenceManager.Instance.m_CurrentLevel is LoadLevelFromDef loadLevelFromDef)
                {
                    m_Hud.XpSlider.minValue = 0;
                    m_Hud.XpSlider.maxValue = loadLevelFromDef.m_LevelDefinition.LevelLength;
                }
            }
        }

        private int multiplier = 1;
        public void SaveMoney()
        {
            int extraMoney = (m_TempGold * multiplier) - m_TempGold;
            m_TempGold += extraMoney;
            SaveManager.Instance.Currency = m_TotalGold;
        }

        /// <summary>
        /// The main capacity will be changed on start screen by an upgrade button
        /// After that button is clicked, saving the capacity will be a must to do.
        /// </summary>
        public void SaveCapacity()
        {
            SaveManager.Instance.Capacity = (int)bucketCapacity;
        }

        public void SaveInventory()
        {
            SaveMoney();
            
        }
        
        public void IncreaseBucketCapacityFromStartingMenu()
        {
            bucketCapacity += 10;
            SaveCapacity();
            Debug.Log(bucketCapacity);
        }
    }
}
