

using System;
using HyperCasual.Core;
using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class ResultScreen : View
    {

        [SerializeField] private HyperCasualButton startSceneButton;

        [SerializeField] private AbstractGameEvent startSceneEvent;

        [SerializeField] private TextMeshProUGUI collectedMoneyText;

        [SerializeField] private GameObject noThanksButton;
        private void OnEnable()
        {
            startSceneButton.AddListener(OnStartSceneButtonClicked);
            InitCollectedMoney();
            Inventory.Instance.MakeBucketLevelZero();
            Invoke(nameof(ActivateGameObject), 2f);
            Time.timeScale = 1f;
        }
        
        private void ActivateGameObject()
        {
            noThanksButton.SetActive(true);
        }
        private void OnDisable()
        {
            startSceneButton.RemoveListener(OnStartSceneButtonClicked);
        }

        private void OnStartSceneButtonClicked()
        {
            startSceneEvent.Raise();            
        }

        private void InitCollectedMoney()
        {
            collectedMoneyText.text = Inventory.Instance.m_TempGold.ToString();
        }
    }
}