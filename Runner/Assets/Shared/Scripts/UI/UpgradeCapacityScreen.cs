using HyperCasual.Core;
using HyperCasual.Runner;
using TMPro;
using UnityEngine;

public class UpgradeCapacityScreen : View
{

    [SerializeField] private HyperCasualButton upgradeCapacityButton;

    [SerializeField] private AbstractGameEvent upgradeEvent;

    private void OnEnable()
    {
        upgradeCapacityButton.AddListener(OnUpgradeCapacityButtonClicked);
    }

    private void OnDisable()
    {
        upgradeCapacityButton.RemoveListener(OnUpgradeCapacityButtonClicked);
    }

    private void OnUpgradeCapacityButtonClicked()
    {
        upgradeEvent.Raise();
    }

}
