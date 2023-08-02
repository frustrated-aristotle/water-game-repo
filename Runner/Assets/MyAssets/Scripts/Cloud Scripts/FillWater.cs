using HyperCasual.Core;
using TMPro;
using UnityEngine;

namespace HyperCasual.Runner
{
    public class FillWater : MonoBehaviour,IFillTheBucket
    {
        public void FillTheBucket( Inventory inventory, string key)
        {
            int rate = (int)PlayerPrefs.GetFloat(key);
            if (inventory.BucketCapacity >= inventory.BucketFilledAmount + rate)
            {
                inventory.BucketFilledAmount = rate;
            }
            if (inventory.BucketCapacity < inventory.BucketFilledAmount)
            {
                inventory.MakeBucketFilledAmountEqualToCapacity();
            }
        }
        public void TakeTheEffect()
        {
        }
    }
}
