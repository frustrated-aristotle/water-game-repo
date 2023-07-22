using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillWater : MonoBehaviour,IFillTheBucket
    {
        public void FillTheBucket( Inventory inventory,int rate)
        {
            Debug.Log("ASAS: log Inventory bucket filled amount" + inventory.BucketFilledAmount + " filled amount: " + inventory.BucketFilledAmount);
            if (inventory.BucketCapacity >= inventory.BucketFilledAmount + rate)
            {
                Debug.Log("ASAS: FillBucket first if " + inventory.BucketCapacity + " filled amount: " + inventory.BucketFilledAmount );
                inventory.BucketFilledAmount = rate;
            }
            if (inventory.BucketCapacity < inventory.BucketFilledAmount)
            {
                Debug.Log("ASAS: FillBucket second if" + inventory.BucketCapacity + " filled amount: " + inventory.BucketFilledAmount);
                //inventory.BucketFilledAmount = inventory.BucketCapacity;
                inventory.MakeBucketFilledAmountEqualToCapacity();
            }
        }
        public void TakeTheEffect()
        {
        }
    }
}
