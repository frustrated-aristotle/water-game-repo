using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEngine;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// The event is triggered when the player completes a level
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ProceedToResultEvent),
        menuName = "Runner/" + nameof(ProceedToResultEvent))]
    public class ProceedToResultEvent : AbstractGameEvent
    {
        public override void Reset()
        {
            //TODO: GET THE AMOUNT OF MONEY THAT IS COLLECTED DURING THE PREVIOUS RUN,
            Inventory.Instance.SaveInventory();
        }
    }
}