using HyperCasual.Core;
using MyAssets.Scripts.PurchaseHandler;
using UnityEngine;

namespace HyperCasual.Runner
{

    /// <summary>
    /// The event is triggered when the player hit the increase bullet power button
    /// </summary>
    [CreateAssetMenu(fileName = nameof(BulletPowerIncreaseEvent),
    menuName = "Runner/" + nameof(BulletPowerIncreaseEvent))]
    public class BulletPowerIncreaseEvent : AbstractGameEvent
    {
        public override void Reset()
        {
            PurchaseHandler.PurchaseUpgradeClicked(UpgradeTypes.BULLETPOWER_UPGRADE, ValueTypes.BULLETPOWER);
        }
    }
}