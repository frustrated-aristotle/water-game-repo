using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;
//Polymorphism kullanarak bu sorunun bir çözümünü oluşturma
[RequireComponent(typeof(FillWater))]
public class Faucet : Spawnable
{
    private const string playerTag = "Player";
    private IFillTheBucket filler;
    private int rate;

    public int Rate {get => rate; set => rate = value;}

    #region Unnecessary Part
    private bool applied;
    public override void ResetSpawnable()
    {
        applied = false;
    }   
    #endregion

    protected override void Awake()
    {
        base.Awake();
        filler = GetComponent<IFillTheBucket>();
    }
    private void Start()
    {
//        Rate = (int)SaveManager.Instance.FaucetRate;

    }

    private void OnTriggerEnter(Collider col)
    {
        WaterFillHelper.FillWater(col,playerTag,filler,"FaucetRate");
    }

    private void OnTriggerStay(Collider col)
    {
        WaterFillHelper.FillWater(col,playerTag,filler,"FaucetRate");
    }
}
