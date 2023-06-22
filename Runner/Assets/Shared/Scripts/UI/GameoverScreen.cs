using System;
using HyperCasual.Core;
using HyperCasual.Gameplay;
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


        public static GameoverScreen Instance;
        private void Awake()
        {
            Instance = this;
        }

        void OnEnable()
        {
            increaseBulletPowerButton.AddListener(OnIncreaseBulletPowerButtonClick);
            increaseIncomeButton.AddListener(OnIncreaseIncomeButtonClick);
            proceedToResultButton.AddListener(OnProceedToResultButtonClick);
            UpdateGameEndUpgradeButtons();
            //m_PlayAgainButton.AddListener(OnPlayAgainButtonClick);
            //m_GoToMainMenuButton.AddListener(OnGoToMainMenuButtonClick);
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
            Debug.Log("Updated");
            int powerInt = (int)VariableManager.Instance.BulletPowerIncreaseCost;
            int incomeInt = (int)VariableManager.Instance.IncomeIncreaseCost;  
            Hud.Instance.GoldValue = Inventory.Instance.TotalGold;
            incomeText.text = incomeInt.ToString();
            bulletPowerText.text = powerInt.ToString();
            incomeText.color = Color.white;
            bulletPowerText.color = Color.white;
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