using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillSoil : MonoBehaviour, IFillTheBucket
    {
        public void FillTheBucket( Inventory inventory ,float rate)
        {
            if (inventory.bucketCapacity <= inventory.bucketCapacity + rate)
            {
                inventory.soilAmount += rate;
                inventory.bucketFilled += rate; 
            }
               
            TextMeshPro textMP = GameObject.Find("playerSoilText").GetComponent<TextMeshPro>(); 
            textMP.text = inventory.soilAmount.ToString();
            TextMeshPro bucketText = GameObject.Find("playerCapacityText").GetComponent<TextMeshPro>();
            bucketText.text = "Capacity: " + inventory.bucketCapacity + "\nEmpty Portion"+(inventory.bucketCapacity - inventory.bucketFilled).ToString();
        }

        public void TakeTheEffect()
        {
            
        }
    }
}
