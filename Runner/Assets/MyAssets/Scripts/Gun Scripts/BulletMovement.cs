using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]private float bulletPower;
    
    public float BulletPower
    {
        get;
        private set;
    }

    private void OnEnable()
    {
        BulletPower = bulletPower;
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.forward; 
    }
}
