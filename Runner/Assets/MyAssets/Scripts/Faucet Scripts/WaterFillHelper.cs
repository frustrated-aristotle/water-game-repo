using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;

public static class WaterFillHelper
{
    public static void FillWater(Collider col, string playerTag, IFillTheBucket filler, int Rate)
    {
        Debug.Log("Inventory bucket capactiy: " + Inventory.Instance.tempBucketCapacity);
        if (col.CompareTag(playerTag))
        {
            filler.FillTheBucket(Inventory.Instance,Rate);
            filler.TakeTheEffect();
        }
    }
}
