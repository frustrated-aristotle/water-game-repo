using System;
using HyperCasual.Runner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject moneyPrefab;
    
    public int health;

    public int targetStep;

    [SerializeField] 
    GameObject healthTxt;

    [SerializeField]private float firstStepZValue;

    public int Health { get => health; private set => health -= value;}

    private void OnEnable()
    {
        float posZValue = transform.position.z;
        targetStep = (int)(posZValue - firstStepZValue) /5 +1;
        health = (int)(targetStep * 1.5f * 5);
        //ne sarayda ne handa
        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        healthTxt.GetComponent<TextMeshPro>().text = health.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        Transform col = other.transform;
        bool isCollisionPlayer = col.CompareTag("Player");

        if (isCollisionPlayer)
        {
            GameManager.Instance.Win();
        }

      
    }

    private void OnCollisionEnter(Collision _collision)
    {
        Transform col = _collision.transform;

        bool isCollisionBullet = col.CompareTag("Bullet");
        if (isCollisionBullet)
        {
            int damageAmount = col.GetComponent<BulletMovement>().BulletPower;
            TakeAHit(damageAmount);
            if (Health <= 0)
            {
                InstantiateMoneyAfterTheTarget();
               // DeactivateBullet(col.gameObject);
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
    

    private void TakeAHit(int damageAmount)
    {
        Debug.LogError("Take A Hit");
        Health = damageAmount;
            UpdateHealthText();
    }
}
