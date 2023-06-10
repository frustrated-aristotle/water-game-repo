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
            Debug.Log($"Current money multiplier is {GameManager.Instance.MoneyMultipler}");
            GameManager.Instance.MoneyMultipler = 0.2f;
            Debug.Log($"New money multiplier is {GameManager.Instance.MoneyMultipler}");

        }
    }
}