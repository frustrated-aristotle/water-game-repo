using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Runner;
using TMPro;
using UnityEngine;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// The event is triggered when the player completes a level
    /// </summary>
    [CreateAssetMenu(fileName = nameof(SaveValuesEvent),
        menuName = "Runner/" + nameof(SaveValuesEvent))]
    public class SaveValuesEvent : AbstractGameEvent
    {
        public override void Reset()
        {
            ValueManipulator.Instance.InitInputs();
            ValueManipulator.Instance.SaveInputs();
        }
    }
}
