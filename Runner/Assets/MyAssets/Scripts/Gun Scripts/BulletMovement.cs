using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private int currentBulletPower;

    public int CurrentBulletPower
    {
        get => (int)SaveManager.Instance.BulletPower;
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.forward; 
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Target"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
