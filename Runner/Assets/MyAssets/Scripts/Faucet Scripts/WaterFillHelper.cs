using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Runner;
using TMPro;
using UnityEngine;

public static class WaterFillHelper
{
    public static void FillWater(Collider col, string playerTag, IFillTheBucket filler, string key)
    {
        if (col.CompareTag(playerTag))
        {
           
            filler.FillTheBucket(Inventory.Instance,key);
            filler.TakeTheEffect();
        }
    }
}
