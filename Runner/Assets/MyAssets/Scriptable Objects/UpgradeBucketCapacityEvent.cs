using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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
             VariableManager.Instance.OnBucketCapacityIncreasePurchased();  
        }
    }
}
