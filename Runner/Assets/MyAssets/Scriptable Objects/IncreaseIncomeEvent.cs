using HyperCasual.Core;
using HyperCasual.Runner;
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
            VariableManager.Instance.OnIncomeIncreasePurchased();
        }
    }
}