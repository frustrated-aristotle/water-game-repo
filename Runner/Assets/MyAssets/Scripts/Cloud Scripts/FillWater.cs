using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillWater : MonoBehaviour,IFillTheBucket
    {
        public void FillTheBucket( Inventory inventory,int rate)
        {
            if (inventory.BucketCapacity >= inventory.BucketFilledAmount + rate)
            {
                inventory.BucketFilledAmount = rate;
            }

            if (inventory.BucketCapacity < inventory.BucketFilledAmount)
            {
                inventory.BucketFilledAmount = inventory.BucketCapacity;
            }
        }
        public void TakeTheEffect()
        {
        }
    }
}
