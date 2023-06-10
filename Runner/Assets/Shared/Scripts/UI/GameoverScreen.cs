using HyperCasual.Core;
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

        
        //buttons
        [SerializeField] private HyperCasualButton increaseIncomeButton;
        [SerializeField] private HyperCasualButton increaseBulletPowerButton;
        [SerializeField] private HyperCasualButton proceedToResultButton;
        
        //Events
        [SerializeField] private AbstractGameEvent increaseBulletPowerEvent;
        [SerializeField] private AbstractGameEvent increaseIncomeEvent;
        [SerializeField] private AbstractGameEvent proceedToResultEvent;

        void OnEnable()
        {
            increaseBulletPowerButton.AddListener(OnIncreaseBulletPowerButtonClick);
            increaseIncomeButton.AddListener(OnIncreaseIncomeButtonClick);
            proceedToResultButton.AddListener(OnProceedToResultButtonClick);
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
    }
}