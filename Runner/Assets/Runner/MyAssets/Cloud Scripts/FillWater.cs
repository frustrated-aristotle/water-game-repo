using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillWater : MonoBehaviour,IFillTheBucket
    {
        public void FillTheBucket( Inventory inventory,float rate)
        {
            if (inventory.bucketCapacity <= inventory.bucketCapacity + rate)
            {
                inventory.waterAmount += rate;
                inventory.bucketFilled += rate;
            }
                
            TextMeshPro textMP = GameObject.Find("playerWaterText").GetComponent<TextMeshPro>(); 
            textMP.text = inventory.waterAmount.ToString();
            TextMeshPro bucketText = GameObject.Find("playerCapacityText").GetComponent<TextMeshPro>();
            bucketText.text = "Capacity: " + inventory.bucketCapacity + "\nEmpty Portion"+(inventory.bucketCapacity - inventory.bucketFilled).ToString();
        }

        public void TakeTheEffect()
        {
        }
    }
}
