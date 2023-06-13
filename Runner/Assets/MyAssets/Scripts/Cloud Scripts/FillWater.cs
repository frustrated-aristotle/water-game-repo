using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillWater : MonoBehaviour,IFillTheBucket
    {
        public void FillTheBucket( Inventory inventory,int rate)
        {
            if (inventory.BucketCapacity <= inventory.BucketCapacity + rate)
            {
                Debug.Log("Rate: " + rate + " and BucketCapacity: " + inventory.BucketCapacity);
                inventory.BucketFilledAmount = rate;
                Debug.Log("BucketFilledAmount : " + inventory.BucketFilledAmount + " and the bucket capacity is " + inventory.BucketCapacity );
            }
        }
        public void TakeTheEffect()
        {
        }
    }
}
