using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillAcid : MonoBehaviour, IFillTheBucket
    {
        public void FillTheBucket(Inventory inventory, float rate)
        {
            if (inventory.bucketCapacity - rate <= inventory.bucketCapacity + rate)
            {
                inventory.acidAmount += rate;
                inventory.bucketFilled += rate;
                inventory.bucketCapacity -= rate;
            }
                
            TextMeshPro textMP = GameObject.Find("playerAcidText").GetComponent<TextMeshPro>(); 
            textMP.text = inventory.acidAmount.ToString();
            TextMeshPro bucketText = GameObject.Find("playerCapacityText").GetComponent<TextMeshPro>(); 
            bucketText.text = "Capacity: " + inventory.bucketCapacity + "\nEmpty Portion"+(inventory.bucketCapacity - inventory.bucketFilled).ToString();
        }
        public void TakeTheEffect()
        {
        }
    }
}
