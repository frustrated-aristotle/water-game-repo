using HyperCasual.Core;
using HyperCasual.Runner;
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
            VariableManager.Instance.IncreaseCloudRate();
            //Need to find all clouds and then give them this. 
            Debug.LogError($"FLOW RATE MUST BE INCREASED : {VariableManager.Instance.CloudRate}");
        }
    }
}