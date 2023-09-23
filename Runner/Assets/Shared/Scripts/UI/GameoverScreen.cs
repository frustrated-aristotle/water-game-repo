using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
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

        public GameObject LookAtObject;
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
        
        public GameObject pena;
        private bool isDirectionRight = true;
        private bool atRightWing = false;

        [SerializeField] 
        private Vector3 penaLeftPos, penaRightPos, penaMidPos;
        private List<Tuple<bool, Vector3>> targetTuples = new List<Tuple<bool, Vector3>>();
        private Tuple<bool, Vector3> currentTuple = null;

        //private float pickSpeed = 6f;

        

        private void Awake()
        {
            Instance = this;
            targetTuples.Add(new Tuple<bool, Vector3>(false, new Vector3(0,80,0)));           
            targetTuples.Add(new Tuple<bool, Vector3>(false, new Vector3(-300,40,0)));           
            targetTuples.Add(new Tuple<bool, Vector3>(false, new Vector3(0,80,0)));           
            targetTuples.Add(new Tuple<bool, Vector3>(false, new Vector3(-300,40,0)));       
        }

        public float[] times = {6,6,8,8,8,6,6 , 6,8,8,8,8,6,6};
        private int timerIndex = 0;
//Index = 0, 1 , 2 , 3, 4, 5, 6 7 , 8 , 9 , 10 , 11
        private bool isTimerOn = true;
        private int targetIndex = 0;
        private float multiplier = 1.5f;
        public TextMeshProUGUI multiplierText;
        public TextMeshProUGUI multipliedTempGoldText;

        private float Multiplier()
        {
            float f = 1.5f;
            switch (timerIndex)
            {
                case 0:
                    f = 1.5f;
                    break;
                case 1:
                    f = 2f;
                    break;
                case 2:
                    f = 2.5f;
                    break;
                case 3:
                    f = 3f;
                    break;
                case 4:
                    f = 2.5f;
                    break;
                case 5:
                    f = 2f;
                    break;
                case 6:
                    f = 1.5f;
                    break;
                case 7:
                    f = 1.5f;
                    break;
                case 8:
                    f = 2f;
                    break;
                case 9:
                    f = 2.5f;
                    break;
                case 10:
                    f = 3f;
                    break;
                case 11:
                    f = 2.5f;
                    break;
                case 12:
                    f = 2f;
                    break;
                case 13:
                    f = 1.5f;
                    break;
            }
            return f;
        }
        private Vector3[] targetPoses =
        {
            new Vector3(-200,45,0),
            new Vector3(-100,55,0),
            new Vector3(-50,55,0),
            new Vector3(0,60,0),
            new Vector3(50,55,0),
            new Vector3(100,55,0),
            new Vector3(200,45,0),
            new Vector3(300,40,0),
            new Vector3(200,45,0),
            new Vector3(100,55,0),
            new Vector3(50,55,0),
            new Vector3(0,60,0),
            new Vector3(-50,55,0),
            new Vector3(-100,55,0),
            new Vector3(-200,45,0),
            new Vector3(-300,40,0),
        };

        private static float firstStep = 6.5f;
        private static float secondStep = 4.75f;
        private static float thirdStep = 3.25f;
        private Vector3[] rotations =
        {
            new Vector3(0,0,firstStep),
            new Vector3(0,0,secondStep),
            new Vector3(0,0,thirdStep),
            new Vector3(0,0,0),
            new Vector3(0,0,-thirdStep),
            new Vector3(0,0,-secondStep),
            new Vector3(0,0,-firstStep),
            new Vector3(0,0,-7.93f),
            new Vector3(0,0,-firstStep),
            new Vector3(0,0,-secondStep),
            new Vector3(0,0,-thirdStep),
            new Vector3(0,0,0),
            new Vector3(0,0,thirdStep),
            new Vector3(0,0,secondStep),
            new Vector3(0,0,firstStep),
            new Vector3(0,0,7.93f),
        };

        private void Update()
        {
            
            /*
            Vector3 mainTarget = new Vector3();
            mainTarget = targetPoses[targetIndex];
            Vector3 targetVector = mainTarget - pena.transform.localPosition;
            Vector3 normalizedTarget = targetVector.normalized;
            pena.transform.eulerAngles = rotations[targetIndex];
            pena.transform.localPosition += normalizedTarget * 10f;
            float penaY = pena.transform.localPosition.y;
            float penaX = pena.transform.localPosition.x;
            if (penaX + 7f  > mainTarget.x && penaX -7f < mainTarget.x)
            {
                Debug.Log("Pena is reached to X");
                if (penaY + 2.5f > mainTarget.y && penaY - 2.5f < mainTarget.y)
                {
                    Debug.Log("Pena is reached to Y");
                    pena.transform.localPosition = mainTarget;
                }
            }

            if (pena.transform.localPosition == mainTarget)
            {
                targetIndex++;
                if (targetIndex >= targetPoses.Length)
                {
                    targetIndex = 0;
                }
            }
            */
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
            Time.timeScale = 1f;
            PlayerController.Instance.m_TargetSpeed = 0;
            Invoke(nameof(ActivateGameObject), 2f);
            StartCoroutine(Timer());
        }
        
        private IEnumerator Timer()
        {
            while (isTimerOn)
            {
                yield return new WaitForSeconds(times[timerIndex] /60f);
                Debug.Log("Timer ended after : " + times[timerIndex] + " and index: " + timerIndex);
                multiplier = Multiplier();
                multiplierText.text =  "Claim " + multiplier.ToString() + "x";
                timerIndex++;
                float multipliedTempGold = Inventory.Instance.m_TempGold * multiplier;
                multipliedTempGoldText.text = "+"+multipliedTempGold.ToString();
                if (timerIndex >= times.Length)
                {
                    timerIndex = 0;
                    Debug.Log("It is 0 now");
                }
            }
        }
        private void ActivateGameObject()
        {
            proceedToResultButton.gameObject.SetActive(true); // GameObject'i aktif et
        }

       
        public void UpdateText(UpgradeTypes type)
        {
            if (gameObject.activeSelf)
            {
                //incomeText.text = ((int)SaveManager.Instance.MoneyValueUpgradeCost).ToString();
                //bulletPowerText.text = ((int)SaveManager.Instance.BulletPowerUpgradeCost).ToString();
                PurchaseRelatedStatics.UpdateTextColors(incomeText, bulletPowerText, UpgradeTypes.MONEY_UPGRADE, UpgradeTypes.BULLETPOWER_UPGRADE);
            }
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