using System;
using HyperCasual.Runner;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject moneyPrefab;
    
    public int health;

    public int stepValue;

    [SerializeField] 
    GameObject healthTxt;

    #region Position Related

    [SerializeField] private float posZValue;
    [SerializeField] private float baseZ;

    #endregion

    public int Health { get => health; private set => health -= value;}


    private void Start()
    {
        TakeHealthByStep();
    }

    private void TakeHealthByStep()
    {
        ChangePlayerToGun armory = FindObjectOfType<ChangePlayerToGun>();
        baseZ = armory.transform.position.z;
        posZValue = transform.position.z;
        stepValue = TargetManager.StepValue((int)posZValue, (int)baseZ);
        health = TargetManager.GiveHealth(stepValue);
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
            int damageAmount = col.GetComponent<BulletMovement>().CurrentBulletPower;
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
        Debug.LogError("dmg amount : " + damageAmount);
        Health = damageAmount;
            UpdateHealthText();
    }
}
