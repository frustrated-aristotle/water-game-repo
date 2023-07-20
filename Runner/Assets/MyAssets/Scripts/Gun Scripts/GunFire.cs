using System;
using System.Collections;
using HyperCasual.Runner;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    private ObjectPooler pool;
    private bool isFiring;
    public bool IsFiring { get => isFiring;}
    private int i = 0;
    [SerializeField]
    private float rate = 0.7f;
    
    //Rate Related
    public float Rate { get => rate; set => value = rate; }
    
    public static GunFire Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        pool = ObjectPooler.Instance;
        /*
        StartFiring();
         */
        StopFiring();
    }

    private IEnumerator Fire()
    {
        while (isFiring)
        {
            FireGun();
            yield return new WaitForSeconds(Rate);
        }
    }
    public void StartFiring()
    {
        isFiring = true;
        StartCoroutine(Fire());
    }
    public void StopFiring()
    {
        isFiring = false;
    }
    private void FireGun()
    {
        Debug.Log("Fired");
        Vector3 pos = PlayerController.Instance.transform.position + new Vector3(0, 3, 2.6f);
        GameObject a = pool.SpawnFromPool("Bullet", pos, Quaternion.identity);
        a.transform.Rotate(90,0,0);
        if (Inventory.Instance.BucketFilledAmount - a.GetComponent<BulletMovement>().CurrentBulletPower > 0)
        {
            Inventory.Instance.BucketFilledAmount = -a.GetComponent<BulletMovement>().CurrentBulletPower;
        }
        else
        {
            a.SetActive(false);
            isFiring = false;
        }
        //Inventory.Instance.BucketFilledAmount = -10;
    }
}
