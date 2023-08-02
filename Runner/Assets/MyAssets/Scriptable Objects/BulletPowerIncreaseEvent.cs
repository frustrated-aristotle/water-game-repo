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
            Debug.Log("Capacity Cost Before: " + SaveManager.Instance.BucketCapacityUpgradeCost);
            PurchaseHandler.PurchaseUpgradeClicked(UpgradeTypes.BULLETPOWER_UPGRADE, ValueTypes.BULLETPOWER);
            Debug.Log("Capacity Cost After: " + SaveManager.Instance.BucketCapacityUpgradeCost);
            Debug.Log("Currency After: " + SaveManager.Instance.Currency);
            //VariableManager.Instance.OnBulletPowerIncreasePurchased();
        }
    }
}