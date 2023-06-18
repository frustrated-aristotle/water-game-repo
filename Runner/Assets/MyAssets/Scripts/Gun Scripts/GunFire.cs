using System;
using System.Collections;
using HyperCasual.Runner;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    private ObjectPooler pool;
    private bool isFiring;
    private int i = 0;
    [SerializeField]
    private float rate = 1;

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
            yield return new WaitForSeconds(rate);
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
        Debug.LogError("GUN");
        Vector3 pos = PlayerController.Instance.transform.position + new Vector3(0, 3, 2.6f);
        GameObject a = pool.SpawnFromPool("Bullet", pos, Quaternion.identity);
        a.transform.Rotate(90,0,0);

        //Inventory.Instance.BucketFilledAmount = -10;
        Inventory.Instance.BucketFilledAmount = -a.GetComponent<BulletMovement>().BulletPower;
        Debug.LogError("Bullet Power: " + a.GetComponent<BulletMovement>().BulletPower);
    }
}
