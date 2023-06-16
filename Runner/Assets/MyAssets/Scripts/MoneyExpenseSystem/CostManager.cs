using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostManager : MonoBehaviour
{
    private int mainBucketCapacityCost;
    private int currentBucketCapacityCost;
    private float bucketCapacityCostMultiplier;
    private float multiplierModifier = .2f;
    
    public int BucketCapacityCost
    { 
        get => currentBucketCapacityCost;
        set
        {
            bucketCapacityCostMultiplier +=  multiplierModifier;
            currentBucketCapacityCost = (int)(mainBucketCapacityCost * bucketCapacityCostMultiplier);
        }
    }
    
    
}
