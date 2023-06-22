using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEngine;
using UnityEngine.UI;

public class ValueManipulatorView : View
{
    [SerializeField] private HyperCasualButton saveButton;
    [SerializeField] private InputField bulletPowerStartingCost;

    [SerializeField] private AbstractGameEvent saveInputsEvent;
    private void OnEnable()
    {
        saveButton.AddListener(OnSaveButtonClicked);
    }

    private void OnDisable()
    {
        saveButton.RemoveListener(OnSaveButtonClicked);
    }

    private void OnSaveButtonClicked()
    {
        //ValueManipulator.InitInputs(float.Parse(bulletPowerStartingCost.text));
        saveInputsEvent.Raise();
    }
}


