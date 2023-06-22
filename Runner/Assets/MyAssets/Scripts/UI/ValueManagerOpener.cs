using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueManagerOpener : VariableManager
{
    [SerializeField] private List<TMP_InputField> inputsFirstScreen = new List<TMP_InputField>();
    public void OpenManager(GameObject managerScreen)
    {
        Debug.LogError("OPENED");
        managerScreen.SetActive(true);
    }
    
    public void CloseManager(GameObject managerScreen)
    {
        managerScreen.SetActive(false);
    }

    public void SaveValues()
    {
        //First is upgrade cost starting value:
        initialIncomeIncreaseCost = int.Parse(inputsFirstScreen[0].text);
    }
}
