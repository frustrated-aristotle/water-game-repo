using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Runner
{
    /// <summary>
    /// This View contains pause menu functionalities
    /// </summary>
    public class PauseMenu : View
    {
        [SerializeField]
        HyperCasualButton m_ContinueButton;

        [SerializeField]
        HyperCasualButton m_QuitButton;
       


        [SerializeField]
        AbstractGameEvent m_ContinueEvent;

        [SerializeField]
        AbstractGameEvent m_QuitEvent;
        
        
        [SerializeField] private HyperCasualButton saveButton;
        [SerializeField] private AbstractGameEvent saveEvent;

        [SerializeField] public TMP_InputField input;
        [SerializeField] public TMP_InputField input2;
        [SerializeField] public TMP_InputField input3;

        public static PauseMenu Instance;

        private void Awake()
        {
            Instance = this;
        }

        void OnEnable()
        {
            saveButton.AddListener(OnSaveClicked);
            m_ContinueButton.AddListener(OnContinueClicked);
            m_QuitButton.AddListener(OnQuitClicked);
            input.text = PlayerController.Instance.m_HorizontalSpeedFactor.ToString();
            input2.text = PlayerController.Instance.m_TargetSpeed.ToString();
        }

        void OnDisable()
        {
            saveButton.RemoveListener(OnSaveClicked);
            m_ContinueButton.RemoveListener(OnContinueClicked);
            m_QuitButton.RemoveListener(OnQuitClicked);
        }

        void OnContinueClicked()
        {
            m_ContinueEvent.Raise();
        }

        void OnQuitClicked()
        {
            m_QuitEvent.Raise();
        }

        void OnSaveClicked()
        {
            saveEvent.Raise();
        }

        public void ResetGame()
        {
            PlayerPrefs.DeleteAll();
            GameManager.Instance.LoadLevel(0);
        }
    }
}
