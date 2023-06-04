using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillWater : MonoBehaviour,IFillTheBucket
    {
        public void FillTheBucket( Inventory inventory,float rate)
        {
            if (inventory.BucketCapacity <= inventory.BucketCapacity + rate)
            {
                inventory.waterAmount += rate;
                inventory.BucketFilledAmount = rate;
            }
                
            TextMeshPro textMP = GameObject.Find("playerWaterText").GetComponent<TextMeshPro>(); 
            textMP.text = inventory.BucketFilledAmount.ToString();
            TextMeshPro bucketText = GameObject.Find("playerCapacityText").GetComponent<TextMeshPro>();
            bucketText.text = "Capacity: " + inventory.BucketCapacity + "\nEmpty Portion"+(inventory.BucketCapacity - inventory.BucketFilledAmount).ToString();
            }
        
        
        

        public void TakeTheEffect()
        {
        }
    }
}
