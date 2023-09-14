using System;
using HyperCasual.Core;
using HyperCasual.Runner;
using MyAssets.Scripts.PurchaseHandler;
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
        
        //Hold and Move Text Related
        [SerializeField] 
        TextMeshProUGUI holdAndMoveText;

        [SerializeField] 
        private float startingFontSize = 50;
        [SerializeField] 
        private float endFontSize = 80;
        private float scaleUpStep = .75f;
       
        private bool canScaleUp = true;
        //Starting val : 50 end val : 80

        public static Hud Instance;

        private void Awake()
        {
            Instance = this;
        }

        
        private void FixedUpdate()
        {
            if (canScaleUp)
            {
                holdAndMoveText.fontSize += scaleUpStep;
            }
            else
            {
                holdAndMoveText.fontSize -= scaleUpStep;
            }
            if (canScaleUp && holdAndMoveText.fontSize >= endFontSize)
            {
                canScaleUp = false;
            }
            else if(!canScaleUp && holdAndMoveText.fontSize <= startingFontSize)
            {
                canScaleUp = true;
            }
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
            get => SaveManager.Instance.Currency;
            set
            {
                if (m_GoldValue != value)
                {
                    m_GoldValue = value;
                    m_GoldText.text = SaveManager.Instance.Currency.ToString();
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
            GoldValue = SaveManager.Instance.Currency;
            PurchaseHandler.UpdateTexts += UpdateText;
            ToggleButtonActiveState(true);
            UpdateText(UpgradeTypes.CLOUD_UPGRADE);
        }

        public void UpdateText(UpgradeTypes type)
        {
            capacityText.text = ((int)SaveManager.Instance.BucketCapacityUpgradeCost).ToString();
            flowText.text = ((int)SaveManager.Instance.CloudRateUpgradeCost).ToString();
            m_GoldText.text = SaveManager.Instance.Currency.ToString();
            PurchaseRelatedStatics.UpdateTextColors(flowText, capacityText, UpgradeTypes.CLOUD_UPGRADE, UpgradeTypes.CAPACITY_UPGRADE);
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
            PurchaseHandler.UpdateText(ref capacityText, ref flowText, UpgradeTypes.MONEY_UPGRADE, UpgradeTypes.BULLETPOWER_UPGRADE);
            GoldValue = Inventory.Instance.TotalGold;
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
