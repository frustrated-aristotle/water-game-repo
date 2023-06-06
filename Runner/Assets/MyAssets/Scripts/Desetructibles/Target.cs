using System;
using HyperCasual.Runner;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject moneyPrefab;
    
    [SerializeField] private float health;
    
    public float Health { get => health; private set => health -= value;}
    private void OnTriggerEnter(Collider other)
    {
        Transform col = other.transform;
        bool isCollisionPlayer = col.CompareTag("Player");

        if (isCollisionPlayer)
        {
            GameManager.Instance.Lose();
        }
    }

    private void OnCollisionEnter(Collision _collision)
    {
        Transform col = _collision.transform;

        bool isCollisionBullet = col.CompareTag("Bullet");
        if (isCollisionBullet)
        {
            float damageAmount = col.GetComponent<BulletMovement>().BulletPower;
            TakeAHit(damageAmount);
            if (Health <= 0)
            {
                InstantiateMoneyAfterTheTarget();
                DeactivateBullet(col.gameObject);
                DestroyTarget();
            }
        }
    }

    private void InstantiateMoneyAfterTheTarget()
    {
        Vector3 moneyPos = transform.position;
        moneyPos.y = 1.25f;
        Instantiate(moneyPrefab, moneyPos, Quaternion.identity);
    }

    private void DeactivateBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
    
    private void DestroyTarget()
    {
        Destroy(this.gameObject);
    }
    

    private void TakeAHit(float damageAmount)
    {
        Health = damageAmount;
        transform.GetChild(0).GetComponent<TextMeshPro>().text = health.ToString();
    }
}
