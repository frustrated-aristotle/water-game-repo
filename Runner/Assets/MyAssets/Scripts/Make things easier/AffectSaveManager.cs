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
        SaveManager.Instance.Capacity = intChangeValue;
        changedValue = SaveManager.Instance.Capacity;
        Debug.Log("Save Manager: " + SaveManager.Instance.Capacity);
    }

    [ContextMenu("Give Money Amount")]
    public void GiveMoneyAmount()
    {
        SaveManager.Instance.Currency = intChangeValue;
        changedValue = SaveManager.Instance.Currency;
    }
}
