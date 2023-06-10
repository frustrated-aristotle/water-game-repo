using System;
using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEngine;

public class UpgradeFlowScreen : View
{
    [SerializeField] private HyperCasualButton upgradeFlowButton;
    [SerializeField] private AbstractGameEvent upgradeFlowEvent;

    private void OnEnable()
    {
        upgradeFlowButton.AddListener(OnUpgradeFlowButtonClick);
    }

    private void OnDisable()
    {
        upgradeFlowButton.RemoveListener(OnUpgradeFlowButtonClick);
    }

    private void OnUpgradeFlowButtonClick()
    {
        upgradeFlowEvent?.Raise();
    }
}
