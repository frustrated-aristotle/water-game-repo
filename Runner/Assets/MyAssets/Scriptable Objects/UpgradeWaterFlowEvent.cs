using HyperCasual.Core;
using HyperCasual.Runner;
using MyAssets.Scripts.PurchaseHandler;
using UnityEngine;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// The event is triggered when the player completes a level
    /// </summary>
    [CreateAssetMenu(fileName = nameof(UpgradeWaterFlowEvent),
        menuName = "Runner/" + nameof(UpgradeWaterFlowEvent))]
    public class UpgradeWaterFlowEvent : AbstractGameEvent
    {
        public override void Reset()
        {
            PurchaseHandler.PurchaseUpgradeClicked(UpgradeTypes.CLOUD_UPGRADE, ValueTypes.CLOUD);
        }
    }
}