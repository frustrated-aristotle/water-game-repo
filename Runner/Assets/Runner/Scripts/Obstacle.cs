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
        private void Start()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag))
            {
               //GameManager.Instance.Lose();
               inventory.BucketFilledAmount = -10;
               TextMeshPro textMP = GameObject.Find("playerWaterText").GetComponent<TextMeshPro>(); 
               textMP.text = inventory.BucketFilledAmount.ToString();
               TextMeshPro bucketText = GameObject.Find("playerCapacityText").GetComponent<TextMeshPro>();
               bucketText.text = "Capacity: " + inventory.BucketCapacity + "\nEmpty Portion"+(inventory.BucketCapacity - inventory.BucketFilledAmount).ToString();
            }
        }
    }
}