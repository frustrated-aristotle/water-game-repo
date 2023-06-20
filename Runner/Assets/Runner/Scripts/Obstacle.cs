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
        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag))
            {
               //GameManager.Instance.Lose();
               CameraManager.Instance.KamerayiTitret();
               Handheld.Vibrate();
               if (Inventory.Instance.BucketFilledAmount - 10 >= 0)
               {
                   Inventory.Instance.BucketFilledAmount = -10;
               }
               else
                   return;
            }
        }
    }
}