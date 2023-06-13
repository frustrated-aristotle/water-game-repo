using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillAcid : MonoBehaviour
    {
        //Some mechanics will not be used in this version of the game:
        //1,2
        //Since these mechanics are not working, we do not need their visuals
        //3-
        public void FillTheBucket(Inventory inventory, float rate)
        {
            if (inventory.BucketCapacity - rate <= inventory.BucketCapacity + rate)
            {
                inventory.acidAmount += rate;
                
                //1-inventory.bucketFilledAmount += rate;
                //2-inventory.bucketCapacity -= rate;
            }
                
            TextMeshPro textMP = GameObject.Find("playerAcidText").GetComponent<TextMeshPro>(); 
            textMP.text = inventory.acidAmount.ToString();
            TextMeshPro bucketText = GameObject.Find("playerCapacityText").GetComponent<TextMeshPro>(); 
           //3- bucketText.text = "Capacity: " + inventory.bucketCapacity + "\nEmpty Portion"+(inventory.bucketCapacity - inventory.bucketFilledAmount).ToString();
        }
        public void TakeTheEffect()
        {
        }
    }
}
