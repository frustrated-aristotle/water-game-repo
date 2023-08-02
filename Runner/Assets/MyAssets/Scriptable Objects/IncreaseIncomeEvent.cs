using HyperCasual.Core;
using HyperCasual.Runner;
using MyAssets.Scripts.PurchaseHandler;
using UnityEngine;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// The event is triggered when the player hit the increase income button
    /// </summary>
    [CreateAssetMenu(fileName = nameof(IncreaseIncomeEvent),
        menuName = "Runner/" + nameof(IncreaseIncomeEvent))]
    public class IncreaseIncomeEvent : AbstractGameEvent
    {
        public override void Reset()
        {
            PurchaseHandler.PurchaseUpgradeClicked(UpgradeTypes.MONEY_UPGRADE, ValueTypes.MONEY);
        }
    }
}