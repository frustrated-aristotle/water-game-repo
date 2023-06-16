using System;
using HyperCasual.Core;
using HyperCasual.Runner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// This View contains head-up-display functionalities
    /// </summary>
    public class Hud : View
    {
        [SerializeField]
        TextMeshProUGUI m_GoldText;
        [SerializeField]
        Slider m_XpSlider;
        [SerializeField]
        HyperCasualButton m_PauseButton;
        [SerializeField]
        AbstractGameEvent m_PauseEvent;
        
        [SerializeField]
        TextMeshProUGUI capacityText;

        [SerializeField] 
        TextMeshProUGUI flowText;


        public static Hud Instance;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// The slider that displays the XP value 
        /// </summary>
        public Slider XpSlider => m_XpSlider;

        int m_GoldValue;
        
        /// <summary>
        /// The amount of gold to display on the hud.
        /// The setter method also sets the hud text.
        /// </summary>
        public int GoldValue
        {
            get => m_GoldValue;
            set
            {
                if (m_GoldValue != value)
                {
                    Debug.Log("a" + this.name);
                    m_GoldValue = value;
                    m_GoldText.text = GoldValue.ToString();
                }
            }
        }

        float m_XpValue;
        
        /// <summary>
        /// The amount of XP to display on the hud.
        /// The setter method also sets the hud slider value.
        /// </summary>
        public float XpValue
        {
            get => m_XpValue;
            set
            {
                if (!Mathf.Approximately(m_XpValue, value))
                {
                    m_XpValue = value;
                    m_XpSlider.value = m_XpValue;
                }
            }
        }

        void OnEnable()
        {
            m_PauseButton.AddListener(OnPauseButtonClick);
            GoldValue = Inventory.Instance.m_TotalGold;
            ToggleButtonActiveState(true);
        }

        public void ToggleButtonActiveState(bool willBeActive)
        {
            if (willBeActive)
            {
                UpdateUpgradeButtonText();
            }
            var childCount = transform.childCount;
            transform.GetChild(childCount-1).gameObject.SetActive(willBeActive);
            transform.GetChild(childCount-2).gameObject.SetActive(willBeActive);
        }
        void OnDisable()
        {
            m_PauseButton.RemoveListener(OnPauseButtonClick);
        }

        void OnPauseButtonClick()
        {
            m_PauseEvent.Raise();
        }

        public void UpdateUpgradeButtonText()
        {
            int flowInt = (int)VariableManager.Instance.CloudRateIncreaseCost;
            int capacityInt = (int)VariableManager.Instance.BucketCapacityIncreaseCost;
            GoldValue = Inventory.Instance.TotalGold;
            flowText.text = flowInt.ToString();
            capacityText.text = capacityInt.ToString();
        }

        public void UpdateUpgradeButtonText(Color color , bool isItFlowText)
        {
            if (isItFlowText)
            {
                int flowInt = (int)VariableManager.Instance.CloudRateIncreaseCost;
                flowText.text = flowInt.ToString();
                flowText.color = color;
            }
            else
            {
                int capacityInt = (int)VariableManager.Instance.BucketCapacityIncreaseCost;
                capacityText.text = capacityInt.ToString();
                capacityText.color = color;
            }
        }
    }
}
