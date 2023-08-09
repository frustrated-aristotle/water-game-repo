using System;
using HyperCasual.Core;
using HyperCasual.Gameplay;
using MyAssets.Scripts.PurchaseHandler;
using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// This View contains Game-over Screen functionalities
    /// </summary>
    public class GameoverScreen : View
    {
        [SerializeField]
        HyperCasualButton m_PlayAgainButton;
        [SerializeField]
        HyperCasualButton m_GoToMainMenuButton;
        [SerializeField]
        AbstractGameEvent m_PlayAgainEvent;
        [SerializeField]
        AbstractGameEvent m_GoToMainMenuEvent;
        [SerializeField]
        TextMeshProUGUI incomeText;
        [SerializeField] 
        TextMeshProUGUI bulletPowerText;
        
        //buttons
        [SerializeField] private HyperCasualButton increaseIncomeButton;
        [SerializeField] private HyperCasualButton increaseBulletPowerButton;
        [SerializeField] private HyperCasualButton proceedToResultButton;
        
        //Events
        [SerializeField] private AbstractGameEvent increaseBulletPowerEvent;
        [SerializeField] private AbstractGameEvent increaseIncomeEvent;
        [SerializeField] private AbstractGameEvent proceedToResultEvent;

        //Gameobjects
        public GameObject bulletGO, incomeGO;
        public static GameoverScreen Instance;
        private void Awake()
        {
            Instance = this;
           
        }

        public void DeactivateBulletUpgrade(GameObject go)
        {
            go.SetActive(false);
        }
        void OnEnable()
        {
            increaseBulletPowerButton.AddListener(OnIncreaseBulletPowerButtonClick);
            increaseIncomeButton.AddListener(OnIncreaseIncomeButtonClick);
            proceedToResultButton.AddListener(OnProceedToResultButtonClick);
            UpdateText(UpgradeTypes.CLOUD_UPGRADE);
            PurchaseHandler.UpdateTexts += UpdateText;
            //m_PlayAgainButton.AddListener(OnPlayAgainButtonClick);
            //m_GoToMainMenuButton.AddListener(OnGoToMainMenuButtonClick);
            
            //We will disable all bullets that are active in our scene at this point    
            BulletMovement[] bullets = GameObject.FindObjectsOfType<BulletMovement>();
            foreach (BulletMovement bullet in bullets)
            {
                bullet.gameObject.SetActive(false);
            }
        }

        public void UpdateText(UpgradeTypes type)
        {
            Debug.Log("Income : " + SaveManager.Instance.MoneyValueUpgradeCost);
            incomeText.text = ((int)SaveManager.Instance.MoneyValueUpgradeCost).ToString();
            bulletPowerText.text = ((int)SaveManager.Instance.BulletPowerUpgradeCost).ToString();
            PurchaseRelatedStatics.UpdateTextColors(incomeText, bulletPowerText, UpgradeTypes.MONEY_UPGRADE, UpgradeTypes.BULLETPOWER_UPGRADE);
        }


        void OnDisable()
        {
            increaseBulletPowerButton.RemoveListener(OnIncreaseBulletPowerButtonClick);
            increaseIncomeButton.RemoveListener(OnIncreaseIncomeButtonClick);
            proceedToResultButton.RemoveListener(OnProceedToResultButtonClick);
            //m_PlayAgainButton.RemoveListener(OnPlayAgainButtonClick);
            //m_GoToMainMenuButton.RemoveListener(OnGoToMainMenuButtonClick);
        }

        void OnPlayAgainButtonClick()
        {
            m_PlayAgainEvent.Raise();
        }

        void OnGoToMainMenuButtonClick()
        {
            //increaseIncomeEvent.Raise();
//            m_GoToMainMenuEvent.Raise();
        }
        
        //PlayAgainEvent starts the game.
        //GoToMainMenu is, obviously ha.
        private void OnIncreaseIncomeButtonClick()
        {
            increaseIncomeEvent.Raise();
            //IncreaseIncomeEvent.Raise();
        }

        private void OnIncreaseBulletPowerButtonClick()
        {
            increaseBulletPowerEvent.Raise();
        }
        private void OnProceedToResultButtonClick()
        {
            proceedToResultEvent.Raise();
        }
        public void UpdateGameEndUpgradeButtons()
        {
            //PurchaseHandler.UpdateText(ref incomeText, ref bulletPowerText, UpgradeTypes.MONEY_UPGRADE, UpgradeTypes.BULLETPOWER_UPGRADE);
        }
        public void UpdateGameEndUpgradeButtons(Color color , bool isItIncome)
        {
            if (isItIncome)
            {
                int incomeInt = (int)VariableManager.Instance.IncomeIncreaseCost;
                incomeText.text = incomeInt.ToString();
                incomeText.color = color;
            }
            else
            {
                int powerInt = (int)VariableManager.Instance.BulletPowerIncreaseCost;
                bulletPowerText.text = powerInt.ToString();
                bulletPowerText.color = color;
            }
        }
    }
}