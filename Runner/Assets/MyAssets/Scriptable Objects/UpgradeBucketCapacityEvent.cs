using HyperCasual.Core;
using HyperCasual.Runner;
using MyAssets.Scripts.PurchaseHandler;
using UnityEngine;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// The event is triggered when the player completes a level
    /// </summary>
    [CreateAssetMenu(fileName = nameof(UpgradeBucketCapacityEvent),
        menuName = "Runner/" + nameof(UpgradeBucketCapacityEvent))]
    public class UpgradeBucketCapacityEvent : AbstractGameEvent
    {
        public override void Reset()
        {
            PurchaseHandler.PurchaseUpgradeClicked(UpgradeTypes.CAPACITY_UPGRADE, ValueTypes.CAPACITY);
        }
    }
}
