using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillSoil : MonoBehaviour, IFillTheBucket
    { 
        //Some mechanics will not be used in this version of the game:
        //1
        //Since these mechanics are not working, we do not need their visuals
        //2
        public void FillTheBucket( Inventory inventory ,float rate)
        {
            if (inventory.BucketCapacity <= inventory.BucketCapacity + rate)
            {
                inventory.soilAmount += rate;
                //1- inventory.bucketFilledAmount += rate; 
            }
               
            TextMeshPro textMP = GameObject.Find("playerSoilText").GetComponent<TextMeshPro>(); 
            textMP.text = inventory.soilAmount.ToString();
            TextMeshPro bucketText = GameObject.Find("playerCapacityText").GetComponent<TextMeshPro>();
            //2-bucketText.text = "Capacity: " + inventory.bucketCapacity + "\nEmpty Portion"+(inventory.bucketCapacity - inventory.bucketFilledAmount).ToString();
        }

        public void TakeTheEffect()
        {
            
        }
    }
}
