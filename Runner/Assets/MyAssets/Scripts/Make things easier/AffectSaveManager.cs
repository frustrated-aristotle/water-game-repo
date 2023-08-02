using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;

public class AffectSaveManager : MonoBehaviour
{
    public float floatChangeValue;
    public int intChangeValue;
    [SerializeField]float changedValue;
    
    [ContextMenu("Give Bucket Capacity")]
    public void GiveBucketCapacity()
    {
        SaveManager.Instance.BucketCapacity = intChangeValue;
        changedValue = SaveManager.Instance.BucketCapacity;
        Debug.Log("Save Manager: " + SaveManager.Instance.BucketCapacity);
    }

    [ContextMenu("Give Money Amount")]
    public void GiveMoneyAmount()
    {
        SaveManager.Instance.Currency = intChangeValue;
        changedValue = SaveManager.Instance.Currency;
    }
}
