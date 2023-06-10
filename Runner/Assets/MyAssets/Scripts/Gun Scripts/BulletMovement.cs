using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]private int bulletPower;


    public int BulletPower
    {
        get => bulletPower;
        private set => bulletPower = value;
    }

    private void OnEnable()
    {
        bulletPower = GameManager.Instance.BulletPower;
    }


    private void FixedUpdate()
    {
        transform.position += Vector3.forward; 
    }
}
