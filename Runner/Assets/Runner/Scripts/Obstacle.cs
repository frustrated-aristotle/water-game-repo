using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// Ends the game on collision, forcing a lose state.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class Obstacle : Spawnable
    {
        const string k_PlayerTag = "Player";

        private Inventory inventory;

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag))
            {
               //GameManager.Instance.Lose();
               Inventory.Instance.BucketFilledAmount = -10;
            }
        }
    }
}